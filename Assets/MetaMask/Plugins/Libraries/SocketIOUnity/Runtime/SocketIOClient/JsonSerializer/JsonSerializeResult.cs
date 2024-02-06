using System.Collections.Generic;

namespace MetaMask.Plugins.Libraries.SocketIOUnity.Runtime.SocketIOClient.JsonSerializer
{
    public class JsonSerializeResult
    {
        public string Json { get; set; }
        public IList<byte[]> Bytes { get; set; }
    }
}
