using System.Collections.Generic;
using System.Linq;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.MoveSearchers.TraitBasedSearchers
{
    public class MovesTraitsNormalizer : IMovesTraitsNormalizer
    {
        public Dictionary<AbstractMove, MoveTraitsValues> NormalizeTraits(Dictionary<AbstractMove, MoveTraitsValues> movesToTraits)
        {
            var normalizedMovesToTraits = new Dictionary<AbstractMove, MoveTraitsValues>();
            var normalizedDestructiveness = NormalizeValues(movesToTraits.Select(move => move.Value.Destructiveness)).ToArray();
            var normalizedAggressiveness = NormalizeValues(movesToTraits.Select(move => move.Value.Aggressiveness)).ToArray();
            var normalizedDefensiveness = NormalizeValues(movesToTraits.Select(move => move.Value.Defensiveness)).ToArray();
            var normalizedEnhanciveness = NormalizeValues(movesToTraits.Select(move => move.Value.Enhanciveness)).ToArray();
            var normalizedHarmfulness = NormalizeValues(movesToTraits.Select(move => move.Value.Harmfulness)).ToArray();
            for (int i = 0; i < movesToTraits.Count; i++)
            {
                var move = movesToTraits.ElementAt(i).Key;
                var normalizedTrait = new MoveTraitsValues
                {
                    Destructiveness = normalizedDestructiveness[i],
                    Aggressiveness = normalizedAggressiveness[i],
                    Defensiveness = normalizedDefensiveness[i],
                    Enhanciveness = normalizedEnhanciveness[i],
                    Harmfulness = normalizedHarmfulness[i]
                };
                normalizedMovesToTraits.Add(move, normalizedTrait);
            }
            return normalizedMovesToTraits;
        }

        private static IEnumerable<float> NormalizeValues(IEnumerable<float> values)
        {
            var enumerable = values.ToList();
            var max = enumerable.Max();
            var min = enumerable.Min();
            if (max == min)
            {
                return enumerable.Select(value => 0f);
            }
            return enumerable.Select(value => (value - min) / (max - min));
        }
    }
}