using Riptide;

namespace Src.GameplayView.ServerConnection
{
    public interface IRejectReasonMessagesConfig
    {
        string GetRejectReasonMessage(RejectReason reason);
    }
}