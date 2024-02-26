using System.Collections.Generic;

namespace MetaMask.Plugins.Libraries.SocketIOUnity.Runtime.SocketIOClient.JsonSerializer
{
    public interface IJsonSerializer
    {
        JsonSerializeResult Serialize(object[] data);
        T Deserialize<T>(string json);
        T Deserialize<T>(string json, IList<byte[]> incomingBytes);
    }
}
