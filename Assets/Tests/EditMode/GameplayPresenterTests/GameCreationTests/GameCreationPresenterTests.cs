using System;
using System.Threading.Tasks;
using castledice_game_data_logic;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.Auth.TokenProviders;
using Src.Caching;
using Src.GameplayPresenter;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayView.GameCreation;
using Src.NetworkingModule;
using Tests.Utils.Mocks;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests
{
    public class GameCreationPresenterTests
    {
        private class TestGameCreationView : IGameCreationView
        {
            public void ShowCreationProcessScreen()
            {
            }

            public void HideCreationProcessScreen()
            {
            }

            public void ShowCancelationMessage(string message)
            {
            }

            public void HideCancelationMessage()
            {
            }

            public void ShowNoConnectionMessage()
            {
                throw new NotImplementedException();
            }

            public void ChooseCreateGame()
            {
                CreateGameChosen?.Invoke(this, EventArgs.Empty);
            }

            public void ChooseCancelGame()
            {
                CancelCreationChosen?.Invoke(this, EventArgs.Empty);
            }

            public event EventHandler CancelCreationChosen;
            public event EventHandler CreateGameChosen;
        }
    
        [Test]
        public void CreateGame_ShouldCallShowCreationProcessScreen_BeforeGameFound()
        {
            var viewMock = new Mock<IGameCreationView>();
            var presenter = new GameCreationPresenterBuilder
            {
                GameCreationView = viewMock.Object,
                AccessTokenProvider = new Mock<IAccessTokenProvider>().Object
            }.Build();

            presenter.CreateGame();

            viewMock.Verify(v => v.ShowCreationProcessScreen(), Times.Once);
        }
        

        [Test]
        public async Task CreateGame_ShouldRegisterGameInstance_IntoSingleton()
        {
            var expectedGame = GetGame();
            var gameCreatorMock = new Mock<IGameCreator>();
            gameCreatorMock.Setup(g => g.CreateGame(It.IsAny<GameStartData>())).Returns(expectedGame);
            var presenter = new GameCreationPresenterBuilder
            {
                GameCreator = gameCreatorMock.Object,
                AccessTokenProvider = new Mock<IAccessTokenProvider>().Object
            }.Build();

            await presenter.CreateGame();
            var actualGame = Singleton<Game>.Instance;
            
            Assert.AreSame(expectedGame, actualGame);
        }

        [Test]
        public async Task CreateGame_ShouldRegisterGameStartData_IntoSingleton()
        {
            var expectedGameStartData = GetGameStartData();
            var gameSearcherMock = new Mock<IGameSearcher>();
            gameSearcherMock.Setup(g => g.SearchGameAsync(It.IsAny<string>())).ReturnsAsync(new GameSearchResult
            {
                Status = GameSearchResult.ResultStatus.Success,
                GameStartData = expectedGameStartData
            });
            var presenter = new GameCreationPresenterBuilder
            {
                GameSearcher = gameSearcherMock.Object,
                AccessTokenProvider = new Mock<IAccessTokenProvider>().Object
            }.Build();
        
            await presenter.CreateGame();
            var actualGameStartData = Singleton<GameStartData>.Instance;
        
            Assert.AreSame(expectedGameStartData, actualGameStartData);
        }

        [Test]
        public async Task CreateGame_ShouldInvokeGameCreatedEvent_IfGameCreated()
        {
            var gameSearcherMock = new Mock<IGameSearcher>();
            gameSearcherMock.Setup(g => g.SearchGameAsync(It.IsAny<string>())).ReturnsAsync(new GameSearchResult
            {
                Status = GameSearchResult.ResultStatus.Success,
                GameStartData = GetGameStartData()
            });
            var presenter = new GameCreationPresenterBuilder
            {
                GameSearcher = gameSearcherMock.Object,
                AccessTokenProvider = new Mock<IAccessTokenProvider>().Object
            }.Build();
            var gameCreatedEventInvoked = false;
            presenter.GameCreated += (sender, args) => gameCreatedEventInvoked = true;
        
            await presenter.CreateGame();
        
            Assert.IsTrue(gameCreatedEventInvoked);
        }

        [Test]
        public async Task CreateGame_ShouldHideLoadingScreenAndCancelingMessage_IfGameCreationIsCanceled()
        {
            var viewMock = new Mock<IGameCreationView>();
            var gameSearcher = new GameSearcherMock
            {
                CancelTimeMilliseconds = 0,
                SearchTimeMilliseconds = 100,
                CanBeCanceled = true
            };
            var presenter = new GameCreationPresenterBuilder
            {
                GameCreationView = viewMock.Object,
                GameSearcher = gameSearcher,
                AccessTokenProvider = new Mock<IAccessTokenProvider>().Object
            }.Build();

            var gameCreationOperation = presenter.CreateGame();
            var cancelationOperation = presenter.CancelGame();
            await gameCreationOperation;
            await cancelationOperation;

            viewMock.Verify(v => v.HideCancelationMessage(), Times.Once);
            viewMock.Verify(v => v.HideCreationProcessScreen(), Times.Once);
        }

        [Test]
        public void CancelGame_ShouldCallShowCancelationMessage_IfGameCreationInProcess()
        {
            var viewMock = new Mock<IGameCreationView>();
            var gameSearcher = new GameSearcherMock
            {
                SearchTimeMilliseconds = 100,
                CancelTimeMilliseconds = 0,
                CanBeCanceled = true
            };
            var presenter = new GameCreationPresenterBuilder
            {
                GameCreationView = viewMock.Object,
                GameSearcher = gameSearcher,
                AccessTokenProvider = new Mock<IAccessTokenProvider>().Object
            }.Build();

            presenter.CreateGame();
            presenter.CancelGame();

            viewMock.Verify(v => v.ShowCancelationMessage(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CancelGame_ShouldNotCallShowCancelationMessage_IfGameCreationIsNotInProcess()
        {
            var viewMock = new Mock<IGameCreationView>();
            var gameSearcher = new GameSearcherMock
            {
                SearchTimeMilliseconds = 100,
                CancelTimeMilliseconds = 0,
                CanBeCanceled = true
            };
            var presenter = new GameCreationPresenterBuilder
            {
                GameCreationView = viewMock.Object,
                GameSearcher = gameSearcher,
                AccessTokenProvider = new Mock<IAccessTokenProvider>().Object
            }.Build();

            presenter.CancelGame();

            viewMock.Verify(v => v.ShowCancelationMessage(It.IsAny<string>()), Times.Never);
        }

        [Test]
        //This case assumes that game creation process is going.
        public async Task CancelGame_ShouldHideCancelationMessage_IfCancelationIsNotSuccessful()
        {
            var viewMock = new Mock<IGameCreationView>();
            var gameSearcher = new GameSearcherMock
            {
                SearchTimeMilliseconds = 100,
                CanBeCanceled = false
            };
            var presenter = new GameCreationPresenterBuilder
            {
                GameCreationView = viewMock.Object,
                GameSearcher = gameSearcher,
                AccessTokenProvider = new Mock<IAccessTokenProvider>().Object
            }.Build();

            presenter.CreateGame();
            await presenter.CancelGame();

            viewMock.Verify(v => v.HideCancelationMessage(), Times.Once);
        }

        [Test]
        public void CreateGame_ShouldBeCalled_IfChooseCreateGameOnViewIsCalled()
        {
            var view = new TestGameCreationView();
            var presenterMock = new Mock<GameCreationPresenter>(new GameSearcherMock(), GetMockObject<IGameCreator>(), new Mock<IAccessTokenProvider>().Object, view, new Mock<IClientWrapper>().Object);
            var testObject = presenterMock.Object;
        
            view.ChooseCreateGame();
        
            presenterMock.Verify(p => p.CreateGame(), Times.Once);
        }

        [Test]
        public void CancelGame_ShouldBeCalled_IfChooseCancelGameOnViewIsCalled()
        {
            var view = new TestGameCreationView();
            var presenterMock = new Mock<GameCreationPresenter>(new GameSearcherMock(), GetMockObject<IGameCreator>(), new Mock<IAccessTokenProvider>().Object, view, new Mock<IClientWrapper>().Object);
            var testObject = presenterMock.Object;
        
            view.ChooseCancelGame();
        
            presenterMock.Verify(p => p.CancelGame(), Times.Once);
        }

        [Test]
        public async Task CreateGame_ShouldCallShowNoConnectionMessageOnView_IfClientIsNotConnected()
        {
            var clientWrapperMock = new Mock<IClientWrapper>();
            clientWrapperMock.Setup(c => c.IsConnected).Returns(false);
            var viewMock = new Mock<IGameCreationView>();
            var presenter = new GameCreationPresenterBuilder
            {
                ClientWrapper = clientWrapperMock.Object,
                GameCreationView = viewMock.Object,
            }.Build();
            
            await presenter.CreateGame();
            
            viewMock.Verify(v => v.ShowNoConnectionMessage(), Times.Once);
        }
        
        [Test]
        public async Task CreateGame_ShouldNotCallShowNoConnectionMessageOnView_IfClientIsConnected()
        {
            var clientWrapperMock = new Mock<IClientWrapper>();
            clientWrapperMock.Setup(c => c.IsConnected).Returns(true);
            var viewMock = new Mock<IGameCreationView>();
            var presenter = new GameCreationPresenterBuilder
            {
                ClientWrapper = clientWrapperMock.Object,
                GameCreationView = viewMock.Object,
            }.Build();
            
            await presenter.CreateGame();
            
            viewMock.Verify(v => v.ShowNoConnectionMessage(), Times.Never);
        }
    
    
        [TearDown]
        public void UnregisterSingletons()
        {
            try
            {
                Singleton<Game>.Unregister();
                Singleton<GameStartData>.Unregister();
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public class GameCreationPresenterMock : GameCreationPresenter
        {
            public bool CancelGameCalled { get; private set; }
            public bool CreateGameCalled { get; private set; }
        
            public GameCreationPresenterMock(IGameSearcher gameSearcher, IGameCreator gameCreator, IAccessTokenProvider accessTokenProvider, IGameCreationView view, IClientWrapper clientWrapper) : base(gameSearcher, gameCreator, accessTokenProvider, view, clientWrapper)
            {
            }

            public override Task CancelGame()
            {
                CancelGameCalled = true;
                return Task.CompletedTask;
            }
        
            public override Task CreateGame()
            {
                CreateGameCalled = true;
                return Task.CompletedTask;
            }
        }
    
        public class GameCreationPresenterBuilder
        {
            public IGameSearcher GameSearcher { get; set; } = new GameSearcherMock();
            public IGameCreator GameCreator { get; set; } = GetMockObject<IGameCreator>();
            public IAccessTokenProvider AccessTokenProvider { get; set; } = GetMockObject<IAccessTokenProvider>();
            public IGameCreationView GameCreationView { get; set; } = GetMockObject<IGameCreationView>();
            public IClientWrapper ClientWrapper { get; set; } = GetMockObject<IClientWrapper>();

            public GameCreationPresenterBuilder()
            {
                var clientWrapperMock = new Mock<IClientWrapper>();
                clientWrapperMock.Setup(c => c.IsConnected).Returns(true);
                ClientWrapper = clientWrapperMock.Object;
            }

            public GameCreationPresenter Build()
            {
                return new GameCreationPresenter(GameSearcher, GameCreator, AccessTokenProvider,
                    GameCreationView, ClientWrapper);
            }
        }

        public static IPlayerDataProvider GetPlayerDataProvider(int id = 1, string accessToken = "sometoken",
            bool isAuthorized = true)
        {
            var mock = new Mock<IPlayerDataProvider>();
            mock.Setup(p => p.GetId()).Returns(id);
            mock.Setup(p => p.GetAccessToken()).Returns(accessToken);
            mock.Setup(p => p.IsAuthorized()).Returns(isAuthorized);
            return mock.Object;
        }

        public static T GetMockObject<T>() where T: class
        {
            return new Mock<T>().Object;
        }
    }
}
