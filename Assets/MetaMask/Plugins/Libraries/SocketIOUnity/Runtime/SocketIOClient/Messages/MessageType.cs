namespace MetaMask.Plugins.Libraries.SocketIOUnity.Runtime.SocketIOClient.Messages
{
    public enum MessageType
    {
        Opened,
        Ping = 2,
        Pong,
        Connected = 40,
        Disconnected,
        EventMessage,
        AckMessage,
        ErrorMessage,
        BinaryMessage,
        BinaryAckMessage
    }
}
