using System.Threading.Tasks;

namespace Src.GameplayPresenter
{
    public abstract class GameSearcher
    {
        public abstract Task<GameSearchResult> SearchGameAsync(string playerToken);
        public abstract Task<bool> CancelGameSearch(string playerToken);
    }
}