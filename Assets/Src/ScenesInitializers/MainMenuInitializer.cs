using System;
using Riptide;
using Riptide.Transports.Tcp;
using Riptide.Utils;
using Src.Caching;
using Src.Components;
using Src.GameplayPresenter;
using Src.GameplayPresenter.Errors;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;
using Src.GameplayView.Errors;
using Src.GameplayView.GameCreation;
using Src.LoadingScenes;
using Src.MainMenu.Controllers;
using Src.MainMenu.Scripts;
using Src.MainMenu.Views;
using Src.NetworkingModule;
using Src.NetworkingModule.ConnectionConfiguration;
using Src.NetworkingModule.Errors;
using Src.NetworkingModule.MessageHandlers;
using Src.NetworkingModule.PeerUpdaters;
using Src.Stubs;
using UnityEngine;

public class MainMenuInitializer : MonoBehaviour
{
    [SerializeField] private UnityErrorPopup errorPopup;
    [SerializeField] private string duelModeSceneName;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameServerConnectionConfig gameServerConnectionConfig;
    [SerializeField] private UnityGameCreationView gameCreationView;
    [SerializeField] private GameObject gameCreationProcessScreen;
    [SerializeField] private UnityPeerUpdater peerUpdater;
    [SerializeField] private SettingsPopup settingsPopup;

    private GameCreationPresenter _gameCreationPresenter;
    private GameNotSavedErrorPresenter _gameNotSavedErrorPresenter;
    private GameNotSavedErrorView _gameNotSavedErrorView;
    
    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log,Debug.Log,Debug.LogWarning, Debug.LogError, false);
        
        //Setting up player data provider
        if (!Singleton<IPlayerDataProvider>.Registered)
        {
            Singleton<IPlayerDataProvider>.Register(new PlayerDataProviderStub());
        }
        var playerDataProvider = Singleton<IPlayerDataProvider>.Instance as PlayerDataProviderStub; //TODO: replace stub with real implementation
        
        //Setting up client
        ClientWrapper clientWrapper;
        if (!ClientsHolder.HasClient(ClientType.GameServerClient))
        {
            var client = new Client(new TcpClient());
            client.Connect($"{gameServerConnectionConfig.Ip}:{gameServerConnectionConfig.Port}");
            clientWrapper = new ClientWrapper(client);
            ClientsHolder.AddClient(ClientType.GameServerClient, clientWrapper);
            peerUpdater.SetPeer(client);
            peerUpdater.StartUpdating();
            
            //Initializing player
            var playerInitializer = new PlayerInitializer(clientWrapper);
            playerInitializer.InitializePlayer(playerDataProvider.GetAccessToken());
        }
        else
        {
            clientWrapper = ClientsHolder.GetClient(ClientType.GameServerClient);
        }

        //Setting up game creation presenter
        var gameSearcher = new GameSearcher(clientWrapper);
        GameCreationMessageHandler.SetDTOAccepter(gameSearcher);
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
        _gameCreationPresenter = new GameCreationPresenter(gameSearcher, gameCreator, playerDataProvider, gameCreationView);
        
        //Setting up error handling
        _gameNotSavedErrorView = new GameNotSavedErrorView(errorPopup, gameCreationProcessScreen);
        _gameNotSavedErrorPresenter = new GameNotSavedErrorPresenter(_gameNotSavedErrorView);
        var errorPresentersProvider = new ErrorPresentersProvider(_gameNotSavedErrorPresenter);
        var serverErrorsRouter = new ServerErrorsRouter(errorPresentersProvider);
        ServerErrorMessageHandler.SetAccepter(serverErrorsRouter);
        
        _gameCreationPresenter.GameCreated += OnGameCreated;
        
        // Setting up settings popup
        SettingsPopupView settingsPopupView = new SettingsPopupView();
        SettingsPopupController settingsPopupController = new SettingsPopupController(settingsPopupView);
        settingsPopup.SettingsPopupView = settingsPopupView;
        settingsPopup.SettingsPopupController = settingsPopupController;
        settingsPopup.Init();
        
    }

    private void OnGameCreated(object sender, EventArgs e)
    {
        sceneLoader.LoadScene(SceneType.DuelGame);
    }
}
