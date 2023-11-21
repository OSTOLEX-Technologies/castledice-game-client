using System.Threading.Tasks;
using castledice_game_data_logic.Moves;
using Src.GameplayPresenter.ClientMoves;

namespace Tests.Manual
{
    public class ServerMovesApplierStub : IServerMoveApplier
    {
        public Task<MoveApplicationResult> ApplyMoveAsync(MoveData moveData, string playerToken)
        {
            return Task.FromResult(MoveApplicationResult.Applied);
        }
    }
}