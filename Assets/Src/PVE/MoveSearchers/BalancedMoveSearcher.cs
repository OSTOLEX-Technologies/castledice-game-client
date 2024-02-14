using System.Collections.Generic;
using castledice_game_logic.MovesLogic;

namespace Src.PVE
{
    public class BalancedMoveSearcher : IBestMoveSearcher
    {
        private struct MoveTraits
        {
            public float Destructiveness;
            public float Aggressiveness;
            public float Defensiveness;
        }
        
        private IMoveTraitEvaluator _destructivenessEvaluator;
        private IMoveTraitEvaluator _aggressivenessEvaluator;
        private IMoveTraitEvaluator _defensivenessEvaluator;

        public BalancedMoveSearcher(IMoveTraitEvaluator destructivenessEvaluator, IMoveTraitEvaluator aggressivenessEvaluator, IMoveTraitEvaluator defensivenessEvaluator)
        {
            _destructivenessEvaluator = destructivenessEvaluator;
            _aggressivenessEvaluator = aggressivenessEvaluator;
            _defensivenessEvaluator = defensivenessEvaluator;
        }

        public AbstractMove GetBestMove(List<AbstractMove> moves)
        {
            var movesToTraits = new Dictionary<AbstractMove, MoveTraits>();
            foreach (var move in moves)
            {
                var destructiveness = _destructivenessEvaluator.EvaluateTrait(move);
                var aggressiveness = _aggressivenessEvaluator.EvaluateTrait(move);
                var defensiveness = _defensivenessEvaluator.EvaluateTrait(move);
                movesToTraits.Add(move, new MoveTraits
                {
                    Destructiveness = destructiveness,
                    Aggressiveness = aggressiveness,
                    Defensiveness = defensiveness
                });
            }
            return GetMostDestructiveMove(movesToTraits);
        }

        private AbstractMove GetMostDestructiveMove(Dictionary<AbstractMove, MoveTraits> movesToTraits)
        {
            AbstractMove mostDestructiveMove = null;
            float maxDestructiveness = float.MinValue;
            foreach (var move in movesToTraits.Keys)
            {
                if (movesToTraits[move].Destructiveness > maxDestructiveness)
                {
                    maxDestructiveness = movesToTraits[move].Destructiveness;
                    mostDestructiveMove = move;
                }
            }
            return mostDestructiveMove;
        }
    }
}