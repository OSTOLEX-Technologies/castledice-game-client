using Moq;
using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentCreation;
using Src.GameplayView.CurrentPlayer;
using UnityEngine;
using static Tests.ObjectCreationUtility;

namespace Tests.PlayMode.GameplayViewTests.CurrentPlayerTests
{
    public class CurrentPlayerViewTests
    {
        [Test]
        public void ShowCurrentPlayer_ShouldSetBluePlayerLabel_IfCurrentPlayerIsBlue()
        {
            var currentPlayer = GetPlayer();
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            playerColorProviderMock.Setup(provider => provider.GetPlayerColor(currentPlayer)).Returns(PlayerColor.Blue);
            var bluePlayerLabel = new GameObject();
            var redPlayerLabel = new GameObject();
            bluePlayerLabel.SetActive(false);
            redPlayerLabel.SetActive(false);
            var view = new CurrentPlayerView(playerColorProviderMock.Object, bluePlayerLabel, redPlayerLabel);
            
            view.ShowCurrentPlayer(currentPlayer);
            
            Assert.IsTrue(bluePlayerLabel.activeSelf);
        }

        [Test]
        public void ShowCurrentPlayer_ShouldSetRedPlayerLabel_IfCurrentPlayerIsRed()
        {
            var currentPlayer = GetPlayer();
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            playerColorProviderMock.Setup(provider => provider.GetPlayerColor(currentPlayer)).Returns(PlayerColor.Red);
            var bluePlayerLabel = new GameObject();
            var redPlayerLabel = new GameObject();
            bluePlayerLabel.SetActive(false);
            redPlayerLabel.SetActive(false);
            var view = new CurrentPlayerView(playerColorProviderMock.Object, bluePlayerLabel, redPlayerLabel);
            
            view.ShowCurrentPlayer(currentPlayer);
            
            Assert.IsTrue(redPlayerLabel.activeSelf);
        }
    }
}