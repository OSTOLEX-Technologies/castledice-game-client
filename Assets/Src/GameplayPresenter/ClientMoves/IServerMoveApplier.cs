using System.Threading.Tasks;
using castledice_game_data_logic.Moves;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayPresenter.ClientMoves
{
    public interface IServerMoveApplier
    {
        Task<MoveApplicationResult> ApplyMoveAsync(MoveData moveData);
    }
}