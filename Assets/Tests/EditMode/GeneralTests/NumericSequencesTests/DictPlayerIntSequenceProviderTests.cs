using System;
using System.Collections.Generic;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
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
    }
}