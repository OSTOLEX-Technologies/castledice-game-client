using NUnit.Framework;
using Src.General.NumericSequences;
using UnityEngine;
using static Tests.Utils.ObjectCreationUtility;
using Random = System.Random;

namespace Tests.EditMode.GeneralTests.NumericSequencesTests
{
    public class IntSequenceConfigTests
    {
        private const string SequenceListFieldName = "sequence";
        private const string DefaultNumberFieldName = "defaultNumber";
        
        private readonly Random _rnd = new Random();

        [Test]
        public void SequenceProperty_ShouldReturnSequence_FromPrivateField()
        {
            var expectedSequence = GetRandomIntList(_rnd.Next(1, 10));
            var config = ScriptableObject.CreateInstance<IntSequenceConfig>();
            config.SetPrivateField(SequenceListFieldName, expectedSequence);
            
            var actualSequence = config.Sequence;
            
            Assert.AreEqual(expectedSequence, actualSequence);
        }
        
        [Test]
        public void DefaultNumberProperty_ShouldReturnDefaultNumber_FromPrivateField()
        {
            var expectedDefaultNumber = _rnd.Next();
            var config = ScriptableObject.CreateInstance<IntSequenceConfig>();
            config.SetPrivateField(DefaultNumberFieldName, expectedDefaultNumber);
            
            var actualDefaultNumber = config.DefaultNumber;
            
            Assert.AreEqual(expectedDefaultNumber, actualDefaultNumber);
        }
    }
}