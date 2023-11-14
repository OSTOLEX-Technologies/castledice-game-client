using castledice_game_data_logic.Errors;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.Errors;
using Src.GameplayView.Errors;

namespace Tests.EditMode.GameplayPresenterTests.ErrorsTests
{
    public class ErrorPresentersProviderTests
    {
        [Test]
        public void GetPresenter_ShouldReturnGameNotSavedErrorPresenter_WhenGivenGameNotSavedErrorType()
        {
            var expectedPresenter = new GameNotSavedErrorPresenter(new Mock<IGameNotSavedErrorView>().Object);
            var provider = new ErrorPresentersProvider(expectedPresenter);
            
            var presenter = provider.GetPresenter(ErrorType.GameNotSaved);
            
            Assert.AreSame(expectedPresenter, presenter);
        }
    }
}