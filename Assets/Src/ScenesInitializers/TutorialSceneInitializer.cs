using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using castledice_game_data_logic;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;
using Src.Components;
using Src.GameplayPresenter.CellMovesHighlights;
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
using Src.GameplayPresenter.GameOver;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayPresenter.NewUnitsHighlights;
using Src.GameplayPresenter.PlacedUnitsHighlights;
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
using Src.General.MoveConditions;
using Src.General.NumericSequences;
using Src.LoadingScenes;
using Src.PlayerInput;
using Src.Prototypes;
using Src.Prototypes.NewActionPoints;
using Src.PVE;
using Src.PVE.Calculators;
using Src.PVE.Checkers;
using Src.PVE.GameSituations;
using Src.PVE.MoveSearchers.TraitBasedSearchers;
using Src.PVE.MoveSearchers.TraitBasedSearchers.TraitsEvaluators;
using Src.PVE.Providers;
using Src.TimeManagement;
using Src.Tutorial;
using Src.Tutorial.ActionPointsGiving;
using Src.Tutorial.BotConfiguration;
using Tests.EditMode.GeneralTests;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using Button = UnityEngine.UI.Button;
using Vector2Int = castledice_game_logic.Math.Vector2Int;
using CastleEntity = castledice_game_logic.GameObjects.Castle;

namespace Src.ScenesInitializers
{
    public class TutorialSceneInitializer : MonoBehaviour
    {
        public const string TutorialPassedPlayerPrefsKey = "TutorialPassed";
        
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

        [Header("SceneLoader")]
        [SerializeField] private SceneLoader sceneLoader;        

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
        
        [Header("Action points giving")]
        [SerializeField] private IntSequenceConfig playerActionPointsSequenceConfig;
        [SerializeField] private IntSequenceConfig enemyActionPointsSequenceConfig;
        private TutorialActionPointsGivingPresenter _actionPointsGivingPresenter;
        private IActionPointsGivingView _actionPointsGivingView;

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
        
        [Header("Move highlights")]
        [SerializeField] private UnityCellMoveHighlightsConfig cellMoveHighlightsConfig;
        [SerializeField] private UnityCellMoveHighlightsFactory cellMoveHighlightsFactory;
        private CellMovesHighlightPresenter _cellMovesHighlightPresenter;
        private CellMovesHighlightView _cellMovesHighlightView;
        
        [Header("Game over")]
        [SerializeField] private GameObject blueWinnerScreen;
        [SerializeField] private GameObject redWinnerScreen;
        [SerializeField] private GameObject drawScreen;
        private GameOverPresenter _gameOverPresenter;
        private GameOverView _gameOverView;

        [Header("Action points UI")] 
        [SerializeField] private GameObject playerBanner;
        [SerializeField] private TextMeshProUGUI playerActionPointsText;
        [SerializeField] private GameObject enemyBanner;
        [SerializeField] private TextMeshProUGUI enemyActionPointsText;
        private ActionPointsUI _playerActionPointsUI;
        private ActionPointsUI _enemyActionPointsUI;
        
        private DuelPlayerColorProvider _playerColorProvider;

        [Header("Bot configuration")]
        [SerializeField] private int botMoveDelayMilliseconds;
        [SerializeField] private AllowedPositionsScenariosConfig allowedPositionsScenariosConfig;
        [SerializeField] private IntSequenceConfig botMovesDelaysConfig;
        private Bot _bot;
        
        [Header("Castles health bars")]
        [SerializeField] private CastleHealthBar blueCastleHeathBar;
        [SerializeField] private CastleHealthBar redCastleHeathBar;
        
        [Header("Audio")]
        [SerializeField] private AudioMixer mixer;

        [Header("Transitions")] 
        [SerializeField] private float introFadeSeconds;
        [SerializeField] private CanvasGroup introCanvasGroup;
        
        [Header("Tutorial controller")]
        [SerializeField] private TutorialController tutorialController;
        [SerializeField] private Button screenClickDetector;
        private BlockableRaycaster3D _raycaster;
        

