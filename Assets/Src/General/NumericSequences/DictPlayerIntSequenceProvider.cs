using System.Collections.Generic;
using castledice_game_logic;
using Dynamitey.DynamicObjects;

namespace Src.General.NumericSequences
{
    public class DictPlayerIntSequenceProvider : IPlayerIntSequenceProvider
    {
        private readonly Dictionary<Player, IIntSequence> _dictionary;

        public DictPlayerIntSequenceProvider(Dictionary<Player, IIntSequence> dictionary)
        {
            _dictionary = dictionary;
        }
        
        public IIntSequence GetSequence(Player forPlayer)
        {
            throw new System.NotImplementedException();
        }
    }
}