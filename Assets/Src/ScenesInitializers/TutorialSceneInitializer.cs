using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_logic;
using castledice_game_logic.Math;
using Src.Caching;
using Src.GameplayPresenter;
using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using Src.GameplayPresenter.CellsContent;
using Src.GameplayPresenter.DestroyedContent;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayView;
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
using Src.GameplayView.Grid;
using Src.GameplayView.Grid.GridGeneration;
using Src.GameplayView.PlayerObjectsColor;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersNumbers;
using Src.GameplayView.PlayersRotations.RotationsByOrder;
using Src.PlayerInput;
using Src.TimeManagement;
using Src.Tutorial;
using UnityEngine;

namespace Src.ScenesInitializers
{
    public class TutorialSceneInitializer : MonoBehaviour
    {
        [Header("Ids")]
        [SerializeField] private int playerId = 0;
        [SerializeField] private int enemyId = 1;
        private Player _player;
        private Player _enemy;
        
        [Header("Game data config")]
        [SerializeField] private TutorialGameStartDataConfig gameStartDataConfig;
        private Game _game;
        private GameStartData _gameStartData;

        [Header("Cameras")]
        [SerializeField] private Camera playerCamera;


        [Header("Clicks detection")] 
        [SerializeField] private CellClickDetectorsConfig cellClickDetectorsConfig;
        [SerializeField] private CellClickDetectorsFactory cellClickDetectorsFactory;
        private List<ICellClickDetector> _cellClickDetectors;
        private TouchInputHandler _touchInputHandler;
        private PlayerInputReader _playerInputReader;

        [Header("Grid")] 
        [SerializeField] private GameObjectsGrid grid;
        [SerializeField] private SquareGridGenerationConfig gridGenerationConfig;
        private SquareGridGenerator _gridGenerator;
        
        [Header("Cells")]
        [SerializeField] private SquareCellsFactory cellsFactory;
        [SerializeField] private SquareCellAssetsConfig cellAssetsConfig;
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
        
        [Header("Destroyed content")]
        [SerializeField] private TransparencyConfig destroyedContentTransparencyConfig;
        private DestroyedContentView _destroyedContentView;
        private DestroyedContentPresenter _destroyedContentPresenter;
        
        [Header("Player moves configs")]
        [SerializeField] private PositionMoveConditionsListConfig positionMoveConditionsListConfig;
        private MovesView _movesView;
        private TutorialMovesPresenter _movesPresenter;
        
        private void Start()
        {
            SetUpGameAndPlayers();
            SetUpInputHandling();
            SetUpGrid();
            SetUpCells();
            SetUpClickDetectors();
            SetUpPlayerMoves();
            SetUpContent();
        }

        private void SetUpGameAndPlayers()
        {
            _gameStartData = gameStartDataConfig.GetGameStartData(playerId, enemyId);
            var gameCreator = GetGameCreator();
            _game = gameCreator.CreateGame();
            _player = _game.GetPlayer(playerId);
            _enemy = _game.GetPlayer(enemyId);
        }

        private TutorialGameCreator GetGameCreator()
        {
            var playerTimerCreator = new InfinityPlayerTimerCreator();
            var playerCreator = new PlayerCreator(playerTimerCreator);
            var playersListCreator = new PlayersListCreator(playerCreator);
            var contentToCoordinateCreator = new ContentToCoordinateCreator();
            var spawnersCreator = new CoordinateContentSpawnerCreator(contentToCoordinateCreator);
            var cellsGeneratorCreator = new MatrixCellsGeneratorCreator();
            var boardConfigCreator = new BoardConfigCreator(spawnersCreator, cellsGeneratorCreator);
            var placeablesConfigCreator = new PlaceablesConfigCreator();
            var turnSwitchConditionsConfigCreator = new TurnSwitchConditionsConfigCreator();
            var gameConstructorWrapper = new GameConstructorWrapper();
            var gameBuilder = new GameBuilder(gameConstructorWrapper);
            var gameCreator = new GameCreator(playersListCreator, boardConfigCreator, placeablesConfigCreator, turnSwitchConditionsConfigCreator, gameBuilder);
            var tutorialGameCreator = new TutorialGameCreator(gameCreator, gameStartDataConfig, playerId, enemyId);
            return tutorialGameCreator;
        }

        private void SetUpInputHandling()
        {
            var cameraWrapper = new CameraWrapper(playerCamera);
            var raycaster = new Raycaster3D(new RaycastHitProvider());
            _touchInputHandler = new TouchInputHandler(cameraWrapper, raycaster);
            _playerInputReader = new PlayerInputReader(_touchInputHandler);
            _playerInputReader.Enable();
        }

        private void SetUpGrid()
        {
            _gridGenerator = new SquareGridGenerator(grid, gridGenerationConfig);
            _gridGenerator.GenerateGrid(_gameStartData.BoardData.CellsPresence);
        }

        private void SetUpCells()
        {
            cellsFactory.Init(cellAssetsConfig);
            _cellsViewGenerator = new SquareCellsViewGenerator3D(cellsFactory, grid);
            var cellViewMapGenerator = new SquareCellViewMapGenerator(cellAssetsConfig);
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
        
        private void SetUpClickDetectors()
        {
            cellClickDetectorsFactory.Init(cellClickDetectorsConfig);
            var placer = new CellClickDetectorsPlacer(grid, cellClickDetectorsFactory);
            _cellClickDetectors = placer.PlaceDetectors();
        }

        private void SetUpPlayerMoves()
        {
            _movesView = new MovesView(_cellClickDetectors);
            var possibleMovesListProvider = new PossibleMovesListProvider(_game);
            var localMoveApplier = new LocalMovesApplier(_game);
            var moveConditionsSequence =
                new ListMoveConditionsSequence(positionMoveConditionsListConfig.GetMoveConditions());
            _movesPresenter = new TutorialMovesPresenter(_movesView, possibleMovesListProvider, localMoveApplier, moveConditionsSequence, playerId);
        }
    }
}