        private void Start()
        {
            mixer.SetFloat("KnightsVolume", -80);

            SetUpGameAndPlayers();
            SetUpPlayerColorProvider();
            SetUpInputHandling();
            SetUpGrid();
            SetUpCells();
            SetUpContent();
            SetupCastleHealth();
            SetUpClickDetectors();
            SetUpPlayerMoves();
            SetUpActionPointsGiving();
            SetUpBot();
            SetUpPlacedUnitsHighlights();
            SetUpNewUnitsHighlights();
            SetUpCellMovesHighlights();
            SetUpGameOver();
            SetUpActionPointsUI();
            HandleGameOver();
            SetUpController();
            
            GiveActionPointsToCurrentPlayer();
            
            StartCoroutine(Intro());
        }

        private IEnumerator Intro()
        {
            yield return new WaitForSeconds(0.2f);
            var elapsedTime = 0f;
            while (elapsedTime < introFadeSeconds)
            {
                elapsedTime += Time.deltaTime;
                introCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / introFadeSeconds);
                yield return null;
            }
            mixer.SetFloat("KnightsVolume", 0);
        }

        private void SetUpController()
        {
            _movesPresenter.WrongMovePicked += (object sender, AbstractMove move) => tutorialController.WrongMoveApplied();
            _movesPresenter.RightMovePicked += (object sender, AbstractMove move) => tutorialController.RightMoveApplied();
            screenClickDetector.onClick.AddListener(tutorialController.ScreenClicked);
            tutorialController.Init(_raycaster);
        }


        private void SetUpGameAndPlayers()
        {
            _gameStartData = gameStartDataConfig.GetGameStartData(playerId, enemyId);
            var gameCreator = GetGameCreator();
            _game = gameCreator.CreateGame();
            _player = _game.GetPlayer(playerId);
            _enemy = _game.GetPlayer(enemyId);
        }
        

        private void SetUpPlayerColorProvider()
        {
            _playerColorProvider = new DuelPlayerColorProvider(_player);
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
            var blockableRaycaster = new BlockableRaycaster3D(raycaster);
            _raycaster = blockableRaycaster;
            _touchInputHandler = new TouchInputHandler(cameraWrapper, _raycaster);
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
            _movesPresenter.WrongMovePicked += (object sender, AbstractMove move) => Debug.Log("Wrong move picked");
            _movesPresenter.RightMovePicked += (object sender, AbstractMove move) => Debug.Log("Right move picked");
        }


        
        private void SetUpActionPointsGiving()
        {
            _actionPointsGivingView = new StubActionPointsView();
            var playerActionPointsSequence = new IntSequence(playerActionPointsSequenceConfig.Sequence.ToList(), playerActionPointsSequenceConfig.DefaultNumber);
            var enemyActionPointsSequence = new IntSequence(enemyActionPointsSequenceConfig.Sequence.ToList(), enemyActionPointsSequenceConfig.DefaultNumber);
            var sequenceProvider = new DictPlayerIntSequenceProvider(new Dictionary<Player, IIntSequence>
            {
                {_player, playerActionPointsSequence},
                {_enemy, enemyActionPointsSequence}
            });
            var actionPointsGenerator = new SequenceActionPointsGenerator(sequenceProvider);
            _actionPointsGivingPresenter = new TutorialActionPointsGivingPresenter(_actionPointsGivingView, actionPointsGenerator, _game);
        }
        
        private void GiveActionPointsToCurrentPlayer()
        {
            _actionPointsGivingPresenter.GiveActionPointsToCurrentPlayer();
        }

