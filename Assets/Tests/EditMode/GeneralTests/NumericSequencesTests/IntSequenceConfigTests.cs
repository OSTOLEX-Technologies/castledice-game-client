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
        
        [Test]
        public void Next_ShouldReturnDefaultValue_IfSequenceListIsEmpty()
        {
            var rnd = new Random();
            var expectedDefaultNumber = rnd.Next();
            var sequenceConfig = ScriptableObject.CreateInstance<IntSequenceConfig>();
            sequenceConfig.SetPrivateField(SequenceListFieldName, new List<int>());
            sequenceConfig.SetPrivateField(DefaultNumberFieldName, expectedDefaultNumber);
            
            var actualNumber = sequenceConfig.Next();
            
            Assert.AreEqual(expectedDefaultNumber, actualNumber);
        }

        [Test]
        public void Next_ShouldReturnFirstNumberFromSequenceList_IfCalledFirstTime()
        {
            var rnd = new Random();
            var expectedNumber = rnd.Next();
            var sequenceConfig = ScriptableObject.CreateInstance<IntSequenceConfig>();
            sequenceConfig.SetPrivateField(SequenceListFieldName, new List<int> {expectedNumber});
            sequenceConfig.SetPrivateField(DefaultNumberFieldName, rnd.Next());
            
            var actualNumber = sequenceConfig.Next();
            
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [Test]
        public void Next_ShouldReturnNextItemInSequenceList_ForEachCall()
        {
            var rnd = new Random();
            var callsNumber = rnd.Next(1, 10);
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
        public void Next_ShouldReturnDefaultNumber_IfSequenceListEnded()
        {
            var rnd = new Random();
            var sequenceList = GetRandomIntList(1);
            var defaultNumber = rnd.Next();
            var sequenceConfig = ScriptableObject.CreateInstance<IntSequenceConfig>();
            sequenceConfig.SetPrivateField(SequenceListFieldName, sequenceList);
            sequenceConfig.SetPrivateField(DefaultNumberFieldName, defaultNumber);
            
            sequenceConfig.Next();
            var actualNumber = sequenceConfig.Next();
            
            Assert.AreEqual(defaultNumber, actualNumber);
        }
    }
}