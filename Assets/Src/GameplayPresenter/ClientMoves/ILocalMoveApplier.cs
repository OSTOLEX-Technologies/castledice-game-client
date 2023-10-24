using castledice_game_logic.MovesLogic;

namespace Src.GameplayPresenter.ClientMoves
{
    public interface ILocalMoveApplier
    {
        void ApplyMove(AbstractMove move);
    }
}