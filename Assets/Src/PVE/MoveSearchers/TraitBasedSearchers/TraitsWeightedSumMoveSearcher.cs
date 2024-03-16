using System;
using System.Collections.Generic;
using System.Linq;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.PVE.GameSituations;
using Src.PVE.Providers;

namespace Src.PVE.MoveSearchers.TraitBasedSearchers
{
    public class TraitsWeightedSumMoveSearcher : IBestMoveSearcher
    {
        private readonly IMovesListTraitsEvaluator _traitsEvaluator;
        private readonly IMovesTraitsNormalizer _traitsNormalizer;
        private readonly ITotalPossibleMovesProvider _totalPossibleMovesProvider;
        private readonly IRangeRandomNumberGenerator _randomNumberGenerator;
        private readonly Dictionary<IGameSituation, MoveTraitsValues> _situationTraitsWeights;
        private readonly MoveTraitsValues _defaultWeights;

        public TraitsWeightedSumMoveSearcher(IMovesListTraitsEvaluator traitsEvaluator, IMovesTraitsNormalizer traitsNormalizer, ITotalPossibleMovesProvider totalPossibleMovesProvider, IRangeRandomNumberGenerator randomNumberGenerator, Dictionary<IGameSituation, MoveTraitsValues> situationTraitsWeights, MoveTraitsValues defaultWeights)
        {
            _traitsEvaluator = traitsEvaluator;
            _traitsNormalizer = traitsNormalizer;
            _totalPossibleMovesProvider = totalPossibleMovesProvider;
            _randomNumberGenerator = randomNumberGenerator;
            _situationTraitsWeights = situationTraitsWeights;
            _defaultWeights = defaultWeights;
        }

        public AbstractMove GetBestMove(int playerId)
        {
            var moves = _totalPossibleMovesProvider.GetTotalPossibleMoves(playerId);
            var movesToTraits = _traitsEvaluator.EvaluateTraitsForMoves(moves);
            var movesToTraitsNormalized = _traitsNormalizer.NormalizeTraits(movesToTraits);
            var weights = GetWeights();
            var bestMoves = GetBestMoves(movesToTraitsNormalized, weights);
            var bestMove = bestMoves[_randomNumberGenerator.GetRandom(0, bestMoves.Count)];
            return bestMove;            
        }

        private MoveTraitsValues GetWeights()
        {
            foreach (var situation in _situationTraitsWeights.Keys)
            {
                if (situation.IsSituation())
                {
                    return _situationTraitsWeights[situation];    
                }
            }

            return _defaultWeights;
        }
        
        private List<AbstractMove> GetBestMoves(Dictionary<AbstractMove, MoveTraitsValues> movesToTraitsNormalized, MoveTraitsValues weights)
        {
            var movesToValues = new Dictionary<AbstractMove, float>();
            foreach (var move in movesToTraitsNormalized.Keys)
            {
                var traits = movesToTraitsNormalized[move];
                var value = GetWeightedSum(traits, weights);
                movesToValues[move] = value;
            }
            var bestValue = movesToValues.Values.Max();
            var bestMoves = movesToValues.Select(x => x.Key).Where(x => Math.Abs(movesToValues[x] - bestValue) < 0.00001f).ToList();
            return bestMoves;
        }
        
        private static float GetWeightedSum(MoveTraitsValues traits, MoveTraitsValues weights)
        {
            return traits.Destructiveness * weights.Destructiveness +
                   traits.Aggressiveness * weights.Aggressiveness +
                   traits.Defensiveness * weights.Defensiveness +
                   traits.Enhanciveness * weights.Enhanciveness +
                   traits.Harmfulness * weights.Harmfulness;
        }
    }
}