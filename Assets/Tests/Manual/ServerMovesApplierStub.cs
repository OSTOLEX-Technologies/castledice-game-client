using System.Threading.Tasks;
using castledice_game_logic.MovesLogic;
using Src.GameplayPresenter.ClientMoves;

namespace Tests.Manual
{
    public class ServerMovesApplierStub : IServerMoveApplier
    {
        public Task<MoveApplicationResult> ApplyMoveAsync(AbstractMove move)
        {
            return Task.FromResult(MoveApplicationResult.Applied);
        }
    }
}