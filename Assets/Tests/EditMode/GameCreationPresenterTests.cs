using System.Threading.Tasks;
using castledice_game_data_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter;
using Src.GameplayView;
using Tests.Mocks;
using static Tests.ObjectCreationUtility;

public class GameCreationPresenterTests
{
    [Test]
    public void CreateGame_ShouldCallShowCreationProcessScreen_BeforeGameFound()
    {
        var viewMock = new Mock<GameCreationView>();
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
        var viewMock = new Mock<GameCreationView>();
        var presenter = new GameCreationPresenterBuilder
        {
            GameCreationView = viewMock.Object,
            PlayerDataProvider = GetPlayerDataProvider(isAuthorized: false)
        }.Build();

        presenter.CreateGame();

        viewMock.Verify(v => v.ShowNonAuthorizedMessage(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task CreateGame_ShouldPutGameInstanceIntoGameHolder_IfSearchIsSuccessful()
    {
        var expectedGame = GetGame();
        var gameHolder = new Mock<GameHolder>().Object;
        var gameCreatorMock = new Mock<GameCreator>();
        gameCreatorMock.Setup(g => g.CreateGame(It.IsAny<GameStartData>())).Returns(expectedGame);
        var presenter = new GameCreationPresenterBuilder
        {
            GameHolder = gameHolder,
            GameCreator = gameCreatorMock.Object,
            PlayerDataProvider = GetPlayerDataProvider(isAuthorized: true)
        }.Build();

        await presenter.CreateGame();

        Assert.AreSame(expectedGame, gameHolder.Game);
    }

    [Test]
    public async Task CreateGame_ShouldHideLoadingScreenAndCancelingMessage_IfGameCreationIsCanceled()
    {
        var viewMock = new Mock<GameCreationView>();
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
        var viewMock = new Mock<GameCreationView>();
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
        var viewMock = new Mock<GameCreationView>();
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
        var viewMock = new Mock<GameCreationView>();
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
        var viewMock = new Mock<GameCreationView>();
        var presenterMock = new Mock<GameCreationPresenter>(new GameSearcherMock(), GetMockObject<GameCreator>(), GetPlayerDataProvider(isAuthorized: true), GetMockObject<GameHolder>(), viewMock.Object);
        var testObject = presenterMock.Object;
        
        viewMock.Object.ChooseCreateGame();
        
        presenterMock.Verify(p => p.CreateGame(), Times.Once);
    }

    [Test]
    public async Task CancelGame_ShouldBeCalled_IfChooseCancelGameOnViewIsCalled()
    {
        var viewMock = new Mock<GameCreationView>();
        var presenterMock = new Mock<GameCreationPresenter>(new GameSearcherMock(), GetMockObject<GameCreator>(), GetPlayerDataProvider(isAuthorized: true), GetMockObject<GameHolder>(), viewMock.Object);
        var testObject = presenterMock.Object;
        
        viewMock.Object.ChooseCancelGame();
        
        presenterMock.Verify(p => p.CancelGame(), Times.Once);
    }
    
    public class GameCreationPresenterBuilder
    {
        public GameSearcher GameSearcher { get; set; } = new GameSearcherMock();
        public GameCreator GameCreator { get; set; } = GetMockObject<GameCreator>();
        public PlayerDataProvider PlayerDataProvider { get; set; } = GetMockObject<PlayerDataProvider>();
        public GameHolder GameHolder { get; set; } = GetMockObject<GameHolder>();
        public GameCreationView GameCreationView { get; set; } = GetMockObject<GameCreationView>();

        public GameCreationPresenter Build()
        {
            return new GameCreationPresenter(GameSearcher, GameCreator, PlayerDataProvider, GameHolder,
                GameCreationView);
        }
    }

    public static PlayerDataProvider GetPlayerDataProvider(int id = 1, string accessToken = "sometoken",
        bool isAuthorized = true)
    {
        var mock = new Mock<PlayerDataProvider>();
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
