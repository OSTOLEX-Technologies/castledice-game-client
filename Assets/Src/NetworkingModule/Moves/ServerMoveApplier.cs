using System.Threading.Tasks;
using casltedice_events_logic.ServerToClient;
using castledice_game_data_logic.Moves;
using Src.GameplayPresenter.ClientMoves;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule.Moves
{
    public class ServerMoveApplier : IServerMoveApplier, IApproveMoveDTOAccepter
    {
        private TaskCompletionSource<MoveApplicationResult> _moveApplicationResultTcs;
        private readonly IMessageSender _messageSender;

        public ServerMoveApplier(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public Task<MoveApplicationResult> ApplyMoveAsync(MoveData moveData, string playerToken)
        {
            throw new System.NotImplementedException();
        }

        public void AcceptApproveMoveDTO(ApproveMoveDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}