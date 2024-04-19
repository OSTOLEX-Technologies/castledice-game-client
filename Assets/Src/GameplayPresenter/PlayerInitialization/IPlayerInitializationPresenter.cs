using System.Threading.Tasks;

namespace Src.GameplayPresenter.PlayerInitialization
{
    public interface IPlayerInitializationPresenter
    {
        public Task StartInitializationAsync();
    }
}