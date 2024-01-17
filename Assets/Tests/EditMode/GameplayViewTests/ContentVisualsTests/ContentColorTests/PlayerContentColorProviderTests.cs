using Moq;
using NUnit.Framework;
using Src.GameplayView.ContentVisuals.ContentColor;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests.ContentColorTests
{
    public class PlayerContentColorProviderTests
    {
        [Test]
        public void GetContentColor_ShouldReturnColor_AccordingToConfig()
        {
            var expectedColor = Random.ColorHSV();
            var player = GetPlayer();
            var playerColor = (PlayerColor)Random.Range(0, 2);
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            playerColorProviderMock.Setup(x => x.GetPlayerColor(player)).Returns(playerColor);
            var configMock = new Mock<IPlayerContentColorConfig>();
            configMock.Setup(x => x.GetColor(playerColor)).Returns(expectedColor);
            var playerContentColorProvider = new PlayerContentColorProvider(configMock.Object, playerColorProviderMock.Object);
            
            var actualColor = playerContentColorProvider.GetContentColor(player);
            
            Assert.That(actualColor.r, Is.EqualTo(expectedColor.r).Within(0.01f));
            Assert.That(actualColor.g, Is.EqualTo(expectedColor.g).Within(0.01f));
            Assert.That(actualColor.b, Is.EqualTo(expectedColor.b).Within(0.01f));
            Assert.That(actualColor.a, Is.EqualTo(expectedColor.a).Within(0.01f));
        }
    }
}