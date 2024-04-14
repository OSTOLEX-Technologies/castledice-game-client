using System;
using Riptide;
using Riptide.Transports.Tcp;
using Riptide.Utils;
using Src.Auth.TokenProviders;
using Src.Components;
using Src.GameplayPresenter.Errors;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;
using Src.GameplayPresenter.GameCreation.Creators.GameCreator;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;
using Src.GameplayPresenter.GameCreation.GameSearching;
using Src.GameplayPresenter.PlayerInitialization;
using Src.GameplayPresenter.PlayerInitialization.Caching;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;
using Src.GameplayPresenter.ServerConnection;
using Src.GameplayView.Errors;
using Src.GameplayView.ServerConnection;
using Src.General.Caching;
using Src.General.LoadingScenes;
using Src.NetworkingModule;
using Src.NetworkingModule.DTOCreators;
using Src.NetworkingModule.Errors;
using Src.NetworkingModule.MessageHandlers;
using Src.NetworkingModule.PeerUpdaters;
using UnityEngine;
using UnityEngine.UI;

namespace Src.ScenesInitializers
{
    public class MainMenuInitializer : MonoBehaviour
    {
        [SerializeField] private UnityErrorPopup errorPopup;
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private GameObject gameCreationProcessScreen;
        [SerializeField] private UnityPeerUpdater peerUpdater;

        private GameCreationPresenter _gameCreationPresenter;
        private GameNotSavedErrorPresenter _gameNotSavedErrorPresenter;
        private GameNotSavedErrorView _gameNotSavedErrorView;
    
        [Header("Server connection")]
        [SerializeField] private GameObject connectingMessage;
        [SerializeField] private GameObject connectionFailedMessage;
        [SerializeField] private TMPro.TextMeshProUGUI connectionFailedReasonText;
        [SerializeField] private RejectReasonMessagesConfig rejectReasonMessagesConfig;
        [SerializeField] private ServerConnectionConfig serverConnectionConfig;
        [SerializeField] private Button connectButton;
        private ServerConnectionPresenter _serverConnectionPresenter;
        private ServerConnectionView _serverConnectionView;
        private ConnectButtonHandler _connectButtonHandler;
        
        [Header("Player initialization")]
        [SerializeField] private GameObject processMessage;
        [SerializeField] private GameObject failureMessage;
        [SerializeField] private Button initializeButton;
        private PlayerInitializationPresenter _playerInitializationPresenter;
        private PlayerInitializationView _playerInitializationView;
        private InitializeButtonHandler _initializeButtonHandler; 
    
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
            
            
        
            //Setting up game creation presenter
            //var gameSearcher = new GameSearcher(clientWrapper);
            //GameCreationMessageHandler.SetDTOAccepter(gameSearcher);
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
            //_gameCreationPresenter = new GameCreationPresenter(gameSearcher, gameCreator, accessTokenProvider, gameCreationView, clientWrapper);
        
            //Setting up error handling
            _gameNotSavedErrorView = new GameNotSavedErrorView(errorPopup, gameCreationProcessScreen);
            _gameNotSavedErrorPresenter = new GameNotSavedErrorPresenter(_gameNotSavedErrorView);
            var errorPresentersProvider = new ErrorPresentersProvider(_gameNotSavedErrorPresenter);
            var serverErrorsRouter = new ServerErrorsRouter(errorPresentersProvider);
            ServerErrorMessageHandler.SetAccepter(serverErrorsRouter);
            
            //Setting up player initialization
            var playerInitializationView = new PlayerInitializationView(processMessage, failureMessage);
            _playerInitializationView = playerInitializationView;
            var initializePlayerDtoSender = new InitializePlayerDtoSender(clientWrapper);
            var initializePlayerDtoCreator = new InitializePlayerDtoCreator(accessTokenProvider);
            var playerInitializationResultDtoAccepter = new PlayerInitializationResultDtoAccepter();
            var initializationSaver = new InitializationCacher();
            PlayerInitializationResultMessageHandler.SetDtoAccepter(playerInitializationResultDtoAccepter);
            _playerInitializationPresenter = new PlayerInitializationPresenter(_playerInitializationView, 
                initializePlayerDtoSender, 
                playerInitializationResultDtoAccepter, 
                initializePlayerDtoCreator, 
                initializationSaver, 
                clientWrapper);
            _initializeButtonHandler = new InitializeButtonHandler(_playerInitializationPresenter, 
                clientWrapper, 
                playerInitializationResultDtoAccepter, 
                initializeButton);
        
            //_gameCreationPresenter.GameCreated += OnGameCreated;
            clientWrapper.Connected += async (sender, args) => await _playerInitializationPresenter.StartInitializationAsync();
        }

        private void OnGameCreated(object sender, EventArgs e)
        {
            sceneLoader.LoadScene(SceneType.DuelGame);
        }
    }
}
