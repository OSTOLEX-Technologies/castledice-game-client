namespace EventEmitter.NET.MetaMask.Plugins.Libraries.EventEmitter.NET.Runtime.Interfaces
{
    /// <summary>
    /// An interface that represents a class that triggers events that can be listened to.
    /// </summary>
    public interface IEvents
    {
        /// <summary>
        /// The EventDelegator that should be used to listen to (or trigger) events
        /// </summary>
        EventDelegator Events { get; }
    }
}
