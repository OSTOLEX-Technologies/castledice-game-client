using Riptide;

namespace Src.GameplayView.ServerConnection
{
    public interface IServerConnectionView
    {
        void ShowConnectingMessage();
        void HideConnectingMessage();
        void ShowConnectionFailedMessage(RejectReason reason);
    }
}