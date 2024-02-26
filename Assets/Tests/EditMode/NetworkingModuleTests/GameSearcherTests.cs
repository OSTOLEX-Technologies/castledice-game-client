using System.Threading.Tasks;
using castledice_events_logic.ServerToClient;
using Moq;
using NUnit.Framework;
using Riptide;
using Src.GameplayPresenter.GameCreation;
using Src.NetworkingModule;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.NetworkingModuleTests
{
    public class GameSearcherTests
    {
        private static bool[] CancelGameResultValues = { true, false };
        
        [Test]
        public void SearchGameAsync_ShouldCallSendMethod_OnMessageSender()
        {
            var senderMock = new Mock<IMessageSender>();
            var searcher = new GameSearcher(senderMock.Object);
            
            searcher.SearchGameAsync("whatever");
            
            senderMock.Verify(x => x.Send(It.IsAny<Message>()), Times.Once);
        }

        [Test]
        public void CancelGameSearchAsync_ShouldCallSendMethod_OnMessageSender()
        {
            var senderMock = new Mock<IMessageSender>();
            var searcher = new GameSearcher(senderMock.Object);
            
            searcher.CancelGameSearchAsync("whatever");
            
            senderMock.Verify(x => x.Send(It.IsAny<Message>()), Times.Once);            
        }


        [Test]
        public async Task SearchGameAsync_ShouldReturnResultWithStartData_IfAcceptedCreateGameDTO()
        {
            var gameStartData = GetGameStartData();
            var dto = new CreateGameDTO(gameStartData);
            var senderMock = new Mock<IMessageSender>();
            var searcher = new GameSearcher(senderMock.Object);
            
            var searchTask = searcher.SearchGameAsync("whatever");
            searcher.AcceptCreateGameDTO(dto);
            var result = await searchTask;
            var dataFromResult = result.GameStartData;
            
            Assert.AreEqual(gameStartData, dataFromResult);
        }

        [Test]
        public async Task SearchGameAsync_ShouldReturnResultWithSuccessStatus_IfAcceptedCreateGameDTO()
        {
            var dto = new CreateGameDTO(GetGameStartData());
            var senderMock = new Mock<IMessageSender>();
            var searcher = new GameSearcher(senderMock.Object);
            
            var searchTask = searcher.SearchGameAsync("whatever");
            searcher.AcceptCreateGameDTO(dto);
            var result = await searchTask;
            var resultStatus = result.Status;
            
            Assert.AreEqual(GameSearchResult.ResultStatus.Success, resultStatus);
        }

        [Test]
        public async Task CancelGameSearchAsync_ShouldReturnBool_FromAcceptedCancelGameResultDTO(
            [ValueSource(nameof(CancelGameResultValues))]bool value)
        {
            var dto = new CancelGameResultDTO(value, 1);
            var senderMock = new Mock<IMessageSender>();
            var searcher = new GameSearcher(senderMock.Object);
            
            var cancelTask = searcher.CancelGameSearchAsync("whatever");
            searcher.AcceptCancelGameResultDTO(dto);
            var result = await cancelTask;
            
            Assert.AreEqual(value, result);            
        }

        [Test]
        public async Task SearchGameAsync_ShouldReturnResultWithCanceledStatus_IfAcceptedTrueCancelResult()
        {
            var dto = new CancelGameResultDTO(true, 1);
            var senderMock = new Mock<IMessageSender>();
            var searcher = new GameSearcher(senderMock.Object);
            
            var searchTask = searcher.SearchGameAsync("whatever");
            searcher.AcceptCancelGameResultDTO(dto);
            var result = await searchTask;
            var resultStatus = result.Status;
            
            Assert.AreEqual(GameSearchResult.ResultStatus.Canceled, resultStatus);            
        }
    }
}