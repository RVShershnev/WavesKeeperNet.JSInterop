using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavesKeeperNet.JSInterop
{
    public class AuthData
    {
        /// <summary>
        /// a string line with any data (required field)
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// name of the service (optional field)
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// path to the logo relative to the referreror origin of the website (optional field)
        /// </summary>
        public string? Icon { get; set; }
        /// <summary>
        /// a websites' full URL for redirect (optional field)
        /// </summary>
        public string? Referrer { get; set; }

        /// <summary>
        /// relative path to the website's Auth API (optional field)
        /// </summary>
        public string? SuccessPath { get; set; }
    }
}
