using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using NUnit.Framework.Constraints;
using Src.PVE.MoveSearchers.TraitsEvaluators;
using Src.PVE.TraitsEvaluators;

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
        
        private Player _player;
        
        private IMoveTraitEvaluator _destructivenessEvaluator;
        private IMoveTraitEvaluator _aggressivenessEvaluator;
        private IMoveTraitEvaluator _defensivenessEvaluator;

        private IBoardStateDistancesCalculator _distancesCalculator;
        private IBoardCellsStateCalculator _boardCellsStateCalculator;

        public BalancedMoveSearcher(IMoveTraitEvaluator destructivenessEvaluator, IMoveTraitEvaluator aggressivenessEvaluator, IMoveTraitEvaluator defensivenessEvaluator, IBoardStateDistancesCalculator distancesCalculator, IBoardCellsStateCalculator boardCellsStateCalculator, Player player)
        {
            _destructivenessEvaluator = destructivenessEvaluator;
            _aggressivenessEvaluator = aggressivenessEvaluator;
            _defensivenessEvaluator = defensivenessEvaluator;
            _distancesCalculator = distancesCalculator;
            _boardCellsStateCalculator = boardCellsStateCalculator;
            _player = player;
        }

        public AbstractMove GetBestMove(List<AbstractMove> moves)
        {
            var movesToTraits = GetMovesToTraits(moves);
            var enemyProximity = GetEnemyProximity();
            if (enemyProximity < 5)
            {
                return GetMostDefensiveMove(movesToTraits);
            }
            return GetMostDestructiveMove(movesToTraits);
        }

        private float GetEnemyProximity()
        {
            var currentBoardState = _boardCellsStateCalculator.GetCurrentBoardState(_player);
            var proximity = _distancesCalculator.GetMinimalDistanceBetweenCellStates(currentBoardState, CellState.FriendlyBase, CellState.Enemy);
            return proximity;
        }

        private Dictionary<AbstractMove, MoveTraits> GetMovesToTraits(List<AbstractMove> moves)
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
            return movesToTraits;
        }

        private AbstractMove GetMostDestructiveMove(Dictionary<AbstractMove, MoveTraits> movesToTraits)
        {
            return GetMoveWithBiggestTrait(movesToTraits, traits => traits.Destructiveness);
        }
        
        private AbstractMove GetMostDefensiveMove(Dictionary<AbstractMove, MoveTraits> movesToTraits)
        {
            return GetMoveWithBiggestTrait(movesToTraits, traits => traits.Defensiveness);
        }
        
        private AbstractMove GetMoveWithBiggestTrait(Dictionary<AbstractMove, MoveTraits> movesToTraits, Func<MoveTraits, float> traitGetter)
        {
            AbstractMove bestMove = null;
            float maxTrait = float.MinValue;
            foreach (var move in movesToTraits.Keys)
            {
                if (traitGetter(movesToTraits[move]) > maxTrait)
                {
                    maxTrait = traitGetter(movesToTraits[move]);
                    bestMove = move;
                }
            }
            return bestMove;
        }
    }
}