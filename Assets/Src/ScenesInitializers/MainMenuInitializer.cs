using System;
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
using Src.GameplayPresenter.GameCreation.Timeout;
using Src.GameplayPresenter.PlayerInitialization;
using Src.GameplayPresenter.PlayerInitialization.Caching;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;
using Src.GameplayPresenter.ServerConnection;
using Src.GameplayView.Errors;
using Src.GameplayView.ServerConnection;
using Src.General.Caching;
using Src.General.LoadingScenes;
using Src.General.PlayerInitialization;
using Src.General.SceneTransitionCommands;
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
        [SerializeField] private CoroutineTimeout gameSearchTimeout;
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
        
        [Header("Logout")]
        [SerializeField] private FirebaseLogout firebaseLogout;
        private IAuthTokenSaver _authTokenSaver;
        private ISceneTransitionHandler _logoutSceneTransitionHandler;
        
        //Common dependencies
        private IAccessTokenProvider _accessTokenProvider;
        private IClientWrapper _clientWrapper;
        private IPlayerInitializationProvider _playerInitializationProvider;
    
        private void Start()
        {
            SetUpLogger();
            SetUpAccessTokenProvider();
            SetUpClientWrapper();
            SetUpServerConnection();
            SetUpPlayerInitialization();
            SetUpGameCreation();
            SetUpErrorHandling();
            SetUpSettingsPopup();
            SetUpLogout();
        }

        private void SetUpLogout()
        {
            _authTokenSaver = new AuthTokenSaver(new StringSaver());
            _logoutSceneTransitionHandler = new SceneTransitionHandler(sceneLoader, SceneType.Auth);
            firebaseLogout.Init(_authTokenSaver, _logoutSceneTransitionHandler);
        }

        private void SetUpSettingsPopup()
        {
            var settingsPopupView = new SettingsPopupView();
            var settingsPopupController = new SettingsPopupController(settingsPopupView);
            settingsPopup.SettingsPopupController = settingsPopupController;
            settingsPopup.SettingsPopupView = settingsPopupView;
            settingsPopup.Init();
        }

        private void SetUpErrorHandling()
        {
            _gameNotSavedErrorView = new GameNotSavedErrorView(errorPopup, matchmakingScreen);
            _gameNotSavedErrorPresenter = new GameNotSavedErrorPresenter(_gameNotSavedErrorView);
            var errorPresentersProvider = new ErrorPresentersProvider(_gameNotSavedErrorPresenter);
            var serverErrorsRouter = new ServerErrorsRouter(errorPresentersProvider);
            ServerErrorMessageHandler.SetAccepter(serverErrorsRouter);
        }

        private void SetUpGameCreation()
        {
            var requestGameDtoSender = new RequestGameDtoSender(_clientWrapper);
            var requestGameDtoCreator = new RequestGameDtoCreator(_accessTokenProvider);
            var gameRequester = new GameRequester(requestGameDtoCreator, requestGameDtoSender);
            var cancelGameDtoSender = new CancelGameDtoSender(_clientWrapper);
            var cancelGameDtoCreator = new CancelGameDtoCreator(_accessTokenProvider);
            var gameCancelRequester = new GameCancelRequester(cancelGameDtoCreator, cancelGameDtoSender);
            var gameSearcher =
                new GameSearcher(gameRequester, gameCancelRequester, _clientWrapper, _playerInitializationProvider);
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
            var gameCreationHandler =
                new CachingGameCreationHandlerDecorator(sceneLoadingGameCreationHandler, new SingletonCacher());
            _gameCreationView = new GameCreationView(playButton,
                cancelButton,
                matchmakingScreen,
                cancelationScreen,
                searchFailedMessage, searchFailedMessageTextMesh,
                failMessagesConfig);
            _gameCreationPresenter =
                new GameCreationPresenter(
                    _gameCreationView, 
                    gameSearcher, 
                    gameCreator, 
                    gameCreationHandler,
                    gameSearchTimeout,
                    sceneLoader);
        }

        private void SetUpPlayerInitialization()
        {
            var playerInitializationView = new PlayerInitializationView(processMessage, initializationFailureMessage);
            _playerInitializationView = playerInitializationView;
            var initializePlayerDtoSender = new InitializePlayerDtoSender(_clientWrapper);
            var initializePlayerDtoCreator = new InitializePlayerDtoCreator(_accessTokenProvider);
            var playerInitializationResultDtoAccepter = new PlayerInitializationResultDtoAccepter();
            var initializationCacher = new InitializationCacher();
            _playerInitializationProvider = initializationCacher;
            PlayerInitializationResultMessageHandler.SetDtoAccepter(playerInitializationResultDtoAccepter);
            _playerInitializationPresenter = new PlayerInitializationPresenter(_playerInitializationView,
                initializePlayerDtoSender,
                playerInitializationResultDtoAccepter,
                initializePlayerDtoCreator,
                initializationCacher,
                _clientWrapper);
            _initializeButtonHandler = new InitializeButtonHandler(_playerInitializationPresenter,
                _clientWrapper,
                playerInitializationResultDtoAccepter,
                initializeButton);
            _clientWrapper.Connected += OnConnected;
        }

        private async void OnConnected(object sender, EventArgs e)
        {
            await _playerInitializationPresenter.StartInitializationAsync();
        }


        private void SetUpServerConnection()
        {
            var serverConnectionView = new ServerConnectionView(connectingMessage, connectionFailedMessage,
                connectionFailedReasonText, rejectReasonMessagesConfig);
            _serverConnectionView = serverConnectionView;
            _serverConnectionPresenter =
                new ServerConnectionPresenter(_serverConnectionView, _clientWrapper, serverConnectionConfig);
            _connectButtonHandler = new ConnectButtonHandler(connectButton, _serverConnectionPresenter, _clientWrapper);
            _serverConnectionPresenter.ConnectToServer();
        }

        private void SetUpClientWrapper()
        {
            if (!ClientsHolder.HasClient(ClientType.GameServerClient))
            {
                var client = new Client(new TcpClient());
                _clientWrapper = new ClientWrapper(client);
                ClientsHolder.AddClient(ClientType.GameServerClient, _clientWrapper);
                peerUpdater.SetPeer(client);
                peerUpdater.StartUpdating();
            }
            else
            {
                _clientWrapper = ClientsHolder.GetClient(ClientType.GameServerClient);
            }
        }

        private void SetUpLogger()
        {
            RiptideLogger.Initialize(Debug.Log,Debug.Log,Debug.LogWarning, Debug.LogError, false);
        }
        
        private void SetUpAccessTokenProvider()
        {
            _accessTokenProvider = Singleton<IAccessTokenProvider>.Instance;
        }

        private void OnDestroy()
        {
            _clientWrapper.Connected -= OnConnected;
            _connectButtonHandler.Dispose();
            _playerInitializationPresenter.Dispose();
            _initializeButtonHandler.Dispose();
            _serverConnectionPresenter.Dispose();
            _gameCreationPresenter.Dispose();
        }
    }
}
