namespace MetaMask.Runtime
{
    public interface IAppConfig
    {
        string AppName { get; }
        
        string AppUrl { get; }
        
        string AppIcon { get; }
    }
}