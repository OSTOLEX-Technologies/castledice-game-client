using Moq;
using NUnit.Framework;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersRotations.RotationsByColor;
using UnityEngine;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.PlayersRotationsTests.RotationsByColorTests
{
    public class PlayerColorRotationProviderTests
    {
        [Test]
        [TestCase(PlayerColor.Blue)]
        [TestCase(PlayerColor.Red)]
        public void GetRotation_ShouldReturnRotation_AccordingToConfig(PlayerColor playerColor)
        {
            var player = GetPlayer();
            var configMock = new Mock<IPlayerColorRotationConfig>();
            var colorProviderMock = new Mock<IPlayerColorProvider>();
            colorProviderMock.Setup(provider => provider.GetPlayerColor(player)).Returns(playerColor);
            var expectedRotation = Random.insideUnitSphere;
            configMock.Setup(config => config.GetRotation(playerColor)).Returns(expectedRotation);
            var playerRotationProvider = new PlayerColorRotationProvider(configMock.Object, colorProviderMock.Object);
            
            var rotation = playerRotationProvider.GetRotation(player);
            
            Assert.AreEqual(expectedRotation, rotation);
        }
    }
}