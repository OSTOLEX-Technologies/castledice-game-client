using System.Threading.Tasks;
using castledice_game_data_logic.Moves;
using castledice_game_logic.MovesLogic;
using Src.GameplayPresenter.ClientMoves;

namespace Tests.Manual
{
    public class ServerMovesApplierStub : IServerMoveApplier
    {
        public Task<MoveApplicationResult> ApplyMoveAsync(MoveData moveData)
        {
            return Task.FromResult(MoveApplicationResult.Applied);
        }
    }
}