using System.Threading.Tasks;

namespace Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting
{
    public interface IGameCancelRequester
    {
        public Task RequestCancelAsync();
    }
}