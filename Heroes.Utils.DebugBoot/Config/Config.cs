using System.ComponentModel;
using Heroes.Utils.DebugBoot.Configuration;
using Heroes.Utils.DebugBoot.Heroes;

namespace Heroes.Utils.DebugBoot.Config;

public class Config : Configurable<Config>
{
    const string SystemModeDescription = "PalSelect: PS2 Leftover. Allows to select 50/60Hz modes.\n" +
                                         "MainMenu: Boots to Main Menu\n" +
                                         "InGame: Boots inside a Stage\n" +
                                         "EasyMenu: Boots into Debug Menu\n" +
                                         "Credits: Boots into Credits (buggy)\n" +
                                         "EasyMenuMovie: Boots into Movie/FMV Selection";

    [DisplayName("Main Menu Mode")]
    [Description("Decides which mode the game should enter when entering the main menu.\n" +
                 $"{SystemModeDescription}")]
    [DefaultValue(SystemMode.EasyMenu)]
    public SystemMode MainMenuMode { get; set; } = SystemMode.EasyMenu;

    [DisplayName("Boot Mode")]
    [Description("Decides into which mode the game should boot into on launch.\n" +
                 $"{SystemModeDescription}")]
    [DefaultValue(SystemMode.EasyMenu)]
    public SystemMode BootMode     { get; set; } = SystemMode.EasyMenu;

    [DisplayName("Boot Options")]
    [Description("Options that are applied one time when the game boots.")]
    public BootOptions BootOptions { get; set; } = BootOptions.Default;
}