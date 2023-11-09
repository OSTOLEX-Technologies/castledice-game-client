using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.MoveConverters;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Src;
using Src.GameplayPresenter;
using Src.GameplayPresenter.ActionPointsGiving;
using Src.GameplayPresenter.CellMovesHighlights;
using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using Src.GameplayPresenter.CellsContent;
using Src.GameplayPresenter.ClientMoves;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayPresenter.ServerMoves;
using Src.GameplayView.ActionPointsGiving;
using Src.GameplayView.CellMovesHighlights;
using Src.GameplayView.Cells;
using Src.GameplayView.CellsContent;
using Src.GameplayView.CellsContent.ContentCreation;
using Src.GameplayView.ClickDetection;
using Src.GameplayView.ClientMoves;
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
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject blueWinnerScreen;
    [SerializeField] private GameObject redWinnerScreen;
    [SerializeField] private GameObject drawScreen;
    [SerializeField] private GameObject currentPlayerBlueText;
    [SerializeField] private GameObject currentPlayerRedText;
    [SerializeField] private Transform secondPlayerCameraPosition;

    [SerializeField] private UnityContentViewProvider contentViewProvider;
    
    //Clicks detection
    [SerializeField] private UnityCellClickDetectorsConfig cellClickDetectorsConfig;
    [SerializeField] private UnityCellClickDetectorsFactory cellClickDetectorsFactory;
    private List<ICellClickDetector> _cellClickDetectors;
    private TouchInputHandler _touchInputHandler;
    private PlayerInputReader _inputReader;
    
    //Grid
    private SquareGridGenerator _gridGenerator;
    [SerializeField] private UnityGrid grid;
    [SerializeField] private UnitySquareGridGenerationConfig gridGenerationConfig;

    //Cells
    private SquareCellsViewGenerator3D _cellsViewGenerator;
    [SerializeField] private UnitySquareCellsFactory cellsFactory;
    [SerializeField] private UnitySquareCellAssetsConfig assetsConfig;

    //Content
    private CellsContentPresenter _cellContentPresenter;
    private CellsContentView _contentView;
    [SerializeField] private UnityCommonContentViewPrefabConfig commonContentConfig;
    [SerializeField] private UnityPlayerContentViewPrefabsConfig playerContentConfig;
    
    //Moves
    private ClientMovesView _clientMovesView;
    private ClientMovesPresenter _clientMovesPresenter;
    private ServerMovesPresenter _serverMovesPresenter;
    
    //Action points
    private ActionPointsGivingPresenter _actionPointsGivingPresenter;
    private ActionPointsGivingView _actionPointsGivingView;
    [SerializeField] private int popupDisappearTimeMilliseconds;
    [SerializeField] private UnityActionPointsPopup redActionPointsPopup;
    [SerializeField] private UnityActionPointsPopup blueActionPointsPopup;
    [SerializeField] private TextMeshProUGUI actionPointsText;
    
    //Move highlights
    private CellMovesHighlightPresenter _cellMovesHighlightPresenter;
    private CellMovesHighlightView _cellMovesHighlightView;
    [SerializeField] private UnityCellMoveHighlightsConfig cellMoveHighlightsConfig;
    [SerializeField] private UnityCellMoveHighlightsFactory cellMoveHighlightsFactory;

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
        SetUpMoveAppliedEvent();
        SetUpTurnSwitchedEvent();
        UpdateActionPoints();
        UpdateCurrentPlayerText();
        SetUpActionPointsEvent();
        SetUpCellMovesHighlights();
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

    private void SetUpMoveAppliedEvent()
    {
        _game.MoveApplied += OnMoveApplied;
    }

    private void SetUpTurnSwitchedEvent()
    {
        _game.TurnSwitched += OnTurnSwitched;
    }

    private void SetUpActionPointsEvent()
    {
        var firstPlayer = _game.GetPlayer(_game.GetAllPlayersIds()[0]);
        var secondPlayer = _game.GetPlayer(_game.GetAllPlayersIds()[1]);
        firstPlayer.ActionPoints.ActionPointsIncreased += OnActionPointsIncreased;
        secondPlayer.ActionPoints.ActionPointsIncreased += OnActionPointsIncreased;
    }

    private void OnActionPointsIncreased(object sender, int e)
    {
        UpdateActionPoints();
    }

    private void OnTurnSwitched(object sender, Game e)
    {
        UpdateActionPoints();
        UpdateCurrentPlayerText();
    }

    private void OnMoveApplied(object sender, AbstractMove e)
    {
        UpdateActionPoints();
        UpdateCurrentPlayerText();
    }

    private void UpdateCurrentPlayerText()
    {
        var currentPlayer = _game.GetCurrentPlayer();
        var playerColorProvider = new DuelPlayerColorProvider(Singleton<IPlayerDataProvider>.Instance);
        var playerColor = playerColorProvider.GetPlayerColor(currentPlayer);
        if (playerColor == PlayerColor.Blue)
        {
            currentPlayerBlueText.SetActive(true);
            currentPlayerRedText.SetActive(false);
        }
        else
        {
            currentPlayerBlueText.SetActive(false);
            currentPlayerRedText.SetActive(true);
        }
    }

    private void UpdateActionPoints()
    {
        var currentPlayer = _game.GetCurrentPlayer();
        var actionPoints = currentPlayer.ActionPoints.Amount;
        var actionPointsString = actionPoints.ToString();
        actionPointsText.text = actionPointsString;
    }
}
