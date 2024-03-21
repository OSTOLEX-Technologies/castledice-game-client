using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Src.OLDPVE.MoveSearchers.TraitsEvaluators;
using Random = UnityEngine.Random;

namespace Src.OLDPVE.MoveSearchers
{
    public class BalancedMoveSearcher : IBestMoveSearcher
    {
        private readonly IMoveTraitEvaluator _aggressivenessEvaluator;
        private readonly IBoardCellsStateCalculator _boardCellsStateCalculator;
        private readonly IMoveTraitEvaluator _defensivenessEvaluator;

        private readonly IMoveTraitEvaluator _destructivenessEvaluator;

        private readonly IBoardStateDistancesCalculator _distancesCalculator;
        private readonly IMoveTraitEvaluator _enhancivenessEvaluator;
        private readonly IMoveTraitEvaluator _harmfulnessEvaluator;

        private readonly Player _player;

        public BalancedMoveSearcher(IMoveTraitEvaluator destructivenessEvaluator,
            IMoveTraitEvaluator aggressivenessEvaluator, IMoveTraitEvaluator defensivenessEvaluator,
            IBoardStateDistancesCalculator distancesCalculator, IBoardCellsStateCalculator boardCellsStateCalculator,
            Player player, IMoveTraitEvaluator enhancivenessEvaluator, IMoveTraitEvaluator harmfulnessEvaluator)
        {
            _destructivenessEvaluator = destructivenessEvaluator;
            _aggressivenessEvaluator = aggressivenessEvaluator;
            _defensivenessEvaluator = defensivenessEvaluator;
            _distancesCalculator = distancesCalculator;
            _boardCellsStateCalculator = boardCellsStateCalculator;
            _player = player;
            _enhancivenessEvaluator = enhancivenessEvaluator;
            _harmfulnessEvaluator = harmfulnessEvaluator;
        }

        public AbstractMove GetBestMove(List<AbstractMove> moves)
        {
            var movesToTraits = GetMovesToTraits(moves);
            var normalizedMovesToTraits = NormalizeMovesToTraits(movesToTraits);
            if (GetEnemyBaseProximity() < 1.3) //Checking if bot units are close to the player base
                return GetBestMove(normalizedMovesToTraits, 0, 1, 0, 0, 0);
            if (GetEnemyProximity() < 5) //Checking if player units are close to the bot base
                return GetBestMove(normalizedMovesToTraits, 0.1f, 0f, 0.4f, 0.3f, 0.2f);
            //Just moving forward
            return GetBestMove(normalizedMovesToTraits, 0.2f, 0.3f, 0f, 0.3f, 0.2f);
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
                var harmfulness = _harmfulnessEvaluator.EvaluateTrait(move);
                movesToTraits.Add(move, new MoveTraits
                {
                    Destructiveness = destructiveness,
                    Aggressiveness = aggressiveness,
                    Defensiveness = defensiveness,
                    Enhanciveness = enhanciveness,
                    Harmfulness = harmfulness
                });
            }

            return movesToTraits;
        }

        private Dictionary<AbstractMove, MoveTraits> NormalizeMovesToTraits(
            Dictionary<AbstractMove, MoveTraits> movesToTraits)
        {
            var normalized = NormalizeTrait(movesToTraits, traits => traits.Destructiveness, (traits, value) =>
            {
                traits.Destructiveness = value;
                return traits;
            });
            normalized = NormalizeTrait(normalized, traits => traits.Aggressiveness, (traits, value) =>
            {
                traits.Aggressiveness = value;
                return traits;
            });
            normalized = NormalizeTrait(normalized, traits => traits.Defensiveness, (traits, value) =>
            {
                traits.Defensiveness = value;
                return traits;
            });
            normalized = NormalizeTrait(normalized, traits => traits.Enhanciveness, (traits, value) =>
            {
                traits.Enhanciveness = value;
                return traits;
            });
            normalized = NormalizeTrait(normalized, traits => traits.Harmfulness, (traits, value) =>
            {
                traits.Harmfulness = value;
                return traits;
            });
            return normalized;
        }

