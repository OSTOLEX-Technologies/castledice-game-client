using System;
using System.Collections.Generic;
using castledice_game_logic.MovesLogic;

namespace Src.OLDPVE.MoveSearchers
{
    public class RandomMoveSearcher : IBestMoveSearcher
    {
        public AbstractMove GetBestMove(List<AbstractMove> moves)
        {
            var rnd = new Random();
            var index = rnd.Next(moves.Count);
            return moves[index];
        }
    }
}