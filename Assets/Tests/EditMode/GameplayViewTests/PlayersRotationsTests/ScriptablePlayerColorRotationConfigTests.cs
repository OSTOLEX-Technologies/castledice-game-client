using System.Collections.Generic;
using NUnit.Framework;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersRotations;
using UnityEngine;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.PlayersRotationsTests
{
    public class ScriptablePlayerColorRotationConfigTests
    {
        [Test]
        [TestCase("blueRotation", PlayerColor.Blue)]
        [TestCase("redRotation", PlayerColor.Red)]
        public void GetRotation_ShouldReturnAppropriateRotation(string fieldName, PlayerColor color)
        {
            var expectedRotation = Random.insideUnitSphere;
            var config = ScriptableObject.CreateInstance<ScriptablePlayerColorRotationConfig>();
            SetPrivateFieldValue(expectedRotation, config, fieldName);
            
            var rotation = config.GetRotation(color);
            
            Assert.AreEqual(expectedRotation, rotation);
        }
    }
}