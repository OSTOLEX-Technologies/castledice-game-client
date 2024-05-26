using System;
using System.Threading.Tasks;
using castledice_game_data_logic;

namespace Src.GameplayPresenter.GameCreation.GameSearching
{
    public interface IGameSearcher
    {
        public Task SearchAsync();
        public Task CancelAsync();

        public event Action<GameStartData> GameFound;
        public event Action<SearchFailReason> SearchFailed;
        public event Action CancellationApproved;
        public event Action CancellationFailed;
    }
}