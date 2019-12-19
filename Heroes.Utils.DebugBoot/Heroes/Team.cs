using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Utils.DebugBoot.Heroes
{
    public enum Team : int
    {
        Null = -1,
        Sonic,
        Dark,
        Rose,
        Chaotix,

        /// <summary>
        /// Unused Team. Consists of Espio, Charmy and Big.
        /// Likely a remnant of initial Hard Mode implementation.
        /// </summary>
        ForEDIT
    }
}
