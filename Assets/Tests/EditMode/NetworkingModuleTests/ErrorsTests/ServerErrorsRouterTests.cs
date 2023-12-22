using castledice_events_logic.ServerToClient;
using castledice_game_data_logic.Errors;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.Errors;
using Src.NetworkingModule.Errors;

namespace Tests.EditMode.NetworkingModuleTests.ErrorsTests
{
    public class ServerErrorsRouterTests
    {
        [Test]
        [TestCase("Test error message", ErrorType.GameNotSaved)]
        [TestCase("Some other error message", ErrorType.GameNotSaved)]
        //In this test appropriate presenter is presenter that was obtained from given presenters provider by passing error type to it. 
        public void AcceptServerErrorDTO_ShouldPassErrorMessage_ToAppropriatePresenter(string errorMessage, ErrorType errorType)
        {
            var presentersProviderMock = new Mock<IErrorPresentersProvider>();
            var presenterMock = new Mock<IErrorPresenter>();
            presentersProviderMock.Setup(provider => provider.GetPresenter(errorType)).Returns(presenterMock.Object);
            var serverErrorsRouter = new ServerErrorsRouter(presentersProviderMock.Object);
            var dto = new ServerErrorDTO(new ErrorData(errorType, errorMessage));
            
            serverErrorsRouter.AcceptServerErrorDTO(dto);
            
            presenterMock.Verify(presenter => presenter.ShowError(errorMessage), Times.Once);
        }
    }
}