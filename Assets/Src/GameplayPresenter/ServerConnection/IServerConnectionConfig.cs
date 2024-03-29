namespace Src.GameplayPresenter.ServerConnection
{
    public interface IServerConnectionConfig
    {
        string HostAddress { get; }
        int MaxConnectionAttempts { get; }
    }
}