using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Checkers
{
    public class BaseCaptureChecker : IBaseCaptureChecker
    {
        private readonly Board _board;

        public BaseCaptureChecker(Board board)
        {
            _board = board;
        }

        public bool WillCaptureBase(AbstractMove move)
        {
            if (move is not CaptureMove) return false;
            var cell = _board[move.Position];
            if (cell.GetContent().Exists(c => c is ICapturable))
            {
                var capturable = cell.GetContent().Find(c => c is ICapturable) as ICapturable;
                var player = move.Player;
                if (!capturable.CanBeCaptured(player)) return false;
            }

            return false;
        }
    }
}