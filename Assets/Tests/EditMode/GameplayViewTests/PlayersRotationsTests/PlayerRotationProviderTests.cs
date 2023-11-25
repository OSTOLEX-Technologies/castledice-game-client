using System.Collections.Generic;
using UnityEngine;
using Moq;
using NUnit.Framework;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersRotations;

namespace Tests.EditMode.GameplayViewTests.PlayersRotationsTests
{
    public class PlayerRotationProviderTests
    {
        [Test]
        [TestCase(PlayerColor.Blue)]
        [TestCase(PlayerColor.Red)]
        public void GetRotation_ShouldReturnRotation_AccordingToConfig(PlayerColor playerColor)
        {
            var configMock = new Mock<IPlayerRotationConfig>();
            var expectedRotation = Random.insideUnitSphere;
            configMock.Setup(config => config.GetRotations()).Returns(new Dictionary<PlayerColor, Vector3>{{playerColor, expectedRotation}});
            var playerRotationProvider = new PlayerRotationProvider(configMock.Object);
            
            var rotation = playerRotationProvider.GetRotation(playerColor);
            
            Assert.AreEqual(expectedRotation, rotation);
        }
    }
}