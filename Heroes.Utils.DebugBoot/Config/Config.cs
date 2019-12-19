using System.ComponentModel;
using Heroes.Utils.DebugBoot.Configuration;
using Heroes.Utils.DebugBoot.Heroes;

namespace Heroes.Utils.DebugBoot.Config
{
    public class Config : Configurable<Config>
    {
        [DisplayName("Main Menu Mode")]
        [Description("Decides which mode the game should enter when entering the main menu.")]
        public SystemMode MainMenuMode { get; set; } = SystemMode.EasyMenu;

        [DisplayName("Boot Mode")]
        [Description("Decides into which mode the game should boot into on launch.")]
        public SystemMode BootMode     { get; set; } = SystemMode.EasyMenu;

        [DisplayName("Boot Options")]
        [Description("Options that are applied one time when the game boots.")]
        public BootOptions BootOptions { get; set; } = BootOptions.Default;
    }
}
