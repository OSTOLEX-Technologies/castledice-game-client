using System.Threading.Tasks;
using casltedice_events_logic.ClientToServer;
using casltedice_events_logic.ServerToClient;
using castledice_game_data_logic.Moves;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.GameplayPresenter.ClientMoves;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule.Moves
{
    public class ServerMoveApplier : IServerMoveApplier, IApproveMoveDTOAccepter
    {
        private TaskCompletionSource<MoveApplicationResult> _moveApplicationResultTcs = new();
        private readonly IMessageSender _messageSender;

        public ServerMoveApplier(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public async Task<MoveApplicationResult> ApplyMoveAsync(MoveData moveData, string playerToken)
        {
            var moveFromClientDTO = new MoveFromClientDTO(moveData, playerToken);
            var message = Message.Create(MessageSendMode.Reliable, ClientToServerMessageType.MakeMove);
            message.AddMoveFromClientDTO(moveFromClientDTO);
            _messageSender.Send(message);
            var result = await _moveApplicationResultTcs.Task;
            _moveApplicationResultTcs = new TaskCompletionSource<MoveApplicationResult>();
            return result;
        }

        public void AcceptApproveMoveDTO(ApproveMoveDTO dto)
        {
            var moveApplicationResult = dto.IsMoveValid
                ? MoveApplicationResult.Applied
                : MoveApplicationResult.Rejected;
            _moveApplicationResultTcs.SetResult(moveApplicationResult);
        }
    }
}