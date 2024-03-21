using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using castledice_game_data_logic;
using castledice_game_data_logic.ConfigsData;
using castledice_game_data_logic.Content;
using castledice_game_data_logic.MoveConverters;
using castledice_game_data_logic.Moves;
using castledice_game_data_logic.TurnSwitchConditions;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions;
using Src.Auth.TokenProviders;
using Src.GameplayPresenter.ActionPointsGiving;
using Src.GameplayPresenter.CellMovesHighlights;
using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using Src.GameplayPresenter.CellsContent;
using Src.GameplayPresenter.ClientMoves;
using Src.GameplayPresenter.DestroyedContent;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;
using Src.GameplayPresenter.GameOver;
using Src.GameplayPresenter.GameWrappers;
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
using Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation.CastleHP;
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
using Src.PlayerInput;
using Src.OLDPVE;
using Src.OLDPVE.MoveSearchers;
using Src.OLDPVE.MoveSearchers.TraitsEvaluators;
using Src.PVE.Providers;
using Src.TimeManagement;
using TMPro;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;
using CastleEntity = castledice_game_logic.GameObjects.Castle;

public class PVESceneInitializer : MonoBehaviour
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
    private PVEMovesPresenter _clientMovesPresenter;
    private ServerMovesPresenter _serverMovesPresenter;
    
    private ActionPointsGivingPresenter _actionPointsGivingPresenter;
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
    [SerializeField] private TimeView redPlayerTimeView;
    [SerializeField] private TimeView bluePlayerTimeView;
    [SerializeField] private Highlighter redPlayerHighlighter;
    [SerializeField] private Highlighter bluePlayerHighlighter;
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
    
    [Header("Castles health bars")]
    [SerializeField] private CastleHealthBar blueCastleHeathBar;
    [SerializeField] private CastleHealthBar redCastleHeathBar;
    
    [Header("Action points UI")] 
    [SerializeField] private GameObject playerBanner;
    [SerializeField] private TextMeshProUGUI playerActionPointsText;
    [SerializeField] private GameObject enemyBanner;
    [SerializeField] private TextMeshProUGUI enemyActionPointsText;
    private ActionPointsUI _playerActionPointsUI;
    private ActionPointsUI _enemyActionPointsUI;
    
    private Game _game;
    private GameStartData _gameStartData;
    private Bot _bot;
    private Player _player;
    private Player _botPlayer;
    private readonly NegentropyRandomNumberGenerator _firstPlayerRandomNumberGenerator = new(1, 7, 1000);
    private readonly NegentropyRandomNumberGenerator _secondPlayerRandomNumberGenerator = new(1, 7, 2000);

    private void Awake()
    {
        SetUpUpdaters();
    }
    
    private void Start()
    {
        
        SetUpGame();
        SetUpInput();
        SetUpGrid();
        SetUpContent();
        SetupCastleHealth();
        SetUpCells();
        SetUpClickDetectors();
        SetUpClientMoves();
        SetUpBot();
        SetUpPlacedUnitsHighlights();
        SetUpNewUnitsHighlights();
        SetUpActionPointsGiving();
        SetUpActionPointsUI();
        SetUpCellMovesHighlights();
        SetUpGameOver();
        GiveActionPointsToCurrentPlayer();
    }
    
    private void SetUpUpdaters()
    {
        updaterBehaviour.Init(_updater);
        fixedUpdaterBehaviour.Init(_fixedUpdater);
    }

    private void SetUpGame()
    {
        var cellsPresence = new bool[10, 10];
        for (int i = 0; i < cellsPresence.GetLength(0); i++)
        {
            for (int j = 0; j < cellsPresence.GetLength(1); j++)
            {
                cellsPresence[i, j] = true;
            }
        }
        var boardData = new BoardData(10,  10, CellType.Square, cellsPresence, new List<ContentData>
        {
            new CastleData((0, 0), 1, 1, 3, 3, 1),
            new CastleData((9, 9), 1, 1, 3, 3, 2),
        });
        var placeablesConfigData = new PlaceablesConfigData(new KnightConfigData(1, 2));
        var turnSwitchConditionsConfigData = new TscConfigData(new List<TscType> { TscType.SwitchByActionPoints });
        var playersData = new List<PlayerData>
        {
            new PlayerData(1, new List<PlacementType> { PlacementType.Knight }, TimeSpan.FromMinutes(50)),
            new PlayerData(2, new List<PlacementType> { PlacementType.Knight }, TimeSpan.FromMinutes(50)),
        };
        _gameStartData = new GameStartData("1.0.0", boardData, placeablesConfigData, turnSwitchConditionsConfigData, playersData);
        var playersListCreator = new PlayersListCreator(new PlayerCreator(new UpdatablePlayerTimerCreator(new FixedTimeDeltaProvider(), _fixedUpdater)));
        var coordinateSpawnerCreator = new CoordinateContentSpawnerCreator(new ContentToCoordinateCreator());
        var matrixCellsGeneratorCreator = new MatrixCellsGeneratorCreator();
        var boardConfigCreator = new BoardConfigCreator(coordinateSpawnerCreator, matrixCellsGeneratorCreator);
        var placeablesConfigCreator = new PlaceablesConfigCreator();
        var turnSwitchConditionsConfigCreator = new TurnSwitchConditionsConfigCreator();
        var gameBuilder = new GameBuilder(new GameConstructorWrapper());
        var gameCreator = new GameCreator(playersListCreator, boardConfigCreator, placeablesConfigCreator, turnSwitchConditionsConfigCreator, gameBuilder);
        _game = gameCreator.CreateGame(_gameStartData);
        _player = _game.GetAllPlayers()[0];
        _botPlayer = _game.GetAllPlayers()[1];
    }

    private void SetUpGameOver()
    {
        _gameOverView = new GameOverView(new DuelPlayerColorProvider(_player),
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
        _cellMovesHighlightView = new CellMovesHighlightView(highlightsPlacer);
        _cellMovesHighlightPresenter = new CellMovesHighlightPresenter(_player,
            new CellMovesListProvider(_game), new CellMovesHighlightObserver(_game, _player), _cellMovesHighlightView);
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
        var playerColorProvider = new DuelPlayerColorProvider(_player);
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
    
    private void SetupCastleHealth()
    {
        var board = _game.GetBoard();
        var playerCastle = board[0, 0].GetContent().
            Find(x => x is CastleEntity) as CastleEntity;
        var enemyCastle = board[
            board.GetLength(0) - 1,
            board.GetLength(1) - 1
        ].GetContent().Find(x => x is CastleEntity) as CastleEntity;
            
        var initialDurability = playerCastle.GetMaxDurability();
        blueCastleHeathBar.Init(initialDurability);
        redCastleHeathBar.Init(initialDurability);
            
        playerCastle.Hit += blueCastleHeathBar.ApplyHit;
        enemyCastle.Hit += redCastleHeathBar.ApplyHit;
    }
    
    private class ServerMoveApplierStub : IServerMoveApplier
    {
        public Task<MoveApplicationResult> ApplyMoveAsync(MoveData moveData, string token)
        {
            return Task.FromResult(MoveApplicationResult.Applied);
        }
    }
    
    private class AccessTokenProviderStub : IAccessTokenProvider
    {
        public Task<string> GetAccessTokenAsync()
        {
            return Task.FromResult("token");
        }
    }

    private void SetUpClientMoves()
    {
        _clientMovesView = new MovesView(_cellClickDetectors);
        var serverMovesApplier = new ServerMoveApplierStub();
        var localMovesApplier = new LocalMovesApplier(_game);
        _clientMovesPresenter = new PVEMovesPresenter(new AccessTokenProviderStub(), serverMovesApplier, new PossibleMovesListProvider(_game),
            localMovesApplier, new MoveToDataConverter(), _clientMovesView);
    }
    
        private void SetUpBot()
    {
        var botPlayer = _game.GetPlayer(2);
        var botBasePosition = new Vector2Int(9, 9);
        var playerBasePosition = new Vector2Int(0, 0);
        var board = _game.GetBoard();
        var localMoveApplier = new LocalMovesApplier(_game);
        var dfsValuesCutter = new DfsUnconnectedValuesCutter();
        var boardStateCalculator = new BoardCellsStateCalculator(board, dfsValuesCutter);
        var distancesCalculator = new BoardStateDistancesCalculator();
        var totalPossibleMovesProvider = new TotalPossibleMovesProvider(_game);
        var unitsStructureCalculator = new UnitsStructureCalculator(dfsValuesCutter, depth: 1);
        var moveEnhancivenessEvaluator = new StructureDeltaEvaluator(boardStateCalculator, unitsStructureCalculator);
        var moveDestructivenessEvaluator = new EnemyUnitsLossEvaluator(boardStateCalculator);
        var boardCostCalculator = new BoardCellsCostCalculator(board, boardStateCalculator);
        var moveAggressivenessEvaluator = new WeightedEnemyBaseProximityDeltaEvaluator(
            new DijkstraMinPathCostSearcher(), 
            boardCostCalculator, 
            boardStateCalculator, 
            playerBasePosition);
        var moveDefensivenessEvaluator = new WeightedEnemyProximityDeltaEvaluator(
            new DijkstraMinPathCostSearcher(), 
            boardCostCalculator, 
            boardStateCalculator, 
            botBasePosition);
        var moveHarmfulnessEvaluator = new EnemyStructureDeltaEvaluator(boardStateCalculator, unitsStructureCalculator);
        var bestMoveSearcher = new BalancedMoveSearcher(
            moveDestructivenessEvaluator, 
            moveAggressivenessEvaluator, 
            moveDefensivenessEvaluator,
            distancesCalculator, 
            boardStateCalculator,
            botPlayer,
            moveEnhancivenessEvaluator,
            moveHarmfulnessEvaluator);
        _bot = new Bot(localMoveApplier, totalPossibleMovesProvider, bestMoveSearcher, _game, 2);
        _bot.CantMove += () => drawScreen.SetActive(true);
    }

    private void SetUpPlacedUnitsHighlights()
    {
        var instantiator = new Instantiator();
        var playerColorProvider = new DuelPlayerColorProvider(_player);
        var objectsColorProvider = new PlayerObjectsColorProvider(placedUnitsHighlightsColorConfig, playerColorProvider);
        var underlineCreator = new ColoredHighlightCreator(coloredHighlightPrefabConfig, instantiator);
        _placedUnitsHighlightsView = new PlacedUnitsHighlightsView(grid, underlineCreator, objectsColorProvider);
        _placedUnitsHighlightsPresenter = new PlacedUnitsHighlightsPresenter(_game.GetBoard(), _placedUnitsHighlightsView);
    }
    
    private void SetUpNewUnitsHighlights()
    {
        var instantiator = new Instantiator();
        var playerColorProvider = new DuelPlayerColorProvider(_player);
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
        _game.TurnSwitched += GiveActionPointsToCurrentPlayer;
    }
    
    private void GiveActionPointsToCurrentPlayer()
    {
        var currentPlayer = _game.GetCurrentPlayer();
        if (currentPlayer.Id == 1)
        {
            var actionPoints = _firstPlayerRandomNumberGenerator.GetNextRandom();
            _actionPointsGivingPresenter.GiveActionPoints(currentPlayer.Id, actionPoints);
        }
        else
        {
            var actionPoints = _secondPlayerRandomNumberGenerator.GetNextRandom();
            _actionPointsGivingPresenter.GiveActionPoints(currentPlayer.Id, actionPoints);
        }
    }

    private void SetUpActionPointsUI()
    {
        _playerActionPointsUI = new ActionPointsUI(playerActionPointsText, playerBanner, _player);
        _enemyActionPointsUI = new ActionPointsUI(enemyActionPointsText, enemyBanner, _botPlayer);
    }

    
    private void GiveActionPointsToCurrentPlayer(object sender, Game e)
    {
        GiveActionPointsToCurrentPlayer();
    }
    
    
}
