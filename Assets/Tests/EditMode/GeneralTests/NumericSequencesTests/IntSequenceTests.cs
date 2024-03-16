using System.Collections.Generic;
using NUnit.Framework;
using Src.General.NumericSequences;
using static Tests.Utils.ObjectCreationUtility;
using Random = System.Random;

namespace Tests.EditMode.GeneralTests.NumericSequencesTests
{
    public class IntSequenceTests
    {
        private readonly Random _rnd = new Random();
        
        [Test]
        public void Next_ShouldReturnDrnDefaultValue_IfSequenceListIsEmpty()
        {
            var expectedDefaultNumber =_rnd.Next();
            var sequence = new IntSequence(new List<int>(), expectedDefaultNumber);
            
            var actualNumber = sequence.Next();
            
            Assert.AreEqual(expectedDefaultNumber, actualNumber);
        }

        [Test]
        public void Next_ShouldReturnFirstNumberFromSequenceList_IfCalledFirstTime()
        {
            var expectedNumber =_rnd.Next();
            var sequence = new IntSequence(new List<int> {expectedNumber}, _rnd.Next());
            
            var actualNumber = sequence.Next();
            
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [Test]
        public void Next_ShouldReturnNextItemInSequenceList_ForEachCall()
        {
            var callsNumber =_rnd.Next(1, 10);
            var sequenceList = GetRandomIntList(callsNumber);
            var expectedNumber = sequenceList[callsNumber-1];
            var sequence = new IntSequence(sequenceList, _rnd.Next());
            
            for (var i = 1; i < callsNumber; i++)
            {
                sequence.Next();
            }
            var actualNumber = sequence.Next();
            
            Assert.AreEqual(expectedNumber, actualNumber);
        }
        
        [Test]
        public void Next_ShouldReturnDrnDefaultNumber_IfSequenceListEnded()
        {
            var sequenceList = GetRandomIntList(1);
            var defaultNumber =_rnd.Next();
            var sequence = new IntSequence(sequenceList, defaultNumber);
            
            sequence.Next();
            var actualNumber = sequence.Next();
            
            Assert.AreEqual(defaultNumber, actualNumber);
        }
    }
}