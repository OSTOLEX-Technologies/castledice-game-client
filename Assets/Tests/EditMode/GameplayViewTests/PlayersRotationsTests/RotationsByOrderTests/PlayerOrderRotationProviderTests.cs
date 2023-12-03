using System;
using System.Collections.Generic;
using static Tests.ObjectCreationUtility;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersNumbers;
using Src.GameplayView.PlayersRotations.RotationsByOrder;
using Random = UnityEngine.Random;

namespace Tests.EditMode.GameplayViewTests.PlayersRotationsTests.RotationsByOrderTests
{
    public class PlayerOrderRotationProviderTests
    {
        
        [Test]
        public void GetRotation_ShouldReturnRotation_AccordingToConfig()
        {
            var player = GetPlayer();
            var playerNumber = Random.Range(1, 100);
            var playerNumberProviderMock = new Mock<IPlayerNumberProvider>();
            playerNumberProviderMock.Setup(provider => provider.GetPlayerNumber(player)).Returns(playerNumber);
            var configMock = new Mock<IPlayerOrderRotationConfig>();
            var expectedRotation = Random.insideUnitSphere;
            configMock.Setup(config => config.GetRotation(playerNumber)).Returns(expectedRotation);
            var playerRotationProvider = new PlayerOrderRotationProvider(configMock.Object, playerNumberProviderMock.Object);
            
            var rotation = playerRotationProvider.GetRotation(player);
            
            Assert.AreEqual(expectedRotation, rotation);
        }
    }
}