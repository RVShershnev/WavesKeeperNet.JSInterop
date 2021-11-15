using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavesKeeperNet.JSInterop
{
    public class Account
    {
        public string name { get; set; }
        public string publicKey { get; set; }
        public string address { get; set; }
        public string networkCode { get; set; }
        public Balance balance { get; set; }
    }
    public class Balance
    {
        public string available { get; set; }
        public string leasedOut { get; set; }
    }
    public class Network
    {
        public string code { get; set; }
        public string server { get; set; }
        public string matcher { get; set; }
    }

    public class TxVersion
    {
        public List<int> _3 { get; set; }
        public List<int> _4 { get; set; }
        public List<int> _5 { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// признак того, что Waves Keeper инициализирован.
        /// </summary>
        public bool initialized { get; set; }

        /// <summary>
        ///  признак того, что требуется ввод пароля.
        /// </summary>
        public bool locked { get; set; }

        /// <summary>
        /// текущий аккаунт, если пользователь разрешил доступ веб-сайту, либо null.
        /// </summary>
        public Account account { get; set; }

        /// <summary>
        /// текущая сеть Waves, адрес ноды и матчера.
        /// </summary>
        public Network network { get; set; }

        /// <summary>
        /// статусы запросов на подписание.
        /// </summary>
        public List<object> messages { get; set; }

        /// <summary>
        /// доступные версии транзакций каждого типа.
        /// </summary>
        public TxVersion txVersion { get; set; }
    }
}
