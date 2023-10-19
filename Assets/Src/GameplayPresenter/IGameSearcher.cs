using System.Threading.Tasks;

namespace Src.GameplayPresenter
{
    public interface IGameSearcher
    {
        Task<GameSearchResult> SearchGameAsync(string playerToken);
        Task<bool> CancelGameSearchAsync(string playerToken);
    }
}