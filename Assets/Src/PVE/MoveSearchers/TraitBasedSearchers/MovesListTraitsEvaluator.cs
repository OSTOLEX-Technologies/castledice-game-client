using System.Collections.Generic;
using castledice_game_logic.MovesLogic;
using Src.PVE.MoveSearchers.TraitBasedSearchers.TraitsEvaluators;

namespace Src.PVE.MoveSearchers.TraitBasedSearchers
{
    public class MovesListTraitsEvaluator : IMovesListTraitsEvaluator
    {
        private readonly IMoveTraitEvaluator _destructivenessEvaluator;
        private readonly IMoveTraitEvaluator _aggressivenessEvaluator;
        private readonly IMoveTraitEvaluator _defensivenessEvaluator;
        private readonly IMoveTraitEvaluator _enhancivenessEvaluator;
        private readonly IMoveTraitEvaluator _harmfulnessEvaluator;

        public MovesListTraitsEvaluator(
            IMoveTraitEvaluator destructivenessEvaluator,
            IMoveTraitEvaluator aggressivenessEvaluator,
            IMoveTraitEvaluator defensivenessEvaluator,
            IMoveTraitEvaluator enhancivenessEvaluator,
            IMoveTraitEvaluator harmfulnessEvaluator
        )
        {
            _destructivenessEvaluator = destructivenessEvaluator;
            _aggressivenessEvaluator = aggressivenessEvaluator;
            _defensivenessEvaluator = defensivenessEvaluator;
            _enhancivenessEvaluator = enhancivenessEvaluator;
            _harmfulnessEvaluator = harmfulnessEvaluator;
        }
        
        public Dictionary<AbstractMove, MoveTraitsValues> EvaluateTraitsForMoves(List<AbstractMove> moves)
        {
            var movesToTraits = new Dictionary<AbstractMove, MoveTraitsValues>();
            foreach (var move in moves)
            {
                var traitsValues = new MoveTraitsValues
                {
                    Destructiveness = _destructivenessEvaluator.EvaluateTrait(move),
                    Aggressiveness = _aggressivenessEvaluator.EvaluateTrait(move),
                    Defensiveness = _defensivenessEvaluator.EvaluateTrait(move),
                    Enhanciveness = _enhancivenessEvaluator.EvaluateTrait(move),
                    Harmfulness = _harmfulnessEvaluator.EvaluateTrait(move)
                };
                movesToTraits.Add(move, traitsValues);
            }
            return movesToTraits;
        }
    }
}