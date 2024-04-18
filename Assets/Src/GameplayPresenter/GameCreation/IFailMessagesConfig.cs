using Src.GameplayPresenter.GameCreation.GameSearching;

namespace Src.GameplayPresenter.GameCreation
{
    public interface IFailMessagesConfig
    {
        public string GetFailMessage(SearchFailReason reason);
    }
}