using System.Collections;
using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.MoveConverters;
using castledice_game_logic;
using Src;
using Src.GameplayPresenter;
using Src.GameplayPresenter.ActionPointsGiving;
using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using Src.GameplayPresenter.CellsContent;
using Src.GameplayPresenter.ClientMoves;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayPresenter.ServerMoves;
using Src.GameplayView.ActionPointsGiving;
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
using Src.Stubs;
using UnityEngine;

public class DuelGameSceneInitializer : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private UnityGrid grid;
    [SerializeField] private int popupDisappearTimeMilliseconds;
    [SerializeField] private UnityActionPointsPopup redActionPointsPopup;
    [SerializeField] private UnityActionPointsPopup blueActionPointsPopup;
    [SerializeField] private UnitySquareCellsFactory cellsFactory;
    [SerializeField] private UnitySquareGridGenerationConfig gridGenerationConfig;
    [SerializeField] private UnitySquareCellAssetsConfig assetsConfig;
    [SerializeField] private UnityCommonContentViewPrefabConfig commonContentConfig;
    [SerializeField] private UnityPlayerContentViewPrefabsConfig playerContentConfig;
    [SerializeField] private UnityContentViewProvider contentViewProvider;
    [SerializeField] private UnityCellClickDetectorsConfig cellClickDetectorsConfig;
    [SerializeField] private UnityCellClickDetectorsFactory cellClickDetectorsFactory;
    private List<ICellClickDetector> _cellClickDetectors;
    private TouchInputHandler _touchInputHandler;
    private PlayerInputReader _inputReader;
    private SquareGridGenerator _gridGenerator;
    private SquareCellsViewGenerator3D _cellsViewGenerator;
    private CellsContentPresenter _cellContentPresenter;
    private CellsContentView _contentView;
    private ClientMovesView _clientMovesView;
    private ClientMovesPresenter _clientMovesPresenter;
    private ServerMovesPresenter _serverMovesPresenter;
    private ActionPointsGivingPresenter _actionPointsGivingPresenter;
    private ActionPointsGivingView _actionPointsGivingView;

    private Game _game;
    private GameStartData _gameStartData;

    private void Start()
    {
        SetUpGame();
        SetUpInput();
        SetUpGrid();
        SetUpClickDetectors();
        SetUpContent();
        SetUpCells();
        SetUpClientMoves();
        SetUpServerMoves();
        SetUpActionPointsGiving();
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
        var gameCreator = new GameCreator(playersListProvider, boardConfigProvider, placeablesConfigProvider, decksListProvider);
        _game = gameCreator.CreateGame(_gameStartData);
        _game.GiveActionPointsToPlayer(1, 6);
    }
    
    private void SetUpInput()
    {
        var cameraWrapper = new CameraWrapper(camera);
        var raycaster = new Raycaster3D(new RaycastHitProvider());
        _touchInputHandler = new TouchInputHandler(cameraWrapper, raycaster);
        _inputReader = new PlayerInputReader(_touchInputHandler);
        _inputReader.Enable();
    }
    

    private void SetUpGrid()
    {
        _gridGenerator = new SquareGridGenerator(grid, gridGenerationConfig);
        _gridGenerator.GenerateGrid(_gameStartData.BoardData.CellsPresence);
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
            new PlayerContentViewPrefabProvider(new DuelPlayerColorProvider(Singleton<IPlayerDataProvider>.Instance), playerContentConfig);
        contentViewProvider.Init(playerPrefabProvider, commonContentConfig);
        _contentView = new CellsContentView(grid, contentViewProvider);
        _cellContentPresenter = new CellsContentPresenter(_contentView, _game.GetBoard());
    }

    private void SetUpClientMoves()
    {
        _clientMovesView = new ClientMovesView(_cellClickDetectors);
        var playerDataProvider = new PlayerDataProviderStub();
        var serverMovesApplier = new ServerMoveApplier(ClientsHolder.GetClient(ClientType.GameServerClient));
        var localMovesApplier = new LocalMovesApplier(_game);
        var possibleMovesProvider = new PossibleMovesListProvider(_game);
        _clientMovesPresenter = new ClientMovesPresenter(playerDataProvider, serverMovesApplier, possibleMovesProvider, localMovesApplier, new MoveToDataConverter(), _clientMovesView);
    }

    private void SetUpServerMoves()
    {
        _serverMovesPresenter = new ServerMovesPresenter(new LocalMovesApplier(_game), new DataToMoveConverter(_game.PlaceablesFactory), new PlayerProvider(_game));
        var movesAccepter = new ServerMoveAccepter(_serverMovesPresenter);
        MoveFromServerMessageHandler.SetDTOAccepter(movesAccepter);
    }

    private void SetUpActionPointsGiving()
    {
        var popupsProvider = new ActionPointsPopupsHolder(blueActionPointsPopup, redActionPointsPopup);
        var popupDemonstrator = new ActionPointsPopupDemonstrator(popupsProvider, popupDisappearTimeMilliseconds);
        _actionPointsGivingView = new ActionPointsGivingView(new DuelPlayerColorProvider(Singleton<IPlayerDataProvider>.Instance), popupDemonstrator);
        _actionPointsGivingPresenter = new ActionPointsGivingPresenter(new PlayerProvider(_game),
            new ActionPointsGiver(_game), _actionPointsGivingView);
        var actionPointsGivingAccepter = new GiveActionPointsAccepter(_actionPointsGivingPresenter);
        GiveActionPointsMessageHandler.SetAccepter(actionPointsGivingAccepter);
    }
}
