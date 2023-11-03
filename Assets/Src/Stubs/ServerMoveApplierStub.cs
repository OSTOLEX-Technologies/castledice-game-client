using System.Threading.Tasks;
using castledice_game_data_logic.Moves;
using Src.GameplayPresenter.ClientMoves;

namespace Src.Stubs
{
    public class ServerMoveApplierStub : IServerMoveApplier
    {
        public async Task<MoveApplicationResult> ApplyMoveAsync(MoveData moveData, string playerToken)
        {
            return MoveApplicationResult.Applied;
        }
    }
}