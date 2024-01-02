using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Request_Proxies.Metamask
{
    [Serializable]
    public class MetamaskRefreshRequestDtoProxy
    {
        [JsonProperty("wallet_id")]
        public string WalletID { get; private set; }

        public MetamaskRefreshRequestDtoProxy(string walletID)
        {
            WalletID = walletID;
        }

        public Dictionary<string, string> AsDictionary()
        {
            return new Dictionary<string, string>
            {
                { "wallet_id", WalletID },
            };
        }
    }
}