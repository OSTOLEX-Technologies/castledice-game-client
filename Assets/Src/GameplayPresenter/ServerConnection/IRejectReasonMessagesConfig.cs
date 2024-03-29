using Riptide;

namespace Src.GameplayPresenter.ServerConnection
{
    public interface IRejectReasonMessagesConfig
    {
        string GetRejectReasonMessage(RejectReason reason);
    }
}