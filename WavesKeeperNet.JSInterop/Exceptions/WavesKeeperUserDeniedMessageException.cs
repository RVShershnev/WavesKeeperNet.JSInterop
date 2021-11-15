using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavesKeeperNet.JSInterop
{
    public class WavesKeeperUserDeniedMessageException : WavesKeeperException
    {
        public WavesKeeperUserDeniedMessageException() : base("User denied message") { }
    }
}
