using castledice_game_logic;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Checkers
{
    public interface IBaseCaptureChecker
    {
        bool WillCaptureBase(AbstractMove move);
    }
}