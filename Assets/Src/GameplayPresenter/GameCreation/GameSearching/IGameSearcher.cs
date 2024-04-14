using System;
using castledice_game_data_logic;

namespace Src.GameplayPresenter.GameCreation.GameSearching
{
    public interface IGameSearcher
    {
        public void Search();
        public void Cancel();

        public event Action<GameStartData> GameFound;
        public event Action<GameSearchFailReason> SearchFailed;
        public event Action CancellationApproved;
    }
}