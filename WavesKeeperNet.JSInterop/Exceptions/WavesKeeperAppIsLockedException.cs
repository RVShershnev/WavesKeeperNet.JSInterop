using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavesKeeperNet.JSInterop
{
    public class WavesKeeperAppIsLockedException : WavesKeeperException 
    {
        public WavesKeeperAppIsLockedException() : base("App is locked") { }
    }
}
