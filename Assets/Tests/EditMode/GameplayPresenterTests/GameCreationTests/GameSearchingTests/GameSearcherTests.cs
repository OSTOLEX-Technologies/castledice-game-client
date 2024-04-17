using System.Threading.Tasks;
using castledice_events_logic.ServerToClient;
using castledice_game_data_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.GameSearching;
using Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting;
using Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting;
using Src.General.PlayerInitialization;
using Src.NetworkingModule;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.GameSearchingTests
{
    public class GameSearcherTests
    {
        [Test]
        public async Task SearchAsync_ShouldRequestGame_Once()
        {
            var requesterMock = new Mock<IGameRequester>();
            var searcher = new SearcherBuilder()
            {
                Requester = requesterMock.Object
            }.Build();
            
            await searcher.SearchAsync();
            
            requesterMock.Verify(x => x.RequestGameAsync(), Times.Once);
        }

        [Test]
        public async Task SearchAsync_ShouldInvokeSearchFailed_WithNotConnectedReason_IfClientNotConnected()
        {
            var clientWrapperMock = new Mock<IClientWrapper>();
            clientWrapperMock.Setup(x => x.IsConnected).Returns(false);
            var reason = SearchFailReason.NotInitialized;
            var searcher = new SearcherBuilder
            {
                ClientWrapper = clientWrapperMock.Object
            }.Build();
            searcher.SearchFailed += r => reason = r;
            
            await searcher.SearchAsync();
         
            Assert.AreEqual(SearchFailReason.NotConnected, reason);
        }
        
        [Test]
        public async Task SearchAsync_ShouldNotRequestGame_IfClientNotConnected()
        {
            var requesterMock = new Mock<IGameRequester>();
            var clientWrapperMock = new Mock<IClientWrapper>();
            clientWrapperMock.Setup(x => x.IsConnected).Returns(false);
            var searcher = new SearcherBuilder
            {
                Requester = requesterMock.Object,
                ClientWrapper = clientWrapperMock.Object
            }.Build();
            
            await searcher.SearchAsync();
            
            requesterMock.Verify(x => x.RequestGameAsync(), Times.Never);
        }

        [Test]
        public async Task SearchAsync_ShouldInvokeSearchFailed_WithNotInitializedReason_IfPlayerNotInitialized()
        {
            var initializationProviderMock = new Mock<IPlayerInitializationProvider>();
            initializationProviderMock.Setup(x => x.Initialized).Returns(false);
            var reason = SearchFailReason.NotConnected;
            var searcher = new SearcherBuilder
            {
                InitializationProvider = initializationProviderMock.Object
            }.Build();
            searcher.SearchFailed += r => reason = r;
            
            await searcher.SearchAsync();
            
            Assert.AreEqual(SearchFailReason.NotInitialized, reason);
        }
        
        [Test]
        public async Task SearchAsync_ShouldNotRequestGame_IfPlayerNotInitialized()
        {
            var requesterMock = new Mock<IGameRequester>();
            var initializationProviderMock = new Mock<IPlayerInitializationProvider>();
            initializationProviderMock.Setup(x => x.Initialized).Returns(false);
            var searcher = new SearcherBuilder
            {
                Requester = requesterMock.Object,
                InitializationProvider = initializationProviderMock.Object
            }.Build();
            
            await searcher.SearchAsync();
            
            requesterMock.Verify(x => x.RequestGameAsync(), Times.Never);
        }
        
        [Test]
        public async Task CancelAsync_ShouldRequestGameCancel_Once()
        {
            var cancelRequesterMock = new Mock<IGameCancelRequester>();
            var searcher = new SearcherBuilder
            {
                CancelRequester = cancelRequesterMock.Object
            }.Build();
            
            await searcher.CancelAsync();
            
            cancelRequesterMock.Verify(x => x.RequestCancelAsync(), Times.Once);
        }
        
        [Test]
        public async Task CancelAsync_ShouldNotRequestGameCancel_IfClientNotConnected()
        {
            var cancelRequesterMock = new Mock<IGameCancelRequester>();
            var clientWrapperMock = new Mock<IClientWrapper>();
            clientWrapperMock.Setup(x => x.IsConnected).Returns(false);
            var searcher = new SearcherBuilder
            {
                CancelRequester = cancelRequesterMock.Object,
                ClientWrapper = clientWrapperMock.Object
            }.Build();
            
            await searcher.CancelAsync();
            
            cancelRequesterMock.Verify(x => x.RequestCancelAsync(), Times.Never);
        }
        
        [Test]
        public async Task CancelAsync_ShouldNotRequestGameCancel_IfPlayerNotInitialized()
        {
            var cancelRequesterMock = new Mock<IGameCancelRequester>();
            var initializationProviderMock = new Mock<IPlayerInitializationProvider>();
            initializationProviderMock.Setup(x => x.Initialized).Returns(false);
            var searcher = new SearcherBuilder
            {
                CancelRequester = cancelRequesterMock.Object,
                InitializationProvider = initializationProviderMock.Object
            }.Build();
            
            await searcher.CancelAsync();
            
            cancelRequesterMock.Verify(x => x.RequestCancelAsync(), Times.Never);
        }

        [Test]
        public void AcceptCreateGameDto_ShouldInvokeGameFound_WithGameStartData_FromDto()
        {
            var gameStartData = GetGameStartData();
            var dto = new CreateGameDTO(gameStartData);
            GameStartData receivedData = null;
            var searcher = new SearcherBuilder().Build();
            searcher.GameFound += data => receivedData = data;
            
            searcher.AcceptCreateGameDto(dto);
            
            Assert.AreEqual(gameStartData, receivedData);
        }

        [Test]
        public void AcceptCancelGameResultDto_ShouldInvokeCancellationApproved_IfIsCanceledIsTrueInDto()
        {
            var dto = new CancelGameResultDTO(true, 1);
            var eventInvoked = false;
            var searcher = new SearcherBuilder().Build();
            searcher.CancellationApproved += () => eventInvoked = true;
            
            searcher.AcceptCancelGameResultDto(dto);
            
            Assert.IsTrue(eventInvoked);
        }
        
        [Test]
        public void AcceptCancelGameResultDto_ShouldNotInvokeCancellationApproved_IfIsCanceledIsFalseInDto()
        {
            var dto = new CancelGameResultDTO(false, 1);
            var eventInvoked = false;
            var searcher = new SearcherBuilder().Build();
            searcher.CancellationApproved += () => eventInvoked = true;
            
            searcher.AcceptCancelGameResultDto(dto);
            
            Assert.IsFalse(eventInvoked);
        }

        private class SearcherBuilder
        {
            public IGameRequester Requester { get; set; }
            public IGameCancelRequester CancelRequester { get; set; }
            public IClientWrapper ClientWrapper { get; set; }
            public IPlayerInitializationProvider InitializationProvider { get; set; }

            public SearcherBuilder()
            {
                Requester = new Mock<IGameRequester>().Object;
                CancelRequester = new Mock<IGameCancelRequester>().Object;
                var clientWrapperMock = new Mock<IClientWrapper>();
                clientWrapperMock.Setup(x => x.IsConnected).Returns(true);
                ClientWrapper = clientWrapperMock.Object;
                var initializationProviderMock = new Mock<IPlayerInitializationProvider>();
                initializationProviderMock.Setup(x => x.Initialized).Returns(true);
                InitializationProvider = initializationProviderMock.Object;
            }
            
            public GameSearcher Build()
            {
                return new GameSearcher(Requester, CancelRequester, ClientWrapper, InitializationProvider);
            }
        }
    }
}