using System.Threading.Tasks;
using casltedice_events_logic.ServerToClient;
using Src.GameplayPresenter;

namespace Src.NetworkingModule
{
    public class RiptideGameSearcher : GameSearcher, IGameCreationDTOAccepter
    {
        private static TaskCompletionSource<GameSearchResult> _searchGameResponseTcs = new();
        private static TaskCompletionSource<bool> _cancelGameResponseTcs = new();
        private readonly IMessageSender _messageSender;

        public RiptideGameSearcher(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public override async Task<GameSearchResult> SearchGameAsync(string playerToken)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<bool> CancelGameSearch(string playerToken)
        {
            throw new System.NotImplementedException();
        }

        public void AcceptCreateGameDTO(CreateGameDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public void AcceptCancelGameResultDTO(CancelGameResultDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}