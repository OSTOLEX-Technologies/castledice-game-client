using System;
using System.Threading.Tasks;
using castledice_game_data_logic;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src;
using Src.GameplayPresenter;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayView;
using Tests.Mocks;
using static Tests.ObjectCreationUtility;

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

        public void ShowNonAuthorizedMessage(string message)
        {
        }

        public void HideNonAuthorizedMessage()
        {
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
            PlayerDataProvider = GetPlayerDataProvider(isAuthorized: true)
        }.Build();

        presenter.CreateGame();

        viewMock.Verify(v => v.ShowCreationProcessScreen(), Times.Once);
    }

    [Test]
    public void CreateGame_ShouldCallShowNonAuthorizedMessage_IfPlayerIsNotAuthorized()
    {
        var viewMock = new Mock<IGameCreationView>();
        var presenter = new GameCreationPresenterBuilder
        {
            GameCreationView = viewMock.Object,
            PlayerDataProvider = GetPlayerDataProvider(isAuthorized: false)
        }.Build();

        presenter.CreateGame();

        viewMock.Verify(v => v.ShowNonAuthorizedMessage(It.IsAny<string>()), Times.Once);
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
            PlayerDataProvider = GetPlayerDataProvider(isAuthorized: true)
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
            PlayerDataProvider = GetPlayerDataProvider(isAuthorized: true)
        }.Build();
        
        await presenter.CreateGame();
        var actualGameStartData = Singleton<GameStartData>.Instance;
        
        Assert.AreSame(expectedGameStartData, actualGameStartData);
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
            PlayerDataProvider = GetPlayerDataProvider(isAuthorized: true)
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
            PlayerDataProvider = GetPlayerDataProvider(isAuthorized: true)
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
            PlayerDataProvider = GetPlayerDataProvider(isAuthorized: true)
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
            PlayerDataProvider = GetPlayerDataProvider(isAuthorized: true)
        }.Build();

        presenter.CreateGame();
        await presenter.CancelGame();

        viewMock.Verify(v => v.HideCancelationMessage(), Times.Once);
    }

    [Test]
    public async Task CreateGame_ShouldBeCalled_IfChooseCreateGameOnViewIsCalled()
    {
        var view = new TestGameCreationView();
        var presenterMock = new Mock<GameCreationPresenter>(new GameSearcherMock(), GetMockObject<IGameCreator>(), GetPlayerDataProvider(isAuthorized: true), view);
        var testObject = presenterMock.Object;
        
        view.ChooseCreateGame();
        
        presenterMock.Verify(p => p.CreateGame(), Times.Once);
    }

    [Test]
    public async Task CancelGame_ShouldBeCalled_IfChooseCancelGameOnViewIsCalled()
    {
        var viewMock = new TestGameCreationView();
        var presenterMock = new Mock<GameCreationPresenter>(new GameSearcherMock(), GetMockObject<IGameCreator>(), GetPlayerDataProvider(isAuthorized: true), viewMock);
        var testObject = presenterMock.Object;
        
        viewMock.ChooseCancelGame();
        
        presenterMock.Verify(p => p.CancelGame(), Times.Once);
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
    
    public class GameCreationPresenterBuilder
    {
        public IGameSearcher GameSearcher { get; set; } = new GameSearcherMock();
        public IGameCreator GameCreator { get; set; } = GetMockObject<IGameCreator>();
        public IPlayerDataProvider PlayerDataProvider { get; set; } = GetMockObject<IPlayerDataProvider>();
        public IGameCreationView GameCreationView { get; set; } = GetMockObject<IGameCreationView>();

        public GameCreationPresenter Build()
        {
            return new GameCreationPresenter(GameSearcher, GameCreator, PlayerDataProvider,
                GameCreationView);
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
