using Heroes.Utils.DebugBoot.Heroes;
using System.ComponentModel;

namespace Heroes.Utils.DebugBoot.Config;

public class BootOptions
{
    public static BootOptions Default => new BootOptions()
    {
        Stage = Stage.SeasideHill,
        TeamP1 = Team.Sonic,
        TeamP2 = Team.Null,
        TeamP3 = Team.Null,
        TeamP4 = Team.Null
    };

    [DisplayName("Stage")]
    [Description($"The stage to boot into when BootMode is set to InGame.")]
    [DefaultValue(Stage.SeasideHill)]
    public Stage Stage { get; set; }

    [DisplayName("Player 1 Team")]
    [DefaultValue(Team.Sonic)]
    public Team TeamP1 { get; set; }

    [DisplayName("Player 2 Team")]
    [DefaultValue(Team.Null)]
    public Team TeamP2 { get; set; }

    [DisplayName("Player 3 Team")]
    [DefaultValue(Team.Null)]
    public Team TeamP3 { get; set; }

    [DisplayName("Player 4 Team")]
    [DefaultValue(Team.Null)]
    public Team TeamP4 { get; set; }
}