using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Utils.DebugBoot.Heroes;

namespace Heroes.Utils.DebugBoot.Config
{
    public struct BootOptions
    {
        public static BootOptions Default => new BootOptions()
        {
            Stage = Stage.SeasideHill,
            TeamP1 = Team.Sonic,
            TeamP2 = Team.Null,
            TeamP3 = Team.Null,
            TeamP4 = Team.Null
        };

        public Stage Stage { get; set; }
        public Team TeamP1 { get; set; }
        public Team TeamP2 { get; set; }
        public Team TeamP3 { get; set; }
        public Team TeamP4 { get; set; }
    }
}
