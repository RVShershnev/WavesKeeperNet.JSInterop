using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavesKeeperNet.JSInterop
{
    public class WavesKeeperFiledRequestException : WavesKeeperException
    {
        public WavesKeeperFiledRequestException() : base("Filed request") { }
    }
}
