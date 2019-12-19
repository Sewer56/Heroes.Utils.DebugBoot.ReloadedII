using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using Heroes.Utils.DebugBoot.Configuration;
using Heroes.Utils.DebugBoot.Heroes;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Memory.Interop;
using Reloaded.Memory.Pointers;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace Heroes.Utils.DebugBoot
{
    public unsafe class DebugBoot
    {
        // Hooks
        private IAsmHook        _loadMainMenuHook;
        private IAsmHook        _bootHook;
        
        // Config
        private Config.Config  _config;
        private Configurator   _configurator;

        // Dynamic Config (Config Deconstructed)
        // Note: Enums are not blittable in .NET, even if their underlying types can be.
        private Pinnable<int> _mainMenuSystemMode = new Pinnable<int>((int) SystemMode.MainMenu);
        private Pinnable<int> _bootSystemMode = new Pinnable<int>((int) SystemMode.MainMenu);

        // Game Variables
        private Stage* _stageId   = (Stage*) 0x8D6720;
        private Team* _teamOne    = (Team*) 0x8D6920;
        private Team* _teamTwo    = (Team*) 0x8D6924;
        private Team* _teamThree  = (Team*) 0x8D6928;
        private Team* _teamFour   = (Team*) 0x8D692C;

        // For use in our ASM.
        private Pinnable<IntPtr> _ediBackup = new Pinnable<IntPtr>((IntPtr) 0x0);

        public DebugBoot(string modDirectory, IReloadedHooks hooks)
        {
            // Setup Config
            _configurator = new Configurator(modDirectory);
            _config       = _configurator.GetConfiguration<Config.Config>(0);
            _config.ConfigurationUpdated += configurable =>
            {
                _config = (Config.Config) configurable;
                ConfigToPinnable(_config);
                Console.WriteLine($"Debug Boot Configuration Updated. Set new main menu mode.");
            };

            ConfigToPinnable(_config);

            // Apply Boot Time Constants
            *_stageId = _config.BootOptions.Stage;
            *_teamOne = _config.BootOptions.TeamP1;
            *_teamTwo   = _config.BootOptions.TeamP2;
            *_teamThree = _config.BootOptions.TeamP3;
            *_teamFour  = _config.BootOptions.TeamP4;

            // Setup mid function hooks.
            // Disasm name: Main::Loop
            string[] asmHookSetMainMenu =
            {
                $"use32",
                $"mov ecx, dword [{(IntPtr)_mainMenuSystemMode.Pointer}]", // New line, sacrificing ECX.
                $"mov [eax + 0x38], ecx",
                $"mov ecx, [esi]",
                $"mov [esi + 4], edi",
            };
            
            string[] asmHookStartGame =
            {
                $"use32",
                $"mov [{(IntPtr)_ediBackup.Pointer}], edi", // Backup EDI
                $"mov edi, dword [{(IntPtr)_bootSystemMode.Pointer}]", // Replace EDI
                $"mov [0xA82034], ebp",
                $"mov [eax + 0x38], edi", // Set SystemMode
                $"mov edi, dword [{(IntPtr)_ediBackup.Pointer}]", // Restore EDI
            };

            _loadMainMenuHook = hooks.CreateAsmHook(asmHookSetMainMenu, 0x427342, AsmHookBehaviour.DoNotExecuteOriginal).Activate();
            _bootHook = hooks.CreateAsmHook(asmHookStartGame, 0x00427138, AsmHookBehaviour.DoNotExecuteOriginal).Activate();
        }

        private void ConfigToPinnable(Config.Config config)
        {
            _mainMenuSystemMode.Value = (int) config.MainMenuMode;
            _bootSystemMode.Value = (int) config.BootMode;
        }

        public void Resume()
        {
            _bootHook?.Enable();
            _loadMainMenuHook?.Enable();
        }

        public void Suspend()
        {
            _bootHook?.Disable();
            _loadMainMenuHook?.Disable();
        }
    }
}
