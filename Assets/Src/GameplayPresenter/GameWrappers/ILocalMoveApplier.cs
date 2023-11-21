using castledice_game_logic.MovesLogic;

namespace Src.GameplayPresenter.GameWrappers
{
    public interface ILocalMoveApplier
    {
        void ApplyMove(AbstractMove move);
    }
}