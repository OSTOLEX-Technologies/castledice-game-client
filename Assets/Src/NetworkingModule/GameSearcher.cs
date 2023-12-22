using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;
using castledice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.GameplayPresenter.GameCreation;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule
{
    public class GameSearcher : IGameSearcher, IGameCreationDTOAccepter
    {
        private TaskCompletionSource<GameSearchResult> _searchGameResponseTcs = new();
        private TaskCompletionSource<bool> _cancelGameResponseTcs = new();
        private readonly IMessageSender _messageSender;

        public GameSearcher(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public async Task<GameSearchResult> SearchGameAsync(string playerToken)
        {
            var requestGameDTO = new RequestGameDTO(playerToken);
            var message = Message.Create(MessageSendMode.Reliable, ClientToServerMessageType.RequestGame);
            message.AddRequestGameDTO(requestGameDTO);
            _messageSender.Send(message);
            var result = await _searchGameResponseTcs.Task;
            _searchGameResponseTcs = new TaskCompletionSource<GameSearchResult>();
            return result;
        }

        public async Task<bool> CancelGameSearchAsync(string playerToken)
        {
            var cancelGameDTO = new CancelGameDTO(playerToken);
            var message = Message.Create(MessageSendMode.Reliable, ClientToServerMessageType.CancelGame);
            message.AddCancelGameDTO(cancelGameDTO);
            _messageSender.Send(message);
            var result = await _cancelGameResponseTcs.Task;
            _cancelGameResponseTcs = new TaskCompletionSource<bool>();
            return result;
        }

        public void AcceptCreateGameDTO(CreateGameDTO dto)
        {
            var searchResult = new GameSearchResult
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