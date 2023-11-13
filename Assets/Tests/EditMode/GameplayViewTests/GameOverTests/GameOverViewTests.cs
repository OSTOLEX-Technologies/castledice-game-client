using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentCreation;
using Src.GameplayView.GameOver;
using UnityEngine;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.GameOverTests
{
    public class GameOverViewTests
    {
        [Test]
        public void ShowWin_ShouldSetBlueWinScreenActive_IfWinnerIsBlue()
        {
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            playerColorProviderMock.Setup(provider => provider.GetPlayerColor(It.IsAny<Player>())).Returns(PlayerColor.Blue);
            var bluePlayerWinScreen = new GameObject();
            var redPlayerWinScreen = new GameObject();
            var drawScreen = new GameObject();
            bluePlayerWinScreen.SetActive(false);
            redPlayerWinScreen.SetActive(false);
            drawScreen.SetActive(false);
            var view = new GameOverView(playerColorProviderMock.Object, bluePlayerWinScreen, redPlayerWinScreen, drawScreen);
            
            view.ShowWin(GetPlayer());
            
            Assert.IsTrue(bluePlayerWinScreen.activeSelf);
        }
        
        [Test]
        public void ShowWin_ShouldSetRedWinScreenActive_IfWinnerIsRed()
        {
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            playerColorProviderMock.Setup(provider => provider.GetPlayerColor(It.IsAny<Player>())).Returns(PlayerColor.Red);
            var bluePlayerWinScreen = new GameObject();
            var redPlayerWinScreen = new GameObject();
            var drawScreen = new GameObject();
            bluePlayerWinScreen.SetActive(false);
            redPlayerWinScreen.SetActive(false);
            drawScreen.SetActive(false);
            var view = new GameOverView(playerColorProviderMock.Object, bluePlayerWinScreen, redPlayerWinScreen, drawScreen);
            
            view.ShowWin(GetPlayer());
            
            Assert.IsTrue(redPlayerWinScreen.activeSelf);
        }
        
        [Test]
        public void ShowDraw_ShouldSetDrawScreenActive()
        {
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            var bluePlayerWinScreen = new GameObject();
            var redPlayerWinScreen = new GameObject();
            var drawScreen = new GameObject();
            bluePlayerWinScreen.SetActive(false);
            redPlayerWinScreen.SetActive(false);
            drawScreen.SetActive(false);
            var view = new GameOverView(playerColorProviderMock.Object, bluePlayerWinScreen, redPlayerWinScreen, drawScreen);
            
            view.ShowDraw();
            
            Assert.IsTrue(drawScreen.activeSelf);
        }
    }
}