        private void SetUpBot()
        {
            var moveDelay = TimeSpan.FromMilliseconds(botMoveDelayMilliseconds);
            var localMoveApplier = new LocalMovesApplier(_game);
            var situationsToPositions = new Dictionary<IGameSituation, List<Vector2Int>>();
            var unconnectedValuesCutter = new DfsUnconnectedValuesCutter();
            var board = _game.GetBoard();
            var unitChecker = new PlayerUnitChecker();
            var unitsPositionsSearcher = new UnitsPositionsSearcher(board, unitChecker);
            var baseCaptureChecker = new BaseCaptureCondition(board);
            var playerBaseChecker = new PlayerBaseChecker();
            var basePositionsCalculator = new BasePositionsCalculator(board, playerBaseChecker, baseCaptureChecker);
            var boardSize = new Vector2Int(board.GetLength(0), board.GetLength(1));
            var armyStateCalculator = new SimpleArmyStateCalculator(unconnectedValuesCutter, unitsPositionsSearcher, basePositionsCalculator, boardSize);
            var occupiedPositionsCalculator = new OccupiedPositionsCalculator(armyStateCalculator);
            foreach (var scenario in allowedPositionsScenariosConfig.Scenarios)
            {
                var playerOccupiedPositions = scenario.EnemyPositions.ConvertToGameLogicVector2IntList();
                var botAllowedPositions = scenario.AllowedPositions.ConvertToGameLogicVector2IntList();
                var situation =
                    new OccupiedPositionsSituation(playerOccupiedPositions, occupiedPositionsCalculator, _player);
                situationsToPositions.Add(situation, botAllowedPositions);
            }
            var allowedPositionsProvider = new SituationalPositionsProvider(situationsToPositions);
            var positionValidityEvaluator = new PositionValidityEvaluator(allowedPositionsProvider);
            var traitsEvaluator = new MovesListTraitsEvaluator(
                positionValidityEvaluator, 
                positionValidityEvaluator, 
                positionValidityEvaluator, 
                positionValidityEvaluator,
                positionValidityEvaluator);
            var movesTraitsNormalizer = new MovesTraitsNormalizer();
            var totalPossibleMovesProvider = new TotalPossibleMovesProvider(_game);
            var randomNumberGenerator = new RangeRandomNumberGenerator();
            var bestMoveSearcher = new TraitsWeightedSumMoveSearcher(
                traitsEvaluator, 
                movesTraitsNormalizer, 
                totalPossibleMovesProvider, 
                randomNumberGenerator, 
                new Dictionary<IGameSituation, MoveTraitsValues>(),
                new MoveTraitsValues
                {
                    Aggressiveness = 1,
                    Defensiveness = 1,
                    Destructiveness = 1,
                    Enhanciveness = 1,
                    Harmfulness = 1
                });
            
            //Configuring delays
            var delayer = new AsyncDelayer();
            var delaysSequence = new IntSequence(botMovesDelaysConfig.Sequence.ToList(), botMovesDelaysConfig.DefaultNumber);
            _bot = new ConfigurableDelaysBot(localMoveApplier, bestMoveSearcher, _game, _enemy, delayer, delaysSequence);
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
        
        private void SetUpCellMovesHighlights()
         {
             cellMoveHighlightsFactory.Init(cellMoveHighlightsConfig);
             var highlightsPlacer = new CellMovesHighlightsPlacer(grid, cellMoveHighlightsFactory);
             _cellMovesHighlightView = new CellMovesHighlightView(highlightsPlacer);
             var cellMovesListProvider = new CellMovesListProvider(_game);
             var observer = new CellMovesHighlightObserver(_game, _player);
             _cellMovesHighlightPresenter = new CellMovesHighlightPresenter(_player, cellMovesListProvider, observer, _cellMovesHighlightView);
         }
        
        private void SetUpGameOver()
        {
            _gameOverView = new GameOverView(_playerColorProvider,
                blueWinnerScreen, redWinnerScreen, drawScreen);
            _gameOverPresenter = new GameOverPresenter(_game, _gameOverView);
        }
        
        private void SetUpActionPointsUI()
        {
            _playerActionPointsUI = new ActionPointsUI(playerActionPointsText, playerBanner, _player);
            _enemyActionPointsUI = new ActionPointsUI(enemyActionPointsText, enemyBanner, _enemy);
        }

        private void HandleGameOver()
        {
            _game.Win += (_, _) =>
            {
                PlayerPrefs.SetInt(TutorialPassedPlayerPrefsKey, 1);
            };
        }

        public void LoadAuthSceneAfterWin()
        {
            sceneLoader.LoadSceneWithTransition(SceneType.Auth);
        }
    }
}
