using System.Collections.Generic;

namespace MetaMask.Plugins.Libraries.SocketIOUnity.Runtime.SocketIOClient.Transport
{
    public class Payload
    {
        public string Text { get; set; }
        public List<byte[]> Bytes { get; set; }
    }
}
