using System;
using castledice_game_data_logic;

namespace Src.GameplayPresenter.GameCreation.GameSearching
{
    public interface IGameSearcher
    {
        public void Search();
        public void Cancel();

        public event Action<GameStartData> GameFound;
        public event Action<SearchFailReason> SearchFailed;
        public event Action CancellationApproved;
    }
}