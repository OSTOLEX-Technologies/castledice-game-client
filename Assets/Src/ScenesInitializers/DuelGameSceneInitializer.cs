using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using castledice_game_data_logic;
using castledice_game_data_logic.MoveConverters;
using castledice_game_logic;
using castledice_game_logic.Math;
using Src.Auth.TokenProviders;
using Src.Components;
using Src.GameplayPresenter.ActionPointsGiving;
using Src.GameplayPresenter.CellMovesHighlights;
using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using Src.GameplayPresenter.CellsContent;
using Src.GameplayPresenter.ClientMoves;
using Src.GameplayPresenter.DestroyedContent;
using Src.GameplayPresenter.EnemyDisconnect;
using Src.GameplayPresenter.EnemyDisconnect.NetworkBridges;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;
using Src.GameplayPresenter.GameCreation.Creators.GameCreator;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;
using Src.GameplayPresenter.GameOver;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayPresenter.InGameDisconnectHandling;
using Src.GameplayPresenter.NewUnitsHighlights;
using Src.GameplayPresenter.PlacedUnitsHighlights;
using Src.GameplayPresenter.ServerMoves;
using Src.GameplayPresenter.Timers;
using Src.GameplayView;
using Src.GameplayView.ActionPointsGiving;
using Src.GameplayView.CellMovesHighlights;
using Src.GameplayView.Cells;
using Src.GameplayView.CellsContent;
using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.CellsContent.ContentViewsCreation;
using Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation;
using Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation;
using Src.GameplayView.CellsContent.ContentViewsCreation.TreeViewCreation;
using Src.GameplayView.ClickDetection;
using Src.GameplayView.ClientMoves;
using Src.GameplayView.ContentVisuals.VisualsCreation;
using Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation;
using Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation;
using Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation;
using Src.GameplayView.DestroyedContent;
using Src.GameplayView.GameOver;
using Src.GameplayView.Grid;
using Src.GameplayView.Grid.GridGeneration;
using Src.GameplayView.Highlights;
using Src.GameplayView.NewUnitsHighlights;
using Src.GameplayView.PlacedUnitsHighlights;
using Src.GameplayView.PlayerObjectsColor;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersNumbers;
using Src.GameplayView.PlayersRotations.RotationsByOrder;
using Src.GameplayView.Timers;
using Src.GameplayView.Timers.PlayerTimerViews;
using Src.GameplayView.Updatables;
using Src.General.Caching;
using Src.General.LoadingScenes;
using Src.General.TimeManagement;
using Src.HttpUtils;
using Src.NetworkingModule;
using Src.NetworkingModule.MessageHandlers;
using Src.NetworkingModule.Moves;
using Src.PlayerInput;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DuelGameSceneInitializer : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera camera;
    [SerializeField] private Transform secondPlayerCameraPosition;

    [Header("Game over")]
    [SerializeField] private GameObject blueWinnerScreen;
    [SerializeField] private GameObject redWinnerScreen;
    [SerializeField] private GameObject drawScreen;
    private GameOverPresenter _gameOverPresenter;
    private GameOverView _gameOverView;
    
    [Header("Clicks detection")]
    [SerializeField] private CellClickDetectorsConfig cellClickDetectorsConfig;
    [SerializeField] private CellClickDetectorsFactory cellClickDetectorsFactory;
    private List<ICellClickDetector> _cellClickDetectors;
    private TouchInputHandler _touchInputHandler;
    private PlayerInputReader _inputReader;
    
    [Header("Grid")]
    [SerializeField] private GameObjectsGrid grid;
    [SerializeField] private SquareGridGenerationConfig gridGenerationConfig;
    private SquareGridGenerator _gridGenerator;
    
    [Header("Cells")]
    [SerializeField] private SquareCellsFactory cellsFactory;
    [SerializeField] private SquareCellAssetsConfig assetsConfig;
    private SquareCellsViewGenerator3D _cellsViewGenerator;
    
    [Header("Content configs")]
    [SerializeField] private ScriptablePlayerOrderRotationConfig playerOrderRotations;
    [Header("Knight configs")]
    [SerializeField] private KnightSoundsConfig knightSoundsConfig;
    [SerializeField] private SoundPlayerKnightAudio knightAudioPrefab;
    [SerializeField] private KnightView knightViewPrefab;
    [SerializeField] private KnightVisualPrefabConfig knightVisualPrefabConfig;
    [SerializeField] private PlayerObjectsColorConfig knightColorConfig;
    [Header("Tree config")]
    [SerializeField] private TreeView treeViewPrefab;
    [SerializeField] private TreeVisualPrefabsConfig treeVisualPrefabConfig;
    [Header("Castle config")]
    [SerializeField] private CastleSoundsConfig castleSoundsConfig;
    [SerializeField] private CastleView castleViewPrefab;
    [SerializeField] private SoundPlayerCastleAudio castleAudioPrefab;
    [SerializeField] private CastleVisualPrefabConfig castleVisualPrefabConfig;
    [SerializeField] private PlayerObjectsColorConfig castleColorConfig;
    private CellsContentPresenter _cellContentPresenter;
    private CellsContentView _contentView;
    
    //Moves
    private MovesView _clientMovesView;
    private ClientMovesPresenter _clientMovesPresenter;
    private ServerMovesPresenter _serverMovesPresenter;

    [Header("Action points")] 
    [SerializeField] private GameObject blueBanner;
    [SerializeField] private TextMeshProUGUI blueActionPointsText;
    [SerializeField] private GameObject redBanner;
    [SerializeField] private TextMeshProUGUI redActionPointsText;
    private ActionPointsUI _blueActionPointsUI;
    private ActionPointsUI _redActionPointsUI;
    private IActionPointsGivingPresenter _actionPointsGivingPresenter;
    private IActionPointsGivingView _actionPointsGivingView;
    
    [Header("Move highlights")]
    [SerializeField] private UnityCellMoveHighlightsConfig cellMoveHighlightsConfig;
    [SerializeField] private UnityCellMoveHighlightsFactory cellMoveHighlightsFactory;
    private CellMovesHighlightPresenter _cellMovesHighlightPresenter;
    private CellMovesHighlightView _cellMovesHighlightView;
    
    [Header("Destroyed content")]
    [SerializeField] private TransparencyConfig destroyedContentTransparencyConfig;
    private DestroyedContentView _destroyedContentView;
    private DestroyedContentPresenter _destroyedContentPresenter;

    [Header("Timers")] 
    [SerializeField] private TextMeshProUGUI redTimerTextActive;
    [SerializeField] private TextMeshProUGUI redTimerTextInactive;
    [SerializeField] private GameObject redTimerBackgroundActive;
    [SerializeField] private GameObject redTimerBackgroundInactive;
    [SerializeField] private GameObject redTimerGlow;
    [SerializeField] private TextMeshProUGUI blueTimerTextActive;
    [SerializeField] private TextMeshProUGUI blueTimerTextInactive;
    [SerializeField] private GameObject blueTimerBackgroundActive;
    [SerializeField] private GameObject blueTimerBackgroundInactive;
    [SerializeField] private GameObject blueTimerGlow;
    [SerializeField] private int glowTimeSeconds;
    private TimersPresenter _timersPresenter;
    private TimersView _timersView;

    [Header("Placed units highlights")]
    [SerializeField] private ColoredHighlightPrefabConfig coloredHighlightPrefabConfig;
    [SerializeField] private PlayerObjectsColorConfig placedUnitsHighlightsColorConfig;
    private PlacedUnitsHighlightsView _placedUnitsHighlightsView;
    private PlacedUnitsHighlightsPresenter _placedUnitsHighlightsPresenter;
    
    [Header("New units highlights")]
    [SerializeField] private ColoredHighlightPrefabConfig newUnitsHighlightsPrefabConfig;
    [SerializeField] private PlayerObjectsColorConfig newUnitsHighlightsColorConfig;
    private NewUnitsHighlightsView _newUnitsHighlightsView;
    private NewUnitsHighlightsPresenter _newUnitsHighlightsPresenter;
    
    [Header("Updater")]
    [SerializeField] private FixedUpdaterBehaviour fixedUpdaterBehaviour;
    [SerializeField] private UpdaterBehaviour updaterBehaviour;
    private readonly Updater _updater = new();
    private readonly Updater _fixedUpdater = new();

    [Header("Disconnect handling")] 
    [SerializeField] private GameObject _disconnectedPopup;
    [SerializeField] private Button _disconnectedPopupButton;
    private InGameDisconnectHandler _disconnectHandler;
    
    [Header("Enemy disconnect handling")]
    [SerializeField] private GameObject _enemyDisconnectedPopup;
    [SerializeField] private Button _enemyDisconnectedPopupButton;
    private EnemyDisconnectPresenter _enemyDisconnectPresenter;
    private IEnemyDisconnectView _enemyDisconnectView;

    [Header("Scene loading")]
    [SerializeField] private SceneLoader _sceneLoader;
    
    private Game _game;
    private GameStartData _gameStartData;
    private Player _localPlayer;
    private Player _enemyPlayer;
    private DuelPlayerColorProvider _playerColorProvider;
    private PlayerIdProvider _playerIdProvider;
    private IAccessTokenProvider _accessTokenProvider;
    
    private async void Start()
    {
        SetUpUpdaters();
        SetUpGame();
        _accessTokenProvider = Singleton<IAccessTokenProvider>.Instance;
        _playerIdProvider = new PlayerIdProvider();
        _localPlayer = _game.GetPlayer(await _playerIdProvider.GetLocalPlayerId());
        _enemyPlayer = _game.GetAllPlayers().Find(p => p != _localPlayer);
        SetUpInput();
        SetUpGrid();
        SetUpContent();
        SetUpCells();
        SetUpClickDetectors();
        SetUpClientMoves();
        SetUpServerMoves();
        SetUpPlacedUnitsHighlights();
        SetUpNewUnitsHighlights();
        SetUpActionPointsGiving();
        SetUpActionPointsCountUI();
        SetUpCamera();
        SetUpCellMovesHighlights();
        SetUpGameOver();
        SetUpTimers();
        SetUpDisconnectHandling();
        SetUpEnemyDisconnectHandling();
        await NotifyPlayerIsReady();
    }

    private void SetUpEnemyDisconnectHandling()
    {
        var view = new EnemyDisconnectView(_enemyDisconnectedPopup, _enemyDisconnectedPopupButton);
        _enemyDisconnectView = view;
        var eventEmmiter = new DuelPlayerDisconnectedDTOAccepter();
        PlayerDisconnectedMessageHandler.SetDTOAccepter(eventEmmiter);
        _enemyDisconnectPresenter = new EnemyDisconnectPresenter(view, eventEmmiter, _sceneLoader);
    }

    private void SetUpDisconnectHandling()
    {
        _disconnectHandler = new InGameDisconnectHandler(ClientsHolder.GetClient(ClientType.GameServerClient),
            _disconnectedPopup, _disconnectedPopupButton, _sceneLoader, SceneType.MainMenu);
    }

    private void SetUpUpdaters()
    {
        updaterBehaviour.Init(_updater);
        fixedUpdaterBehaviour.Init(_fixedUpdater);
    }

    private void SetUpTimers()
    {
        var bluePlayerTimerView = new PlayerTimerView(
            blueTimerTextActive, 
            blueTimerTextInactive, 
            blueTimerBackgroundActive, 
            blueTimerBackgroundInactive,
            blueTimerGlow,
            _localPlayer.Timer,
            TimeSpan.FromSeconds(glowTimeSeconds));
        var redPlayerTimerView = new PlayerTimerView(
            redTimerTextActive, 
            redTimerTextInactive, 
            redTimerBackgroundActive, 
            redTimerBackgroundInactive,
            redTimerGlow,
            _enemyPlayer.Timer,
            TimeSpan.FromSeconds(glowTimeSeconds));
        var timersDictionary = new Dictionary<PlayerColor, IPlayerTimerView>
        {
            {PlayerColor.Blue, bluePlayerTimerView},
            {PlayerColor.Red, redPlayerTimerView}
        };
        var playerTimerViewsProvider = new PlayerTimerViewProvider(timersDictionary, _playerColorProvider);
        _timersView = new TimersView(playerTimerViewsProvider, _updater);
        _timersPresenter = new TimersPresenter(_timersView, _game);
        var switchTimerDTOAccepter = new SwitchTimerAccepter(_timersPresenter);
        SwitchTimerMessageHandler.SetAccepter(switchTimerDTOAccepter);
    }

    private void SetUpGame()
    {
        _gameStartData = Singleton<GameStartData>.Instance;
        var playersListCreator = new PlayersListCreator(new PlayerCreator(new UpdatablePlayerTimerCreator(new FixedTimeDeltaProvider(), _fixedUpdater)));
        var coordinateSpawnerCreator = new CoordinateContentSpawnerCreator(new ContentToCoordinateCreator());
        var matrixCellsGeneratorCreator = new MatrixCellsGeneratorCreator();
        var boardConfigCreator = new BoardConfigCreator(coordinateSpawnerCreator, matrixCellsGeneratorCreator);
        var placeablesConfigCreator = new PlaceablesConfigCreator();
        var turnSwitchConditionsConfigCreator = new TurnSwitchConditionsConfigCreator();
        var gameBuilder = new GameBuilder(new GameConstructorWrapper());
        var gameCreator = new GameCreator(playersListCreator, boardConfigCreator, placeablesConfigCreator, turnSwitchConditionsConfigCreator, gameBuilder);
        _game = gameCreator.CreateGame(_gameStartData);
    }

    private void SetUpGameOver()
    {
        _gameOverView = new GameOverView(new DuelPlayerColorProvider(_localPlayer),
            blueWinnerScreen, redWinnerScreen, drawScreen);
        _gameOverPresenter = new GameOverPresenter(_game, _gameOverView);
    }

    private void SetUpInput()
    {
        var cameraWrapper = new CameraWrapper(camera);
        var raycaster = new Raycaster3D(new RaycastHitProvider());
        _touchInputHandler = new TouchInputHandler(cameraWrapper, raycaster);
        _inputReader = new PlayerInputReader(_touchInputHandler);
        _inputReader.Enable();
    }

    private void OnDestroy()
    {
        _inputReader.Disable();
        _disconnectHandler.Dispose();
        _enemyDisconnectPresenter.Dispose();
    }


    private void SetUpGrid()
    {
        _gridGenerator = new SquareGridGenerator(grid, gridGenerationConfig);
        _gridGenerator.GenerateGrid(_gameStartData.BoardData.CellsPresence);
    }

    private void SetUpCellMovesHighlights()
    {
        cellMoveHighlightsFactory.Init(cellMoveHighlightsConfig);
        var highlightsPlacer = new CellMovesHighlightsPlacer(grid, cellMoveHighlightsFactory);
        var highlights = highlightsPlacer.PlaceHighlights();
        _cellMovesHighlightView = new CellMovesHighlightView(highlights);
        _cellMovesHighlightPresenter = new CellMovesHighlightPresenter(_localPlayer,
            new CellMovesListProvider(_game), new CellMovesHighlightObserver(_game, _localPlayer), _cellMovesHighlightView);
    }

    private void SetUpClickDetectors()
    {
        cellClickDetectorsFactory.Init(cellClickDetectorsConfig);
        var placer = new CellClickDetectorsPlacer(grid, cellClickDetectorsFactory);
        _cellClickDetectors = placer.PlaceDetectors();
    }

    private void SetUpCells()
    {
        cellsFactory.Init(assetsConfig);
        _cellsViewGenerator = new SquareCellsViewGenerator3D(cellsFactory, grid);
        var cellViewMapGenerator = new SquareCellViewMapGenerator(assetsConfig);
        var cellViewMap = cellViewMapGenerator.GetCellViewMap(_gameStartData.BoardData);
        _cellsViewGenerator.GenerateCellsView(cellViewMap);
    }

    private void SetUpContent()
    {
        var instantiator = new Instantiator();
        var playersList = _game.GetAllPlayers();
        var playerColorProvider = new DuelPlayerColorProvider(_localPlayer);
        var playerNumberProvider = new PlayerNumberProvider(playersList);
        var playerRotationProvider = new PlayerOrderRotationProvider(playerOrderRotations, playerNumberProvider);
        
        var randomTreeVisualCreator = new RandomTreeVisualCreator(new RangeRandomNumberGenerator(), treeVisualPrefabConfig, instantiator);
        var cachingTreeVisualCreator = new CachingTreeVisualCreator(randomTreeVisualCreator);
        var treeViewFactory = new TreeViewFactory(cachingTreeVisualCreator, treeViewPrefab, instantiator);

        var knightColorProvider = new PlayerObjectsColorProvider(knightColorConfig, playerColorProvider);
        var knightVisualCreator = new KnightVisualCreator(knightVisualPrefabConfig, knightColorProvider, instantiator, playerRotationProvider);
        var knightAudioFactory = new SoundPlayerKnightAudioFactory(knightSoundsConfig, knightAudioPrefab, instantiator);
        var knightViewFactory = new KnightViewFactory(knightVisualCreator, knightAudioFactory, knightViewPrefab, instantiator);
        var castleColorProvider = new PlayerObjectsColorProvider(castleColorConfig, playerColorProvider);
        var castleVisualCreator = new CastleVisualCreator(castleVisualPrefabConfig, castleColorProvider, instantiator);
        var castleAudioFactory = new SoundPlayerCastleAudioFactory(castleSoundsConfig, castleAudioPrefab, instantiator);
        var castleViewFactory = new CastleViewFactory(castleVisualCreator, castleAudioFactory, castleViewPrefab, instantiator);
        
        var contentViewProvider = new ContentViewProvider(treeViewFactory, knightViewFactory, castleViewFactory);
        
        _contentView = new CellsContentView(grid, contentViewProvider);
        _cellContentPresenter = new CellsContentPresenter(_contentView, _game.GetBoard());
        
        var contentVisualsCreator = new VisitorContentVisualCreator(knightVisualCreator, cachingTreeVisualCreator, castleVisualCreator);
        _destroyedContentView = new DestroyedContentView(grid, contentVisualsCreator, destroyedContentTransparencyConfig);
        _destroyedContentPresenter = new DestroyedContentPresenter(_game, _destroyedContentView);
    }

    private void SetUpClientMoves()
    {
        _clientMovesView = new MovesView(_cellClickDetectors);
        var serverMovesApplier = new ServerMoveApplier(ClientsHolder.GetClient(ClientType.GameServerClient));
        ApproveMoveMessageHandler.SetDTOAccepter(serverMovesApplier);
        var localMovesApplier = new LocalMovesApplier(_game);
        var possibleMovesProvider = new PossibleMovesListProvider(_game);
        _clientMovesPresenter = new ClientMovesPresenter(_accessTokenProvider, serverMovesApplier, possibleMovesProvider,
            localMovesApplier, new MoveToDataConverter(), _clientMovesView);
    }

    private void SetUpServerMoves()
    {
        _serverMovesPresenter = new ServerMovesPresenter(new LocalMovesApplier(_game),
            new DataToMoveConverter(_game.PlaceablesFactory), new PlayerProvider(_game));
        var movesAccepter = new ServerMoveAccepter(_serverMovesPresenter);
        MoveFromServerMessageHandler.SetDTOAccepter(movesAccepter);
    }

    private void SetUpPlacedUnitsHighlights()
    {
        var instantiator = new Instantiator();
        var playerColorProvider = new DuelPlayerColorProvider(_localPlayer);
        var objectsColorProvider = new PlayerObjectsColorProvider(placedUnitsHighlightsColorConfig, playerColorProvider);
        var underlineCreator = new ColoredHighlightCreator(coloredHighlightPrefabConfig, instantiator);
        _placedUnitsHighlightsView = new PlacedUnitsHighlightsView(grid, underlineCreator, objectsColorProvider);
        _placedUnitsHighlightsPresenter = new PlacedUnitsHighlightsPresenter(_game.GetBoard(), _placedUnitsHighlightsView);
    }
    
    private void SetUpNewUnitsHighlights()
    {
        var instantiator = new Instantiator();
        var playerColorProvider = new DuelPlayerColorProvider(_localPlayer);
        var objectsColorProvider = new PlayerObjectsColorProvider(newUnitsHighlightsColorConfig, playerColorProvider);
        var underlineCreator = new ColoredHighlightCreator(newUnitsHighlightsPrefabConfig, instantiator);
        _newUnitsHighlightsView = new NewUnitsHighlightsView(grid, underlineCreator, objectsColorProvider);
        _newUnitsHighlightsPresenter = new NewUnitsHighlightsPresenter(_game, _newUnitsHighlightsView);
    }


    public class ActionPointsGivingViewStub : IActionPointsGivingView
    {
        public void ShowActionPointsForPlayer(Player player, int amount)
        {
        }
    }
    
    private void SetUpActionPointsGiving()
    {
        _actionPointsGivingView = new ActionPointsGivingViewStub();
        _actionPointsGivingPresenter = new ActionPointsGivingPresenter(new PlayerProvider(_game),
            new ActionPointsGiver(_game), _actionPointsGivingView);
        GiveActionPointsMessageHandler.SetAccepter(new GiveActionPointsAccepter(_actionPointsGivingPresenter));
    }
    
    private void SetUpActionPointsCountUI()
    {
        _blueActionPointsUI = new ActionPointsUI(blueActionPointsText, blueBanner, _localPlayer);
        _redActionPointsUI = new ActionPointsUI(redActionPointsText, redBanner, _enemyPlayer);
    }

    private void SetUpCamera()
    {
        var playerId = _localPlayer.Id;
        var playerIndex = _game.GetAllPlayersIds().IndexOf(playerId);
        if (playerIndex == 1)
        {
            camera.transform.SetParent(secondPlayerCameraPosition);
            camera.transform.localPosition = Vector3.zero;
            camera.transform.localEulerAngles = Vector3.zero;
        }
    }

    private async Task NotifyPlayerIsReady()
    {
        var playerReadinessSender = new ReadinessSender(ClientsHolder.GetClient(ClientType.GameServerClient));
        playerReadinessSender.SendPlayerReadiness(await _accessTokenProvider.GetAccessTokenAsync());
    }
    
}
