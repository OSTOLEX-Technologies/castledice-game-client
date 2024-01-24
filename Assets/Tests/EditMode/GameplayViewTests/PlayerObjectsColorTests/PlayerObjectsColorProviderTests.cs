using Moq;
using NUnit.Framework;
using Src.GameplayView.PlayerObjectsColor;
using Src.GameplayView.PlayersColors;
using UnityEngine;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.PlayerObjectsColorTests
{
    public class PlayerObjectsColorProviderTests
    {
        [Test]
        public void GetColor_ShouldReturnColor_AccordingToConfig()
        {
            var expectedColor = Random.ColorHSV();
            var player = GetPlayer();
            var playerColor = (PlayerColor)Random.Range(0, 2);
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            playerColorProviderMock.Setup(x => x.GetPlayerColor(player)).Returns(playerColor);
            var configMock = new Mock<IPlayerObjectsColorConfig>();
            configMock.Setup(x => x.GetColor(playerColor)).Returns(expectedColor);
            var playerObjectsColorProvider = new PlayerObjectsColorProvider(configMock.Object, playerColorProviderMock.Object);
            
            var actualColor = playerObjectsColorProvider.GetColor(player);
            
            Assert.That(actualColor.r, Is.EqualTo(expectedColor.r).Within(0.01f));
            Assert.That(actualColor.g, Is.EqualTo(expectedColor.g).Within(0.01f));
            Assert.That(actualColor.b, Is.EqualTo(expectedColor.b).Within(0.01f));
            Assert.That(actualColor.a, Is.EqualTo(expectedColor.a).Within(0.01f));
        }
    }
}