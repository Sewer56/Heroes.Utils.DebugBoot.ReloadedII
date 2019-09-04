using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.ReloadedII.Interfaces;

namespace Heroes.Utils.DebugBoot
{
    public unsafe class DebugBoot
    {
        private IAsmHook        _loadMainMenuHook;
        private IAsmHook        _bootHook;

        private Config.Config   _config;

        private int* _stageId  = (int*) 0x8D6720;
        private int* _teamOne      = (int*) 0x8D6920;
        private int* _teamTwo      = (int*) 0x8D6924;
        private int* _teamThree    = (int*) 0x8D6928;
        private int* _teamFour     = (int*) 0x8D692C;


        public DebugBoot(string modDirectory, IReloadedHooks hooks)
        {
            _config         = Config.Config.FromJson(modDirectory);
            _config.ToJson(modDirectory);

            int startGameSystemMode  = _config.LoadIntoLevel.Enable ? 2 : 3;
            *_stageId = _config.LoadIntoLevel.LevelId;
            *_teamOne = (byte) _config.LoadIntoLevel.TeamId;
            *_teamTwo   = -1;
            *_teamThree = -1;
            *_teamFour  = -1;

            // 0x427342, 0x00427138 : MID FUNCTION HOOKS !! 
            // Disasm name: Main::Loop
            if (_config.ReplaceMainMenuWithDebug.Enable)
            {
                int mainMenuSystemMode = _config.ReplaceMainMenuWithDebug.RestartStage ? 2 : 3;

                string[] asmHookSetMainMenu =
                {
                    $"use32",
                    $"mov [eax + 0x38], dword {mainMenuSystemMode}",
                    $"mov ecx, [esi]",
                    $"mov [esi+4], edi",
                };

                _loadMainMenuHook = hooks.CreateAsmHook(asmHookSetMainMenu, 0x427342, AsmHookBehaviour.DoNotExecuteOriginal).Activate();
            }

            string[] asmHookStartGame =
            {
                $"use32",
                $"mov [0xA82034], ebp",
                $"mov [eax+0x38], dword {startGameSystemMode}"
            };

            _bootHook = hooks.CreateAsmHook(asmHookStartGame, 0x00427138, AsmHookBehaviour.DoNotExecuteOriginal).Activate();
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
