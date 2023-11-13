using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.MoveConverters;
using castledice_game_logic;
using Src;
using Src.GameplayPresenter;
using Src.GameplayPresenter.ActionPointsCount;
using Src.GameplayPresenter.ActionPointsGiving;
using Src.GameplayPresenter.CellMovesHighlights;
using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using Src.GameplayPresenter.CellsContent;
using Src.GameplayPresenter.ClientMoves;
using Src.GameplayPresenter.CurrentPlayer;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;
using Src.GameplayPresenter.GameOver;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayPresenter.ServerMoves;
using Src.GameplayView.ActionPointsCount;
using Src.GameplayView.ActionPointsGiving;
using Src.GameplayView.CellMovesHighlights;
using Src.GameplayView.Cells;
using Src.GameplayView.CellsContent;
using Src.GameplayView.CellsContent.ContentCreation;
using Src.GameplayView.ClickDetection;
using Src.GameplayView.ClientMoves;
using Src.GameplayView.CurrentPlayer;
using Src.GameplayView.GameOver;
using Src.GameplayView.Grid;
using Src.GameplayView.Grid.GridGeneration;
using Src.GameplayView.PlayersColor;
using Src.NetworkingModule;
using Src.NetworkingModule.MessageHandlers;
using Src.NetworkingModule.Moves;
using Src.PlayerInput;
using TMPro;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

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
    
    [Header("Content")]
    [SerializeField] private UnityCommonContentViewPrefabConfig commonContentConfig;
    [SerializeField] private UnityPlayerContentViewPrefabsConfig playerContentConfig;
    [SerializeField] private UnityContentViewProvider contentViewProvider;
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
        var playersListProvider = new PlayersListProvider();
        var coordinateSpawnerProvider = new CoordinateContentSpawnerProvider(new ContentToCoordinateProvider());
        var matrixCellsGeneratorProvider = new MatrixCellsGeneratorProvider();
        var boardConfigProvider = new BoardConfigProvider(coordinateSpawnerProvider, matrixCellsGeneratorProvider);
        var placeablesConfigProvider = new PlaceablesConfigProvider();
        var decksListProvider = new DecksListProvider();
        var gameCreator = new GameCreator(playersListProvider, boardConfigProvider, placeablesConfigProvider,
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
        var playerPrefabProvider =
            new PlayerContentViewPrefabProvider(new DuelPlayerColorProvider(Singleton<IPlayerDataProvider>.Instance),
                playerContentConfig);
        contentViewProvider.Init(playerPrefabProvider, commonContentConfig);
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
