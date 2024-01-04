using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.MoveConverters;
using castledice_game_logic;
using castledice_game_logic.Math;
using Src;
using Src.Caching;
using Src.GameplayPresenter;
using Src.GameplayPresenter.ActionPointsCount;
using Src.GameplayPresenter.ActionPointsGiving;
using Src.GameplayPresenter.CellMovesHighlights;
using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using Src.GameplayPresenter.CellsContent;
using Src.GameplayPresenter.ClientMoves;
using Src.GameplayPresenter.CurrentPlayer;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.Creators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;
using Src.GameplayPresenter.GameCreation.Creators.DecksListCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;
using Src.GameplayPresenter.GameOver;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayPresenter.ServerMoves;
using Src.GameplayView;
using Src.GameplayView.ActionPointsCount;
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
using Src.GameplayView.CurrentPlayer;
using Src.GameplayView.GameOver;
using Src.GameplayView.Grid;
using Src.GameplayView.Grid.GridGeneration;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersNumbers;
using Src.GameplayView.PlayersRotations.RotationsByColor;
using Src.GameplayView.PlayersRotations.RotationsByOrder;
using Src.NetworkingModule;
using Src.NetworkingModule.MessageHandlers;
using Src.NetworkingModule.Moves;
using Src.PlayerInput;
using TMPro;
using UnityEngine;

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
    [SerializeField] private UnityCellClickDetectorsConfig cellClickDetectorsConfig;
    [SerializeField] private UnityCellClickDetectorsFactory cellClickDetectorsFactory;
    private List<ICellClickDetector> _cellClickDetectors;
    private TouchInputHandler _touchInputHandler;
    private PlayerInputReader _inputReader;
    
    [Header("Grid")]
    [SerializeField] private UnityGrid grid;
    [SerializeField] private UnitySquareGridGenerationConfig gridGenerationConfig;
    private SquareGridGenerator _gridGenerator;
    
    [Header("Cells")]
    [SerializeField] private UnitySquareCellsFactory cellsFactory;
    [SerializeField] private UnitySquareCellAssetsConfig assetsConfig;
    private SquareCellsViewGenerator3D _cellsViewGenerator;
    
    [Header("Content configs")]
    [SerializeField] private ScriptablePlayerOrderRotationConfig playerOrderRotations;
    [Header("Knight configs")]
    [SerializeField] private KnightSoundsConfig knightSoundsConfig;
    [SerializeField] private SoundPlayerKnightAudio knightAudioPrefab;
    [SerializeField] private KnightView knightViewPrefab;
    [SerializeField] private KnightModelPrefabConfig knightModelPrefabConfig;
    [Header("Tree config")]
    [SerializeField] private TreeView treeViewPrefab;
    [SerializeField] private TreeModelPrefabsConfig treeModelPrefabConfig;
    [Header("Castle config")]
    [SerializeField] private CastleSoundsConfig castleSoundsConfig;
    [SerializeField] private CastleView castleViewPrefab;
    [SerializeField] private SoundPlayerCastleAudio castleAudioPrefab;
    [SerializeField] private CastleModelPrefabConfig castleModelPrefabConfig;
    private CellsContentPresenter _cellContentPresenter;
    private CellsContentView _contentView;
    
    //Moves
    private ClientMovesView _clientMovesView;
    private ClientMovesPresenter _clientMovesPresenter;
    private ServerMovesPresenter _serverMovesPresenter;
    
    [Header("Action points giving")]
    [SerializeField] private int popupDisappearTimeMilliseconds;
    [SerializeField] private UnityActionPointsPopup redActionPointsPopup;
    [SerializeField] private UnityActionPointsPopup blueActionPointsPopup;
    private ActionPointsGivingPresenter _actionPointsGivingPresenter;
    private ActionPointsGivingView _actionPointsGivingView;
    
    [Header("Action points count")]
    [SerializeField] private TextMeshProUGUI actionPointsLabel;
    [SerializeField] private TextMeshProUGUI actionPointsText;
    private ActionPointsCountPresenter _actionPointsCountPresenter;
    private ActionPointsCountView _actionPointsCountView;
    
    [Header("Move highlights")]
    [SerializeField] private UnityCellMoveHighlightsConfig cellMoveHighlightsConfig;
    [SerializeField] private UnityCellMoveHighlightsFactory cellMoveHighlightsFactory;
    private CellMovesHighlightPresenter _cellMovesHighlightPresenter;
    private CellMovesHighlightView _cellMovesHighlightView;

    [Header("Current player label")] 
    [SerializeField] private GameObject bluePlayerLabel;
    [SerializeField] private GameObject redPlayerLabel;
    private CurrentPlayerPresenter _currentPlayerPresenter;
    private CurrentPlayerView _currentPlayerView;


    private Game _game;
    private GameStartData _gameStartData;

    private void Start()
    {
        SetUpGame();
        SetUpInput();
        SetUpGrid();
        SetUpContent();
        SetUpCells();
        SetUpClickDetectors();
        SetUpClientMoves();
        SetUpServerMoves();
        SetUpActionPointsGiving();
        SetUpGameOverProcessing();
        SetUpCamera();
        SetUpCellMovesHighlights();
        SetUpActionPointsCount();
        SetUpCurrentPlayerLabel();
        SetUpCurrentPlayerLabel();
        SetUpGameOver();
        NotifyPlayerIsReady();
    }

    private void SetUpGame()
    {
        _gameStartData = Singleton<GameStartData>.Instance;
        var playersListProvider = new PlayersListCreator();
        var coordinateSpawnerProvider = new CoordinateContentSpawnerCreator(new ContentToCoordinateCreator());
        var matrixCellsGeneratorProvider = new MatrixCellsGeneratorCreator();
        var boardConfigProvider = new BoardConfigCreator(coordinateSpawnerProvider, matrixCellsGeneratorProvider);
        var placeablesConfigProvider = new PlaceablesConfigCreator();
        var decksListProvider = new DecksListCreator();
        var turnSwitchConditionsConfigProvider = new TurnSwitchConditionsConfigCreator();
        var gameCreator = new GameCreator(playersListProvider, boardConfigProvider, placeablesConfigProvider, turnSwitchConditionsConfigProvider,
            decksListProvider);
        _game = gameCreator.CreateGame(_gameStartData);
    }

    private void SetUpGameOver()
    {
        _gameOverView = new GameOverView(new DuelPlayerColorProvider(Singleton<IPlayerDataProvider>.Instance),
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
        var playerDataProvider = Singleton<IPlayerDataProvider>.Instance;
        _cellMovesHighlightPresenter = new CellMovesHighlightPresenter(playerDataProvider,
            new CellMovesListProvider(_game), _game, _cellMovesHighlightView);
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
        var playerDataProvider = Singleton<IPlayerDataProvider>.Instance;
        var playerColorProvider = new DuelPlayerColorProvider(playerDataProvider);
        var playerNumberProvider = new PlayerNumberProvider(playersList);
        var playerRotationProvider = new PlayerOrderRotationProvider(playerOrderRotations, playerNumberProvider);
        
        var treeModelProvider = new RandomTreeModelProvider(new RangeRandomNumberGenerator(), treeModelPrefabConfig, instantiator);
        var treeViewFactory = new TreeViewFactory(treeModelProvider, treeViewPrefab, instantiator);

        var knightModelProvider = new ColoredKnightModelProvider(playerColorProvider, knightModelPrefabConfig, instantiator);
        var knightAudioFactory = new SoundPlayerKnightAudioFactory(knightSoundsConfig, knightAudioPrefab, instantiator);
        var knightViewFactory = new KnightViewFactory(playerRotationProvider, knightModelProvider, knightAudioFactory, knightViewPrefab, instantiator);

        var castleModelProvider = new ColoredCastleModelProvider(playerColorProvider, castleModelPrefabConfig, instantiator);
        var castleAudioFactory = new SoundPlayerCastleAudioFactory(castleSoundsConfig, castleAudioPrefab, instantiator);
        var castleViewFactory = new CastleViewFactory(castleModelProvider, castleAudioFactory, castleViewPrefab, instantiator);
        
        var contentViewProvider = new ContentViewProvider(treeViewFactory, knightViewFactory, castleViewFactory);
        _contentView = new CellsContentView(grid, contentViewProvider);
        _cellContentPresenter = new CellsContentPresenter(_contentView, _game.GetBoard());
    }

    private void SetUpClientMoves()
    {
        _clientMovesView = new ClientMovesView(_cellClickDetectors);
        var playerDataProvider = Singleton<IPlayerDataProvider>.Instance;
        var serverMovesApplier = new ServerMoveApplier(ClientsHolder.GetClient(ClientType.GameServerClient));
        ApproveMoveMessageHandler.SetDTOAccepter(serverMovesApplier);
        var localMovesApplier = new LocalMovesApplier(_game);
        var possibleMovesProvider = new PossibleMovesListProvider(_game);
        _clientMovesPresenter = new ClientMovesPresenter(playerDataProvider, serverMovesApplier, possibleMovesProvider,
            localMovesApplier, new MoveToDataConverter(), _clientMovesView);
    }

    private void SetUpServerMoves()
    {
        _serverMovesPresenter = new ServerMovesPresenter(new LocalMovesApplier(_game),
            new DataToMoveConverter(_game.PlaceablesFactory), new PlayerProvider(_game));
        var movesAccepter = new ServerMoveAccepter(_serverMovesPresenter);
        MoveFromServerMessageHandler.SetDTOAccepter(movesAccepter);
    }

    private void SetUpActionPointsGiving()
    {
        var popupsProvider = new ActionPointsPopupsHolder(blueActionPointsPopup, redActionPointsPopup);
        var popupDemonstrator = new ActionPointsPopupDemonstrator(popupsProvider, popupDisappearTimeMilliseconds);
        _actionPointsGivingView =
            new ActionPointsGivingView(new DuelPlayerColorProvider(Singleton<IPlayerDataProvider>.Instance),
                popupDemonstrator);
        _actionPointsGivingPresenter = new ActionPointsGivingPresenter(new PlayerProvider(_game),
            new ActionPointsGiver(_game), _actionPointsGivingView);
        var actionPointsGivingAccepter = new GiveActionPointsAccepter(_actionPointsGivingPresenter);
        GiveActionPointsMessageHandler.SetAccepter(actionPointsGivingAccepter);
    }

    private void SetUpActionPointsCount()
    {
        var playerDataProvider = Singleton<IPlayerDataProvider>.Instance;
        _actionPointsCountView = new ActionPointsCountView(actionPointsText, actionPointsLabel);
        _actionPointsCountPresenter = new ActionPointsCountPresenter(playerDataProvider, _game,
            _actionPointsCountView);
    }

    //TODO: Refactor this and move game over logic into separate class
    private void SetUpGameOverProcessing()
    {
        _game.Win += OnWin;
        _game.Draw += OnDraw;
    }

    private void OnDraw(object sender, Game e)
    {
        drawScreen.SetActive(true);
    }

    private void OnWin(object sender, (Game game, Player player) e)
    {
        var colorProvider = new DuelPlayerColorProvider(Singleton<IPlayerDataProvider>.Instance);
        if (colorProvider.GetPlayerColor(e.player) == PlayerColor.Blue)
        {
            blueWinnerScreen.SetActive(true);
        }
        else
        {
            redWinnerScreen.SetActive(true);
        }
    }

    private void SetUpCamera()
    {
        var playerId = Singleton<IPlayerDataProvider>.Instance.GetId();
        var playerIndex = _game.GetAllPlayersIds().IndexOf(playerId);
        if (playerIndex == 1)
        {
            camera.transform.SetParent(secondPlayerCameraPosition);
            camera.transform.localPosition = Vector3.zero;
            camera.transform.localEulerAngles = Vector3.zero;
        }
    }
    
    private void SetUpCurrentPlayerLabel()
    {
        _currentPlayerView = new CurrentPlayerView(new DuelPlayerColorProvider(Singleton<IPlayerDataProvider>.Instance),
            bluePlayerLabel, redPlayerLabel);
        _currentPlayerPresenter = new CurrentPlayerPresenter(_game, _currentPlayerView);
        _currentPlayerPresenter.ShowCurrentPlayer();
    }

    private void NotifyPlayerIsReady()
    {
        var playerDataProvider = Singleton<IPlayerDataProvider>.Instance;
        var playerToken = playerDataProvider.GetAccessToken();
        var playerReadinessSender = new ReadinessSender(ClientsHolder.GetClient(ClientType.GameServerClient));
        playerReadinessSender.SendPlayerReadiness(playerToken);
    }
}
