using System;
using Newtonsoft.Json;

namespace evm.net.MetaMask.Plugins.Libraries.evm.net.Runtime.Models
{
    public class JsonRpcRequest<T> : JsonRpcPayload
    {
        [JsonProperty("method")]
        public string Method { get; }
        
        [JsonProperty("params")]
        public T Parameters { get; }

        public JsonRpcRequest(string method, T parameters)
        {
            Method = method;
            Parameters = parameters;

            Id = RandomId(1, long.MaxValue, new Random()).ToString();
        }
    }
}