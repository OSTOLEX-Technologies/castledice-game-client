using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.GameSearchingTests.GameRequestingTests
{
    public class GameRequesterTest
    {
        [Test]
        public async Task RequestGameAsync_ShouldSendRequestGameDto_Once()
        {
            var senderMock = new Mock<IRequestGameDtoSender>();
            var dtoCreator = new RequesterBuilder
            {
                DtoSender = senderMock.Object
            }.Build();
            
            await dtoCreator.RequestGameAsync();
            
            senderMock.Verify(sender => sender.SendDto(It.IsAny<RequestGameDTO>()), Times.Once);
        }

        [Test]
        public async Task RequestGameAsync_ShouldSendDto_FromCreator()
        {
            var expectedDto = new RequestGameDTO("whatever");
            var senderMock = new Mock<IRequestGameDtoSender>();
            var dtoCreatorMock = new Mock<IRequestGameDtoCreator>();
            dtoCreatorMock.Setup(creator => creator.CreateDtoAsync()).ReturnsAsync(expectedDto);
            var dtoCreator = new RequesterBuilder
            {
                DtoSender = senderMock.Object,
                DtoCreator = dtoCreatorMock.Object
            }.Build();
            
            await dtoCreator.RequestGameAsync();
            
            senderMock.Verify(sender => sender.SendDto(expectedDto));
        }

        private class RequesterBuilder
        {
            public IRequestGameDtoCreator DtoCreator { get; set; } = Mock.Of<IRequestGameDtoCreator>();
            public IRequestGameDtoSender DtoSender { get; set; } = Mock.Of<IRequestGameDtoSender>();
            
            public GameRequester Build()
            {
                return new GameRequester(DtoCreator, DtoSender);
            }
        }
    }
}