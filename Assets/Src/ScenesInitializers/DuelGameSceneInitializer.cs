using System.Collections.Generic;
using System.Threading.Tasks;
using castledice_game_data_logic;
using castledice_game_data_logic.MoveConverters;
using castledice_game_logic;
using castledice_game_logic.Math;
using Src.Auth.TokenProviders;
using Src.Caching;
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
using Src.HttpUtils;
using Src.NetworkingModule;
using Src.NetworkingModule.MessageHandlers;
using Src.NetworkingModule.Moves;
using Src.PlayerInput;
using Src.Prototypes.NewActionPoints;
using Src.TimeManagement;
using TMPro;
using UnityEngine;
using CastleEntity = castledice_game_logic.GameObjects.Castle;

namespace Src.ScenesInitializers
{
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
        private MovesView _movesView;
        private ClientMovesPresenter _clientMovesPresenter;
        private ServerMovesPresenter _serverMovesPresenter;
    
        [Header("Action points giving")]
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
        
        [Header("Action points UI")]
        [SerializeField] private TextMeshProUGUI blueActionPointsText;
        [SerializeField] private GameObject blueActionPointsBanner;
        [SerializeField] private TextMeshProUGUI redActionPointsText;
        [SerializeField] private GameObject redActionPointsBanner;
        private ActionPointsUI _blueActionPointsUI;
        private ActionPointsUI _redActionPointsUI;
    
        [Header("Castles health bars")]
        [SerializeField] private CastleHealthBar blueCastleHeathBar;
        [SerializeField] private CastleHealthBar redCastleHeathBar;

        
        private Game _game;
        private GameStartData _gameStartData;

        private IAccessTokenProvider _accessTokenProvider;
        private IPlayerIdProvider _playerIdProvider;
        private DuelPlayerColorProvider _playerColorProvider;

        private async void Start()
        {
            _accessTokenProvider = Singleton<IAccessTokenProvider>.Instance;
            _playerIdProvider = new PlayerIdProvider();

            SetUpUpdaters();
            SetUpGame();
            await SetUpColorProvider();
            SetUpInput();
            SetUpGrid();
            SetUpContent();
            SetupCastleHealth();
            SetUpCells();
            SetUpClickDetectors();
            SetUpClientMoves();
            SetUpServerMoves();
            SetUpPlacedUnitsHighlights();
            SetUpNewUnitsHighlights();
            SetUpActionPointsGiving();
            await SetUpCamera();
            await SetUpCellMovesHighlights();
            SetUpGameOver();
            SetUpTimers();
            await SetUpActionPointsUI();
            await NotifyPlayerIsReady();
        }
    
        private void SetUpUpdaters()
        {
            updaterBehaviour.Init(_updater);
            fixedUpdaterBehaviour.Init(_fixedUpdater);
        }