        private Dictionary<AbstractMove, MoveTraits> NormalizeTrait(Dictionary<AbstractMove, MoveTraits> movesToTraits,
            Func<MoveTraits, float> traitGetter, Func<MoveTraits, float, MoveTraits> traitSetter)
        {
            var maxTrait = float.MinValue;
            foreach (var move in movesToTraits.Keys)
                if (traitGetter(movesToTraits[move]) > maxTrait)
                    maxTrait = traitGetter(movesToTraits[move]);
            var minTrait = float.MaxValue;
            foreach (var move in movesToTraits.Keys)
                if (traitGetter(movesToTraits[move]) < minTrait)
                    minTrait = traitGetter(movesToTraits[move]);

            var normalizedMovesToTraits = new Dictionary<AbstractMove, MoveTraits>();
            var minIsEqualToMax = Math.Abs(minTrait - maxTrait) < 0.00001;
            foreach (var move in movesToTraits.Keys)
            {
                var traits = movesToTraits[move];
                //var normalizedValue = minIsEqualToMax ? 0 : NormalizeValue(traitGetter(traits), minTrait, maxTrait);
                //var newTraits = traitSetter(traits, normalizedValue);
                normalizedMovesToTraits.Add(move, traits);
            }

            return normalizedMovesToTraits;
        }

        private float NormalizeValue(float value, float minValue, float maxValue)
        {
            return (value - minValue) / (maxValue - minValue);
        }

        private AbstractMove GetBestMove(Dictionary<AbstractMove, MoveTraits> movesToTraits,
            float destructivenessWeight, float aggressivenessWeight, float defensivenessWeight,
            float enhancivenessWeight, float harmfulnessWeight)
        {
            var bestMoves = GetBestMoves(movesToTraits, destructivenessWeight, aggressivenessWeight,
                defensivenessWeight, enhancivenessWeight, harmfulnessWeight);
            return bestMoves[Random.Range(0, bestMoves.Count)];
        }

        private List<AbstractMove> GetBestMoves(Dictionary<AbstractMove, MoveTraits> movesToTraits,
            float destructivenessWeight, float aggressivenessWeight, float defensivenessWeight,
            float enhancivenessWeight, float harmfulnessWeight)
        {
            var bestMoves = new List<AbstractMove>();
            var bestMoveValue = float.MinValue;
            foreach (var move in movesToTraits.Keys)
            {
                var traits = movesToTraits[move];
                var moveValue = traits.Destructiveness * destructivenessWeight +
                                traits.Aggressiveness * aggressivenessWeight +
                                traits.Defensiveness * defensivenessWeight +
                                traits.Enhanciveness * enhancivenessWeight +
                                traits.Harmfulness * harmfulnessWeight;
                if (moveValue > bestMoveValue) bestMoveValue = moveValue;
            }

            foreach (var move in movesToTraits.Keys)
            {
                var traits = movesToTraits[move];
                var moveValue = traits.Destructiveness * destructivenessWeight +
                                traits.Aggressiveness * aggressivenessWeight +
                                traits.Defensiveness * defensivenessWeight +
                                traits.Enhanciveness * enhancivenessWeight +
                                traits.Harmfulness * harmfulnessWeight;
                if (Math.Abs(moveValue - bestMoveValue) < 0.01) //0.01 is a move picking precision
                    bestMoves.Add(move);
            }

            return bestMoves;
        }

        private float GetEnemyProximity()
        {
            var currentBoardState = _boardCellsStateCalculator.GetCurrentBoardState(_player);
            var proximity =
                _distancesCalculator.GetMinimalDistanceBetweenCellStates(currentBoardState, CellState.FriendlyBase,
                    CellState.Enemy);
            return proximity;
        }

        private float GetEnemyBaseProximity()
        {
            var currentBoardState = _boardCellsStateCalculator.GetCurrentBoardState(_player);
            var proximity =
                _distancesCalculator.GetMinimalDistanceBetweenCellStates(currentBoardState, CellState.Friendly,
                    CellState.EnemyBase);
            return proximity;
        }

        private struct MoveTraits
        {
            public float Destructiveness;
            public float Aggressiveness;
            public float Defensiveness;
            public float Enhanciveness;
            public float Harmfulness;
        }
    }
}