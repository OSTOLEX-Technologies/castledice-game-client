using System;
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
            if (_dictionary.Count == 0)
            {
                throw new InvalidOperationException("Dictionary is empty!");
            }
            if (!_dictionary.ContainsKey(forPlayer))
            {
                throw new InvalidOperationException("Player with id " + forPlayer.Id + " is not in dictionary!");
            }
            return _dictionary[forPlayer];
        }
    }
}