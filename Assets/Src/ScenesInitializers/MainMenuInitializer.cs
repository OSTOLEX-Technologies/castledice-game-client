using System;
using Riptide;
using Riptide.Transports.Tcp;
using Riptide.Utils;
using Src;
using Src.GameplayPresenter;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;
using Src.GameplayView.GameCreation;
using Src.NetworkingModule;
using Src.NetworkingModule.ConnectionConfiguration;
using Src.NetworkingModule.MessageHandlers;
using Src.NetworkingModule.PeerUpdaters;
using Src.Stubs;
using UnityEngine;

public class MainMenuInitializer : MonoBehaviour
{
    [SerializeField] private string duelModeSceneName;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameServerConnectionConfig gameServerConnectionConfig;
    [SerializeField] private UnityGameCreationView gameCreationView;
    [SerializeField] private UnityPeerUpdater peerUpdater;

    private GameCreationPresenter gameCreationPresenter;
    
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
        var cellsGeneratorProvider = new MatrixCellsGeneratorProvider();
        var contentToCoordinateProvider = new ContentToCoordinateProvider();
        var spawnersProvider = new CoordinateContentSpawnerProvider(contentToCoordinateProvider);
        var boardConfigProvider = new BoardConfigProvider(spawnersProvider, cellsGeneratorProvider);
        var playersListProvider = new PlayersListProvider();
        var placeablesConfigProvider = new PlaceablesConfigProvider();
        var decksListProvider = new DecksListProvider();
        var gameCreator = new GameCreator(playersListProvider, boardConfigProvider, placeablesConfigProvider, decksListProvider);
        gameCreationPresenter = new GameCreationPresenter(gameSearcher, gameCreator, playerDataProvider, gameCreationView);
        
        gameCreationPresenter.GameCreated += OnGameCreated;
    }

    private void OnGameCreated(object sender, EventArgs e)
    {
        sceneLoader.LoadScene(duelModeSceneName);
    }
}
