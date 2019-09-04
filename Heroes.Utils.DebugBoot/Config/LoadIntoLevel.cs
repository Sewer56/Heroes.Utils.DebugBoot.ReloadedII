using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Utils.DebugBoot.Config
{
    public class LoadIntoLevel
    {
        public bool Enable { get; set; } = false;
        public int LevelId { get; set; } = 2;
        public int TeamId  { get; set; } = 0;
    }
}
