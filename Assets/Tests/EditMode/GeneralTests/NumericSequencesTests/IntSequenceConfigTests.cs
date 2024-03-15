using System.Collections.Generic;
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
        public void Next_ShouldReturnDrnDefaultValue_IfSequenceListIsEmpty()
        {
            var expectedDefaultNumber =_rnd.Next();
            var sequenceConfig = ScriptableObject.CreateInstance<IntSequenceConfig>();
            sequenceConfig.SetPrivateField(SequenceListFieldName, new List<int>());
            sequenceConfig.SetPrivateField(DefaultNumberFieldName, expectedDefaultNumber);
            
            var actualNumber = sequenceConfig.Next();
            
            Assert.AreEqual(expectedDefaultNumber, actualNumber);
        }

        [Test]
        public void Next_ShouldReturnFirstNumberFromSequenceList_IfCalledFirstTime()
        {
            var expectedNumber =_rnd.Next();
            var sequenceConfig = ScriptableObject.CreateInstance<IntSequenceConfig>();
            sequenceConfig.SetPrivateField(SequenceListFieldName, new List<int> {expectedNumber});
            sequenceConfig.SetPrivateField(DefaultNumberFieldName,_rnd.Next());
            
            var actualNumber = sequenceConfig.Next();
            
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [Test]
        public void Next_ShouldReturnNextItemInSequenceList_ForEachCall()
        {
            var callsNumber =_rnd.Next(1, 10);
            var sequenceList = GetRandomIntList(callsNumber);
            var expectedNumber = sequenceList[callsNumber-1];
            var sequenceConfig = ScriptableObject.CreateInstance<IntSequenceConfig>();
            sequenceConfig.SetPrivateField(SequenceListFieldName, sequenceList);
            
            for (var i = 1; i < callsNumber; i++)
            {
                sequenceConfig.Next();
            }
            var actualNumber = sequenceConfig.Next();
            
            Assert.AreEqual(expectedNumber, actualNumber);
        }
        
        [Test]
        public void Next_ShouldReturnDrnDefaultNumber_IfSequenceListEnded()
        {
            var sequenceList = GetRandomIntList(1);
            var defaultNumber =_rnd.Next();
            var sequenceConfig = ScriptableObject.CreateInstance<IntSequenceConfig>();
            sequenceConfig.SetPrivateField(SequenceListFieldName, sequenceList);
            sequenceConfig.SetPrivateField(DefaultNumberFieldName, defaultNumber);
            
            sequenceConfig.Next();
            var actualNumber = sequenceConfig.Next();
            
            Assert.AreEqual(defaultNumber, actualNumber);
        }
    }
}