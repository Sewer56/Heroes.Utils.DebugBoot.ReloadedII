using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X86;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using static Reloaded.Hooks.Definitions.X86.FunctionAttribute;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace Heroes.Utils.DebugBoot
{
    public unsafe class Program : IMod
    {
        private IModLoader _modLoader;
        private IReloadedHooks _hooks;
        private string _modDirectory;
        private DebugBoot _debugBoot;

        public static void Main() { }
        public void Start(IModLoaderV1 loader)
        {
            _modLoader = (IModLoader)loader;
            _modLoader.GetController<IReloadedHooks>().TryGetTarget(out _hooks);
            _modDirectory = _modLoader.GetDirectoryForModId("sonicheroes.utils.debugboot");

            /* Your mod code starts here. */
            _debugBoot = new DebugBoot(_modDirectory, _hooks);
        }

        /* Mod loader actions. */
        public void Suspend() => _debugBoot.Suspend();
        public void Resume()  => _debugBoot.Resume();

        public void Unload() => Suspend();

        public bool CanUnload()  => true;
        public bool CanSuspend() => true;

        /* Automatically called by the mod loader when the mod is about to be unloaded. */
        public Action Disposing { get; }

        /* Hook Definitions */
    }                                   
}
