using System.Threading.Tasks;
using casltedice_events_logic.ClientToServer;
using casltedice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.GameplayPresenter;

namespace Src.NetworkingModule
{
    public class RiptideGameSearcher : IGameSearcher, IGameCreationDTOAccepter
    {
        private readonly TaskCompletionSource<GameSearchResult> _searchGameResponseTcs = new();
        private readonly TaskCompletionSource<bool> _cancelGameResponseTcs = new();
        private readonly IMessageSender _messageSender;

        public RiptideGameSearcher(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public async Task<GameSearchResult> SearchGameAsync(string playerToken)
        {
            var requestGameDTO = new RequestGameDTO(playerToken);
            var message = Message.Create(MessageSendMode.Reliable, ClientToServerMessageType.RequestGame);
            message.AddRequestGameDTO(requestGameDTO);
            _messageSender.Send(message);
            return await _searchGameResponseTcs.Task;
        }

        public async Task<bool> CancelGameSearchAsync(string playerToken)
        {
            var cancleGameDTO = new CancelGameDTO(playerToken);
            var message = Message.Create(MessageSendMode.Reliable, ClientToServerMessageType.CancelGame);
            message.AddCancelGameDTO(cancleGameDTO);
            _messageSender.Send(message);
            return await _cancelGameResponseTcs.Task;
        }

        public void AcceptCreateGameDTO(CreateGameDTO dto)
        {
            var searchResult = new GameSearchResult()
            {
                Status = GameSearchResult.ResultStatus.Success,
                GameStartData = dto.GameStartData
            };
            _searchGameResponseTcs.SetResult(searchResult);
        }

        public void AcceptCancelGameResultDTO(CancelGameResultDTO dto)
        {
            _cancelGameResponseTcs.SetResult(dto.IsCanceled);
            if (!dto.IsCanceled) return;
            var searchResult = new GameSearchResult
            {
                Status = GameSearchResult.ResultStatus.Canceled
            };
            _searchGameResponseTcs.SetResult(searchResult);
        }
    }
}