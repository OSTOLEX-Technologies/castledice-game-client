using System;
using Newtonsoft.Json;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models
{
    public class JsonRpcBase
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpc { get; } = "2.0";
        
        [JsonProperty("id")]
        public string Id { get; protected set; }
        
        protected long RandomId(long min, long max, Random rand) {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
    }
}