        private void SetUpTimers()
        {
            var highlighterForPlayerProvider = new PlayerColorHighlighterProvider(redPlayerHighlighter, bluePlayerHighlighter,
                _playerColorProvider);
            var timeViewForPlayerProvider =
                new PlayerColorTimeViewProvider(redPlayerTimeView, bluePlayerTimeView, _playerColorProvider);
            var playerTimerViewCreator = new PlayerTimerViewCreator(highlighterForPlayerProvider, timeViewForPlayerProvider);
            var playerTimerViewsProvider = new CachingPlayerTimerViewProvider(playerTimerViewCreator);
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

        private async Task SetUpColorProvider()
        {
            var localPlayerId = await _playerIdProvider.GetLocalPlayerId();
            var localPlayer = _game.GetPlayer(localPlayerId);
            _playerColorProvider = new DuelPlayerColorProvider(localPlayer);
        }

        private void SetUpGameOver()
        {
            _gameOverView = new GameOverView(_playerColorProvider,
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

        private async Task SetUpCellMovesHighlights()
        {
            cellMoveHighlightsFactory.Init(cellMoveHighlightsConfig);
            var highlightsPlacer = new CellMovesHighlightsPlacer(grid, cellMoveHighlightsFactory);
            _cellMovesHighlightView = new CellMovesHighlightView(highlightsPlacer);
            var player = _game.GetPlayer(await _playerIdProvider.GetLocalPlayerId());
            var observer = new CellMovesHighlightObserver(_game, player);
            _cellMovesHighlightPresenter = new CellMovesHighlightPresenter(player,
                new CellMovesListProvider(_game), observer, _cellMovesHighlightView);
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
            var playerNumberProvider = new PlayerNumberProvider(playersList);
            var playerRotationProvider = new PlayerOrderRotationProvider(playerOrderRotations, playerNumberProvider);
        
            var randomTreeVisualCreator = new RandomTreeVisualCreator(new RangeRandomNumberGenerator(), treeVisualPrefabConfig, instantiator);
            var cachingTreeVisualCreator = new CachingTreeVisualCreator(randomTreeVisualCreator);
            var treeViewFactory = new TreeViewFactory(cachingTreeVisualCreator, treeViewPrefab, instantiator);

            var knightColorProvider = new PlayerObjectsColorProvider(knightColorConfig, _playerColorProvider);
            var knightVisualCreator = new KnightVisualCreator(knightVisualPrefabConfig, knightColorProvider, instantiator, playerRotationProvider);
            var knightAudioFactory = new SoundPlayerKnightAudioFactory(knightSoundsConfig, knightAudioPrefab, instantiator);
            var knightViewFactory = new KnightViewFactory(knightVisualCreator, knightAudioFactory, knightViewPrefab, instantiator);
            var castleColorProvider = new PlayerObjectsColorProvider(castleColorConfig, _playerColorProvider);
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
            var firstCastle = board[0, 0].GetContent().
                Find(x => x is CastleEntity) as CastleEntity;
            var secondCastle = board[
                    board.GetLength(0) - 1,
                    board.GetLength(1) - 1
                ].GetContent().
                Find(x => x is CastleEntity) as CastleEntity;
            var firstCastleColor = _playerColorProvider.GetPlayerColor(firstCastle.GetOwner());
           
            var bFirstCastleIsOurs = firstCastleColor == PlayerColor.Blue;

            var initialDurability = firstCastle.GetMaxDurability();
            blueCastleHeathBar.Init(initialDurability);
            redCastleHeathBar.Init(initialDurability);
            
            firstCastle.Hit += bFirstCastleIsOurs ? 
                blueCastleHeathBar.ApplyHit : redCastleHeathBar.ApplyHit;
            secondCastle.Hit += bFirstCastleIsOurs ? 
                redCastleHeathBar.ApplyHit : blueCastleHeathBar.ApplyHit;
        }

        private void SetUpClientMoves()
        {
            _movesView = new MovesView(_cellClickDetectors);
            var serverMovesApplier = new ServerMoveApplier(ClientsHolder.GetClient(ClientType.GameServerClient));
            ApproveMoveMessageHandler.SetDTOAccepter(serverMovesApplier);
            var localMovesApplier = new LocalMovesApplier(_game);
            var possibleMovesProvider = new PossibleMovesListProvider(_game);
            _clientMovesPresenter = new ClientMovesPresenter(_accessTokenProvider, serverMovesApplier, possibleMovesProvider,
                localMovesApplier, new MoveToDataConverter(), _movesView);
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
            var objectsColorProvider = new PlayerObjectsColorProvider(placedUnitsHighlightsColorConfig, _playerColorProvider);
            var underlineCreator = new ColoredHighlightCreator(coloredHighlightPrefabConfig, instantiator);
            _placedUnitsHighlightsView = new PlacedUnitsHighlightsView(grid, underlineCreator, objectsColorProvider);
            _placedUnitsHighlightsPresenter = new PlacedUnitsHighlightsPresenter(_game.GetBoard(), _placedUnitsHighlightsView);
        }
    
        private void SetUpNewUnitsHighlights()
        {
            var instantiator = new Instantiator();
            var objectsColorProvider = new PlayerObjectsColorProvider(newUnitsHighlightsColorConfig, _playerColorProvider);
            var underlineCreator = new ColoredHighlightCreator(newUnitsHighlightsPrefabConfig, instantiator);
            _newUnitsHighlightsView = new NewUnitsHighlightsView(grid, underlineCreator, objectsColorProvider);
            _newUnitsHighlightsPresenter = new NewUnitsHighlightsPresenter(_game, _newUnitsHighlightsView);
        }
    
        private void SetUpActionPointsGiving()
        {
            _actionPointsGivingView = new StubActionPointsView();
            _actionPointsGivingPresenter = new ActionPointsGivingPresenter(new PlayerProvider(_game),
                new ActionPointsGiver(_game), _actionPointsGivingView);
            var actionPointsGivingAccepter = new GiveActionPointsAccepter(_actionPointsGivingPresenter);
            GiveActionPointsMessageHandler.SetAccepter(actionPointsGivingAccepter);
        }

        private async Task SetUpCamera()
        {
            var playerId = await _playerIdProvider.GetLocalPlayerId();
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
            var playerToken = await _accessTokenProvider.GetAccessTokenAsync();
            var playerReadinessSender = new ReadinessSender(ClientsHolder.GetClient(ClientType.GameServerClient));
            playerReadinessSender.SendPlayerReadiness(playerToken);
        }
        
        private async Task SetUpActionPointsUI()
        {
            var bluePlayer = _game.GetPlayer(await _playerIdProvider.GetLocalPlayerId());
            var redPlayer = _game.GetPlayer(_game.GetAllPlayersIds().Find(id => id != bluePlayer.Id));
            _blueActionPointsUI = new ActionPointsUI(blueActionPointsText, blueActionPointsBanner, bluePlayer);
            _redActionPointsUI = new ActionPointsUI(redActionPointsText, redActionPointsBanner, redPlayer);
        }
    }
}
