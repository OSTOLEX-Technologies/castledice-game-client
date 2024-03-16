using castledice_game_logic;
using Src.General;
using Src.General.NumericSequences;
using Src.Tutorial.ActionPointsGiving;

namespace Src.Tutorial
{
    public class SequenceActionPointsGenerator : IActionPointsGenerator
    {
        private readonly IPlayerIntSequenceProvider _sequenceProvider;

        public SequenceActionPointsGenerator(IPlayerIntSequenceProvider sequenceProvider)
        {
            _sequenceProvider = sequenceProvider;
        }
        public int GetActionPoints(Player forPlayer)
        {
            var sequence = _sequenceProvider.GetSequence(forPlayer);
            return sequence.Next();
        }
    }
}