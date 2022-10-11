using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace Heroes.Utils.DebugBoot;

public class Program : IMod
{
    private IModLoader _modLoader;
    private IReloadedHooks _hooks;
    private DebugBoot _debugBoot;

    public void StartEx(IModLoaderV1 loader, IModConfigV1 config)
    {
        _modLoader = (IModLoader)loader;
        _modLoader.GetController<IReloadedHooks>().TryGetTarget(out _hooks);
        var modDirectory = _modLoader.GetDirectoryForModId(config.ModId);
        var configDirectory = _modLoader.GetModConfigDirectory(config.ModId);

        /* Your mod code starts here. */
        _debugBoot = new DebugBoot(modDirectory, configDirectory, _hooks);
    }

    /* Mod loader actions. */
    public void Suspend() => _debugBoot.Suspend();
    public void Resume()  => _debugBoot.Resume();

    public void Unload() => Suspend();

    public bool CanUnload()  => true;
    public bool CanSuspend() => true;

    /* Automatically called by the mod loader when the mod is about to be unloaded. */
    public Action Disposing { get; }
}