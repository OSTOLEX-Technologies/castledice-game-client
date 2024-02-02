using System;
using System.Collections.Generic;
using NUnit.Framework;
using Src.GameplayView.PlayersColors;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.PlayersRotations.RotationsByOrder;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tests.EditMode.GameplayViewTests.PlayersRotationsTests.RotationsByOrderTests
{
    public class ScriptablePlayerOrderRotationConfigTests
    {
        [Test]
        [TestCaseSource(nameof(GetPlayerOrderNumbers))]
        public void GetRotation_ShouldReturnAppropriateRotation(int playerOrder)
        {
            var fieldName = GetFieldNameByOrder(playerOrder);
            var expectedRotation = Random.insideUnitSphere;
            var config = ScriptableObject.CreateInstance<ScriptablePlayerOrderRotationConfig>();
            SetPrivateFieldValue(expectedRotation, config, fieldName);
            
            var rotation = config.GetRotation(playerOrder);
            
            Assert.AreEqual(expectedRotation, rotation);
        }

        
        //As PlayerColor enum is related to potential number of players in one game, we can use it to get all the potential order numbers of players.
        public static IEnumerable<int> GetPlayerOrderNumbers()
        {
            var colors = Enum.GetValues(typeof(PlayerColor));
            var orderNumber = 1;
            foreach (var color in colors)
            {
                yield return orderNumber;
                orderNumber++;
            }
        }

        private static string GetFieldNameByOrder(int playerOrder)
        {
            switch (playerOrder)
            {
                case 1:
                    return "firstPlayerRotation";
                case 2:
                    return "secondPlayerRotation";
                default:
                    throw new ArgumentException("No field for player order " + playerOrder + ".");
            }
        }
    }
}