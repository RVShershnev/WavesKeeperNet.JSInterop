using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavesKeeperNet.JSInterop
{
    public class AuthData
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Referrer { get; set; }
        public string SuccessPath { get; set; }
    }

    public class AuthDataResponce
    {
        public string host { get; set; }
        public string name { get; set; }
        public string prefix { get; set; }
        public string address { get; set; }
        public string publicKey { get; set; }
        public string signature { get; set; }
        public int version { get; set; }
    }
}
