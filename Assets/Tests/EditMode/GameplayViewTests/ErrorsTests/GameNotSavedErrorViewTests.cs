using Moq;
using NUnit.Framework;
using Src.GameplayView.Errors;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.ErrorsTests
{
    public class GameNotSavedErrorViewTests
    {
        [Test]
        public void ShowError_ShouldCallShowMethod_OnGivenErrorPopup()
        {
            var errorPopupMock = new Mock<IErrorPopup>();
            var gameCreationProcessScreen = new GameObject();
            var view = new GameNotSavedErrorView(errorPopupMock.Object, gameCreationProcessScreen);

            view.ShowError("test");

            errorPopupMock.Verify(popup => popup.Show(), Times.Once);
        }

        [Test]
        [TestCase("Test error message")]
        [TestCase("Some other error message")]
        [TestCase("Some other error message with numbers 1234567890")]
        public void ShowError_ShouldPassGivenErrorMessage_ToSetMessageMethodOnGivenErrorPopup(string message)
        {
            var errorPopupMock = new Mock<IErrorPopup>();
            var gameCreationProcessScreen = new GameObject();
            var view = new GameNotSavedErrorView(errorPopupMock.Object, gameCreationProcessScreen);

            view.ShowError(message);

            errorPopupMock.Verify(popup => popup.SetMessage(message), Times.Once);
        }

        [Test]
        public void HideGameCreationProcessScreen_ShouldSetActiveFalse_OnGivenGameCreationProcessScreen()
        {
            var errorPopupMock = new Mock<IErrorPopup>();
            var gameCreationProcessScreen = new GameObject();
            gameCreationProcessScreen.SetActive(true);
            var view = new GameNotSavedErrorView(errorPopupMock.Object, gameCreationProcessScreen);

            view.HideGameCreationProcessScreen();

            Assert.IsFalse(gameCreationProcessScreen.activeSelf);
        }
    }
}