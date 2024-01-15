using System.Collections.Generic;
using castledice_game_logic.MovesLogic;

namespace Src.PVE
{
    public class RandomMoveSearcher : IBestMoveSearcher
    {
        public AbstractMove GetBestMove(List<AbstractMove> moves)
        {
            var rnd = new System.Random();
            var index = rnd.Next(moves.Count);
            return moves[index];
        }
    }
}