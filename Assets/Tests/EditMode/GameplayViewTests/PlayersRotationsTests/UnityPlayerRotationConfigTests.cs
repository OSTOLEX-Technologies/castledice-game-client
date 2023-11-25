using System.Collections.Generic;
using NUnit.Framework;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersRotations;
using UnityEngine;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.PlayersRotationsTests
{
    public class UnityPlayerRotationConfigTests
    {
        [Test]
        public void GetRotations_ShouldReturnDictionary_WithElementsBasedOnSerializedList()
        {
            var serializedList = new List<PlayerColorToRotation>();
            serializedList.Add(new PlayerColorToRotation {playerColor = PlayerColor.Blue, rotation = Random.insideUnitSphere});
            serializedList.Add(new PlayerColorToRotation {playerColor = PlayerColor.Red, rotation = Random.insideUnitSphere});
            var unityPlayerRotationConfig = ScriptableObject.CreateInstance<UnityPlayerRotationConfig>();
            SetPrivateFieldValue(serializedList, unityPlayerRotationConfig, "colorsToRotations");
            
            var rotations = unityPlayerRotationConfig.GetRotations();

            foreach (var playerNumberToRotation in serializedList)
            {
                Assert.AreEqual(playerNumberToRotation.rotation, rotations[playerNumberToRotation.playerColor]);
            }
        }
    }
}