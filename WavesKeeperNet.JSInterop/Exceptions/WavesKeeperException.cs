using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavesKeeperNet.JSInterop
{
    public class WavesKeeperException : Exception
    {
        public WavesKeeperException(string message) : base(message) { }


        public static WavesKeeperException Parse(string message) => message switch
        {
            "App is locked" => new WavesKeeperAppIsLockedException(),
            "Add Waves Keeper account" => new WavesKeeperAddWavesKeeperAccountException(),
            "Init Waves Keeper and add account" => new WavesKeeperInitWavesKeeperAndAddAccountException(),
            "User denied message" => new WavesKeeperUserDeniedMessageException(),
            "Api rejected by user" => new WavesKeeperUserDeniedMessageException(),
            "Filed request" => new WavesKeeperFiledRequestException(),
            _ => throw new WavesKeeperException(message),
        };

    }
}
