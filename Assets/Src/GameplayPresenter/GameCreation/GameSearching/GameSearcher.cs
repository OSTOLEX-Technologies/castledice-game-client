using System;
using castledice_events_logic.ServerToClient;
using castledice_game_data_logic;
using Src.NetworkingModule.DTOAccepters;

namespace Src.GameplayPresenter.GameCreation.GameSearching
{
    public class GameSearcher : IGameSearcher, IGameCreationDTOAccepter
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

        
        public void AcceptCreateGameDTO(CreateGameDTO dto)
        {
            throw new NotImplementedException();
        }

        public void AcceptCancelGameResultDTO(CancelGameResultDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}