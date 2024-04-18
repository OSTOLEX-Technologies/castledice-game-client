using System.Threading.Tasks;

namespace Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting
{
    public interface IGameRequester
    {
        public Task RequestGameAsync();
    }
}