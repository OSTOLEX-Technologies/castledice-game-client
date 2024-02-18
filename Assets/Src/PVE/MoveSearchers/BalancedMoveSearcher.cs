using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;
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
            public float Enahnciveness;
        }
        
        private Player _player;
        
        private IMoveTraitEvaluator _destructivenessEvaluator;
        private IMoveTraitEvaluator _aggressivenessEvaluator;
        private IMoveTraitEvaluator _defensivenessEvaluator;
        private IMoveTraitEvaluator _enhancivenessEvaluator;

        private IBoardStateDistancesCalculator _distancesCalculator;
        private IBoardCellsStateCalculator _boardCellsStateCalculator;

        public BalancedMoveSearcher(IMoveTraitEvaluator destructivenessEvaluator, IMoveTraitEvaluator aggressivenessEvaluator, IMoveTraitEvaluator defensivenessEvaluator, IBoardStateDistancesCalculator distancesCalculator, IBoardCellsStateCalculator boardCellsStateCalculator, Player player, IMoveTraitEvaluator enhancivenessEvaluator)
        {
            _destructivenessEvaluator = destructivenessEvaluator;
            _aggressivenessEvaluator = aggressivenessEvaluator;
            _defensivenessEvaluator = defensivenessEvaluator;
            _distancesCalculator = distancesCalculator;
            _boardCellsStateCalculator = boardCellsStateCalculator;
            _player = player;
            _enhancivenessEvaluator = enhancivenessEvaluator;
        }

        public AbstractMove GetBestMove(List<AbstractMove> moves)
        {
            var movesToTraits = GetMovesToTraits(moves);
            var normalizedMovesToTraits = NormalizeMovesToTraits(movesToTraits);
            if (GetEnemyProximity() < 5)
            {
                return GetBestMove(normalizedMovesToTraits, 0.1f, 0f, 0.5f, 0.4f);
            }

            return GetBestMove(normalizedMovesToTraits, 0.3f, 0.4f, 0f, 0.3f);
        }
        
        private Dictionary<AbstractMove, MoveTraits> GetMovesToTraits(List<AbstractMove> moves)
        {
            var movesToTraits = new Dictionary<AbstractMove, MoveTraits>();
            foreach (var move in moves)
            {
                var destructiveness = _destructivenessEvaluator.EvaluateTrait(move);
                var aggressiveness = _aggressivenessEvaluator.EvaluateTrait(move);
                var defensiveness = _defensivenessEvaluator.EvaluateTrait(move);
                var enhanciveness = _enhancivenessEvaluator.EvaluateTrait(move);
                movesToTraits.Add(move, new MoveTraits
                {
                    Destructiveness = destructiveness,
                    Aggressiveness = aggressiveness,
                    Defensiveness = defensiveness,
                    Enahnciveness = enhanciveness
                });
            }
            return movesToTraits;
        }

        private Dictionary<AbstractMove, MoveTraits> NormalizeMovesToTraits(Dictionary<AbstractMove, MoveTraits> movesToTraits)
        {
            var normalized = NormalizeTrait(movesToTraits, traits => traits.Destructiveness, (traits, value) => traits.Destructiveness = value);
            normalized = NormalizeTrait(normalized, traits => traits.Aggressiveness, (traits, value) => traits.Aggressiveness = value);
            normalized = NormalizeTrait(normalized, traits => traits.Defensiveness, (traits, value) => traits.Defensiveness = value);
            normalized = NormalizeTrait(normalized, traits => traits.Enahnciveness, (traits, value) => traits.Enahnciveness = value);
            return normalized;
        }
        
        private Dictionary<AbstractMove, MoveTraits> NormalizeTrait(Dictionary<AbstractMove, MoveTraits> movesToTraits, Func<MoveTraits, float> traitGetter, Action<MoveTraits, float> traitSetter)
        {
            var maxTrait = float.MinValue;
            foreach (var move in movesToTraits.Keys)
            {
                if (traitGetter(movesToTraits[move]) > maxTrait)
                {
                    maxTrait = traitGetter(movesToTraits[move]);
                }
            }
            var minTrait = float.MaxValue;
            foreach (var move in movesToTraits.Keys)
            {
                if (traitGetter(movesToTraits[move]) < minTrait)
                {
                    minTrait = traitGetter(movesToTraits[move]);
                }
            }
            
            var normalizedMovesToTraits = new Dictionary<AbstractMove, MoveTraits>();
            foreach (var move in movesToTraits.Keys)
            {
                var traits = movesToTraits[move];
                traitSetter(traits, (traitGetter(traits) - minTrait) / (maxTrait - minTrait));
                normalizedMovesToTraits.Add(move, traits);
            }
            return normalizedMovesToTraits;
        }

        private AbstractMove GetBestMove(Dictionary<AbstractMove, MoveTraits> movesToTraits, float destructivenessWeight, float aggressivenessWeight, float defensivenessWeight, float enhancivenessWeight)
        {
            AbstractMove bestMove = null;
            var bestMoveValue = float.MinValue;
            foreach (var move in movesToTraits.Keys)
            {
                var traits = movesToTraits[move];
                var moveValue = traits.Destructiveness * destructivenessWeight + 
                                traits.Aggressiveness * aggressivenessWeight + 
                                traits.Defensiveness * defensivenessWeight + 
                                traits.Enahnciveness * enhancivenessWeight;
                if (moveValue > bestMoveValue)
                {
                    bestMoveValue = moveValue;
                    bestMove = move;
                }
            }
            return bestMove;
        }
        
        private float GetEnemyProximity()
        {
            var currentBoardState = _boardCellsStateCalculator.GetCurrentBoardState(_player);
            var proximity = _distancesCalculator.GetMinimalDistanceBetweenCellStates(currentBoardState, CellState.FriendlyBase, CellState.Enemy);
            return proximity;
        }


    }
}