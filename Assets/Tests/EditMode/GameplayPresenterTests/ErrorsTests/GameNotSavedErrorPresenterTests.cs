using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.Errors;
using Src.GameplayView.Errors;

namespace Tests.EditMode.GameplayPresenterTests.ErrorsTests
{
    public class GameNotSavedErrorPresenterTests
    {
        [Test]
        public void ShowError_ShouldCallHideGameCreationScreen_OnGivenView()
        {
            var viewMock = new Mock<IGameNotSavedErrorView>();
            var presenter = new GameNotSavedErrorPresenter(viewMock.Object); 
            
            presenter.ShowError("test");
            
            viewMock.Verify(view => view.HideGameCreationProcessScreen(), Times.Once);
        }

        [Test]
        [TestCase("Test error message")]
        [TestCase("Some other error message")]
        [TestCase("Some other error message with numbers 1234567890")]
        public void ShowError_ShouldPassGivenErrorMessage_ToShowErrorMethodOnGivenView(string errorMessage)
        {
            var viewMock = new Mock<IGameNotSavedErrorView>();
            var presenter = new GameNotSavedErrorPresenter(viewMock.Object);
            
            presenter.ShowError(errorMessage);
            
            viewMock.Verify(view => view.ShowError(errorMessage), Times.Once);
        }
    }
}