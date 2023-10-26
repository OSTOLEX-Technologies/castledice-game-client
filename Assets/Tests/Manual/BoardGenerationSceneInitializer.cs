using System.Collections.Generic;
using castledice_game_logic;
using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using Src.GameplayPresenter.CellsContent;
using Src.GameplayPresenter.ClientMoves;
using Src.GameplayView.Cells;
using Src.GameplayView.CellsContent;
using Src.GameplayView.CellsContent.ContentCreation;
using Src.GameplayView.ClickDetection;
using Src.GameplayView.ClientMoves;
using Src.GameplayView.Grid;
using Src.GameplayView.Grid.GridGeneration;
using Src.PlayerInput;
using Tests.Manual;
using static Tests.ObjectCreationUtility;
using UnityEngine;

public class BoardGenerationSceneInitializer : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private UnityGrid grid;
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
    private SquareCellsGenerator3D _cellsGenerator;
    private CellsContentPresenter _cellContentPresenter;
    private CellsContentView _contentView;
    private ClientMovesView _clientMovesView;
    private ClientMovesPresenter _clientMovesPresenter;

    private Game _game;

    private void Start()
    {
        SetUpGame();
        SetUpInput();
        SetUpGrid();
        SetUpClickDetectors();
        SetUpContent();
        SetUpCells();
        SetUpMoves();
    }

    private void SetUpGame()
    {
        _game = GetGame();
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
        _gridGenerator.GenerateGrid(GetGameStartData().CellsPresence);
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
        _cellsGenerator = new SquareCellsGenerator3D(cellsFactory, grid);
        var cellViewMapGenerator = new SquareCellViewMapGenerator(assetsConfig);
        var cellViewMap = cellViewMapGenerator.GetCellViewMap(GetGameStartData());
        _cellsGenerator.GenerateCells(cellViewMap);
    }
    
    private void SetUpContent()
    {
        var playerPrefabProvider =
            new PlayerContentViewPrefabProvider(new PlayerColorProviderStub(), playerContentConfig);
        contentViewProvider.Init(playerPrefabProvider, commonContentConfig);
        _contentView = new CellsContentView(grid, contentViewProvider);
        _cellContentPresenter = new CellsContentPresenter(_contentView, _game.GetBoard());
    }

    private void SetUpMoves()
    {
        _clientMovesView = new ClientMovesView(_cellClickDetectors);
        var playerDataProvider = new PlayerDataProviderStub();
        var serverMovesApplier = new ServerMovesApplierStub();
        var localMovesApplier = new LocalMovesApplier(_game);
        var possibleMovesProvider = new PossibleMovesListProvider(_game);
        _clientMovesPresenter = new ClientMovesPresenter(playerDataProvider, serverMovesApplier, possibleMovesProvider, localMovesApplier, _clientMovesView);
    }

}
