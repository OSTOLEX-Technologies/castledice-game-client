using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.GameSearchingTests.CancelRequestingTests
{
    public class GameCancelRequesterTests
    {
        [Test]
        public async Task CancelGameAsync_ShouldSendDto_Once()
        {
            var senderMock = new Mock<ICancelGameDtoSender>();
            var dtoCreator = new RequesterBuilder
            {
                DtoSender = senderMock.Object
            }.Build();
            
            await dtoCreator.RequestCancelAsync();
            
            senderMock.Verify(sender => sender.SendDto(It.IsAny<CancelGameDTO>()), Times.Once);
        }
        
        [Test]
        public async Task CancelGameAsync_ShouldSendDto_FromCreator()
        {
            var expectedDto = new CancelGameDTO("whatever");
            var senderMock = new Mock<ICancelGameDtoSender>();
            var dtoCreatorMock = new Mock<ICancelGameDtoCreator>();
            dtoCreatorMock.Setup(creator => creator.CreateDtoAsync()).ReturnsAsync(expectedDto);
            var dtoCreator = new RequesterBuilder
            {
                DtoSender = senderMock.Object,
                DtoCreator = dtoCreatorMock.Object
            }.Build();
            
            await dtoCreator.RequestCancelAsync();
            
            senderMock.Verify(sender => sender.SendDto(expectedDto));
        }

        private class RequesterBuilder
        {
            public ICancelGameDtoCreator DtoCreator { get; set; } = Mock.Of<ICancelGameDtoCreator>();
            public ICancelGameDtoSender DtoSender { get; set; } = Mock.Of<ICancelGameDtoSender>();
            
            public GameCancelRequester Build()
            {
                return new GameCancelRequester(DtoCreator, DtoSender);
            }
        }
    }
}