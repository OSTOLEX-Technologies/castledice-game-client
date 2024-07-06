using Riptide;
using Riptide.Transports.Tcp;
using Riptide.Utils;
using Src.Auth.AuthTokenSaver;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;
using Src.Auth.TokenProviders;
using Src.Components;
using Src.GameplayPresenter.Errors;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.CreationHandling;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;
using Src.GameplayPresenter.GameCreation.Creators.GameCreator;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;
using Src.GameplayPresenter.GameCreation.GameSearching;
using Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting;
using Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting;
using Src.GameplayPresenter.GameCreation.GameSearching.MessageHandlers;
using Src.GameplayPresenter.PlayerInitialization;
using Src.GameplayPresenter.PlayerInitialization.Caching;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;
using Src.GameplayPresenter.ServerConnection;
using Src.GameplayView.Errors;
using Src.GameplayView.ServerConnection;
using Src.General.Caching;
using Src.General.LoadingScenes;
using Src.General.TimeRetriever;
using Src.MainMenu.Controllers;
using Src.MainMenu.Scripts;
using Src.MainMenu.Views;
using Src.NetworkingModule;
using Src.NetworkingModule.DTOCreators;
using Src.NetworkingModule.Errors;
using Src.NetworkingModule.MessageHandlers;
using Src.NetworkingModule.PeerUpdaters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.ScenesInitializers
{
    public class MainMenuInitializer : MonoBehaviour
    {
        
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private UnityPeerUpdater peerUpdater;

        [Header("Game creation")]
        [SerializeField] private GameObject matchmakingScreen;
        [SerializeField] private GameObject cancelationScreen;
        [SerializeField] private GameObject searchFailedMessage;
        [SerializeField] private TextMeshProUGUI searchFailedMessageTextMesh;
        [SerializeField] private FailMessagesConfig failMessagesConfig;
        [SerializeField] private Button playButton;
        [SerializeField] private Button cancelButton;
        private GameCreationPresenter _gameCreationPresenter;
        private GameCreationView _gameCreationView;
        
        [Header("Errors")]
        [SerializeField] private UnityErrorPopup errorPopup;
        private GameNotSavedErrorPresenter _gameNotSavedErrorPresenter;
        private GameNotSavedErrorView _gameNotSavedErrorView;

        [Header("Settings")] 
        [SerializeField] private SettingsPopup settingsPopup;
        
        [Header("Server connection")]
        [SerializeField] private GameObject connectingMessage;
        [SerializeField] private GameObject connectionFailedMessage;
        [SerializeField] private TextMeshProUGUI connectionFailedReasonText;
        [SerializeField] private RejectReasonMessagesConfig rejectReasonMessagesConfig;
        [SerializeField] private ServerConnectionConfig serverConnectionConfig;
        [SerializeField] private Button connectButton;
        private ServerConnectionPresenter _serverConnectionPresenter;
        private ServerConnectionView _serverConnectionView;
        private ConnectButtonHandler _connectButtonHandler;
        
        [Header("Player initialization")]
        [SerializeField] private GameObject processMessage;
        [SerializeField] private GameObject initializationFailureMessage;
        [SerializeField] private Button initializeButton;
        private PlayerInitializationPresenter _playerInitializationPresenter;
        private PlayerInitializationView _playerInitializationView;
        private InitializeButtonHandler _initializeButtonHandler;

        [Header("Firebase logout")] 
        [SerializeField] private FirebaseLogout logoutComponent;

        private IDateTimeRetriever _dateTimeRetriever;

        private void Start()
        {
            RiptideLogger.Initialize(Debug.Log,Debug.Log,Debug.LogWarning, Debug.LogError, false);
        
            //Setting up access token provider
            var accessTokenProvider = Singleton<IAccessTokenProvider>.Instance;
        
            //Setting up client
            ClientWrapper clientWrapper;
            if (!ClientsHolder.HasClient(ClientType.GameServerClient))
            {
                var client = new Client(new TcpClient());
                clientWrapper = new ClientWrapper(client);
                ClientsHolder.AddClient(ClientType.GameServerClient, clientWrapper);
                peerUpdater.SetPeer(client);
                peerUpdater.StartUpdating();
            }
            else
            {
                clientWrapper = ClientsHolder.GetClient(ClientType.GameServerClient);
            }

            //Setting up connection presenter
            var serverConnectionView = new ServerConnectionView(connectingMessage, connectionFailedMessage, connectionFailedReasonText, rejectReasonMessagesConfig);
            _serverConnectionView = serverConnectionView;
            _serverConnectionPresenter = new ServerConnectionPresenter(_serverConnectionView, clientWrapper, serverConnectionConfig);
            _connectButtonHandler = new ConnectButtonHandler(connectButton, _serverConnectionPresenter, clientWrapper);
            _serverConnectionPresenter.ConnectToServer();
            
            //Setting up player initialization
            var playerInitializationView = new PlayerInitializationView(processMessage, initializationFailureMessage);
            _playerInitializationView = playerInitializationView;
            var initializePlayerDtoSender = new InitializePlayerDtoSender(clientWrapper);
            var initializePlayerDtoCreator = new InitializePlayerDtoCreator(accessTokenProvider);
            var playerInitializationResultDtoAccepter = new PlayerInitializationResultDtoAccepter();
            var initializationCacher = new InitializationCacher();
            PlayerInitializationResultMessageHandler.SetDtoAccepter(playerInitializationResultDtoAccepter);
            _playerInitializationPresenter = new PlayerInitializationPresenter(_playerInitializationView, 
                initializePlayerDtoSender, 
                playerInitializationResultDtoAccepter, 
                initializePlayerDtoCreator, 
                initializationCacher, 
                clientWrapper);
            _initializeButtonHandler = new InitializeButtonHandler(_playerInitializationPresenter, 
                clientWrapper, 
                playerInitializationResultDtoAccepter, 
                initializeButton);
            clientWrapper.Connected += async (sender, args) => await _playerInitializationPresenter.StartInitializationAsync();
            
        
            //Setting up game creation presenter
            var requestGameDtoSender = new RequestGameDtoSender(clientWrapper);
            var requestGameDtoCreator = new RequestGameDtoCreator(accessTokenProvider);
            var gameRequester = new GameRequester(requestGameDtoCreator, requestGameDtoSender);
            var cancelGameDtoSender = new CancelGameDtoSender(clientWrapper);
            var cancelGameDtoCreator = new CancelGameDtoCreator(accessTokenProvider);
            var gameCancelRequester = new GameCancelRequester(cancelGameDtoCreator, cancelGameDtoSender);
            var gameSearcher = new GameSearcher(gameRequester, gameCancelRequester, clientWrapper, initializationCacher);
            var cellsGeneratorProvider = new MatrixCellsGeneratorCreator();
            var contentToCoordinateProvider = new ContentToCoordinateCreator();
            var spawnersProvider = new CoordinateContentSpawnerCreator(contentToCoordinateProvider);
            var boardConfigProvider = new BoardConfigCreator(spawnersProvider, cellsGeneratorProvider);
            var playersListProvider = new PlayersListCreator(new PlayerCreator(new StopwatchPlayerTimerCreator()));
            var placeablesConfigProvider = new PlaceablesConfigCreator();
            var gameBuilder = new GameBuilder(new GameConstructorWrapper());
            var turnSwitchConditionsConfigProvider = new TurnSwitchConditionsConfigCreator();
            var gameCreator = new GameCreator(playersListProvider, boardConfigProvider, placeablesConfigProvider, 
                turnSwitchConditionsConfigProvider, gameBuilder);
            var sceneLoadingGameCreationHandler = new SceneLoadingGameCreationHandler(sceneLoader, SceneType.DuelGame);
            CreateGameMessageHandler.SetDtoAccepter(gameSearcher);
            CancelGameResultMessageHandler.SetDtoAccepter(gameSearcher);
            var gameCreationHandler = new CachingGameCreationHandlerDecorator(sceneLoadingGameCreationHandler, new SingletonCacher());
            _gameCreationView = new GameCreationView(playButton, 
                cancelButton, 
                matchmakingScreen, 
                cancelationScreen, 
                searchFailedMessage, searchFailedMessageTextMesh, 
                failMessagesConfig);
            _gameCreationPresenter = new GameCreationPresenter(_gameCreationView, gameSearcher, gameCreator, gameCreationHandler);
        
            //Setting up error handling
            _gameNotSavedErrorView = new GameNotSavedErrorView(errorPopup, matchmakingScreen);
            _gameNotSavedErrorPresenter = new GameNotSavedErrorPresenter(_gameNotSavedErrorView);
            var errorPresentersProvider = new ErrorPresentersProvider(_gameNotSavedErrorPresenter);
            var serverErrorsRouter = new ServerErrorsRouter(errorPresentersProvider);
            ServerErrorMessageHandler.SetAccepter(serverErrorsRouter);
            
            //Setting up settings popup
            var settingsPopupView = new SettingsPopupView();
            var settingsPopupController = new SettingsPopupController(settingsPopupView);
            settingsPopup.SettingsPopupController = settingsPopupController;
            settingsPopup.SettingsPopupView = settingsPopupView;
            settingsPopup.Init();

            _dateTimeRetriever = new DateTimeRetriever();
            logoutComponent.Init(
                new AuthTokenSaver(
                    new StringSaver()),
                sceneLoader,
                _dateTimeRetriever);
        }
    }
}
