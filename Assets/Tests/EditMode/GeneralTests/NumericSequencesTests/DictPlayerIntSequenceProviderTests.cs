using System;
using System.Collections.Generic;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.General.NumericSequences;

namespace Tests.EditMode.GeneralTests.NumericSequencesTests
{
    public class DictPlayerIntSequenceProviderTests
    {
        [Test]
        public void GetSequence_ShouldThrowInvalidOperationException_IfDictionaryIsEmpty()
        {
            var dictionary = new Dictionary<Player, IIntSequence>();
            var provider = new DictPlayerIntSequenceProvider(dictionary);

            Assert.Throws<InvalidOperationException>(() => provider.GetSequence(It.IsAny<Player>()));
        }

        [Test]
        public void GetSequence_ShouldReturnSequenceForPlayer_FromDictionary()
        {
            var expectedSequence = new Mock<IIntSequence>().Object;
            var player = GetPlayer();
            var dictionary = new Dictionary<Player, IIntSequence>
            {
                {player, expectedSequence}
            };
            var provider = new DictPlayerIntSequenceProvider(dictionary);
            
            var actualSequence = provider.GetSequence(player);
            
            Assert.AreSame(expectedSequence, actualSequence);
        }

        [Test]
        public void GetSequence_ShouldThrowInvalidOperationException_IfPlayerIsNotInDictionary()
        {
            var player = GetPlayer();
            var dictionary = new Dictionary<Player, IIntSequence>
            {
                { GetPlayer(), new Mock<IIntSequence>().Object }
            };
            var provider = new DictPlayerIntSequenceProvider(dictionary);
            
            Assert.Throws<InvalidOperationException>(() => provider.GetSequence(player));
        }
    }
}