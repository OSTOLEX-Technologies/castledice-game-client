using System;
using castledice_events_logic.ServerToClient;
using castledice_game_data_logic;

namespace Src.GameplayPresenter.GameCreation.GameSearching
{
    public class GameSearcher : IGameSearcher, ICreateGameDtoAccepter, ICancelGameResultDtoAccepter
    {
        public event Action<GameStartData> GameFound;
        public event Action<SearchFailReason> SearchFailed;
        public event Action CancellationApproved;
        
        public void Search()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void AcceptCreateGameDto(CreateGameDTO dto)
        {
            throw new NotImplementedException();
        }

        public void AcceptCancelGameResultDto(CancelGameResultDTO cancelGameResultDto)
        {
            throw new NotImplementedException();
        }
    }
}