using System;
using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using Src.GameplayPresenter.CellsContent;
using Src.GameplayView.Cells;
using Src.GameplayView.CellsContent;
using Src.GameplayView.CellsContent.ContentCreation;
using Src.GameplayView.Grid;
using Src.GameplayView.Grid.GridGeneration;
using Tests.Manual;
using static Tests.ObjectCreationUtility;
using UnityEngine;

public class BoardGenerationSceneInitializer : MonoBehaviour
{
    [SerializeField] private UnityGrid grid;
    [SerializeField] private UnitySquareCellsFactory cellsFactory;
    [SerializeField] private UnitySquareGridGenerationConfig gridGenerationConfig;
    [SerializeField] private UnitySquareCellAssetsConfig assetsConfig;
    [SerializeField] private UnityCommonContentViewPrefabConfig commonContentConfig;
    [SerializeField] private UnityPlayerContentViewPrefabsConfig playerContentConfig;
    [SerializeField] private UnityContentViewProvider contentViewProvider;
    private SquareGridGenerator _gridGenerator;
    private SquareCellsGenerator3D _generator;
    private CellsContentPresenter _presenter;
    private CellsContentView _view;

    private void Start()
    {
        var playerPrefabProvider =
            new PlayerContentViewPrefabProvider(new PlayerColorProviderStub(), playerContentConfig);
        contentViewProvider.Init(playerPrefabProvider, commonContentConfig);
        cellsFactory.Init(assetsConfig);
        _gridGenerator = new SquareGridGenerator(grid, gridGenerationConfig);
        // _generator = new SquareCellsGenerator3D(cellsFactory, grid);
        // _view = new CellsContentView(grid, contentViewProvider);
    }

    [ContextMenu("GenerateGrid")]

    public void GenerateGrid()
    {
        _gridGenerator.GenerateGrid(GetGameStartData().CellsPresence);
    }

    [ContextMenu("GenerateCells")]
    public void GenerateCells()
    {
        var cellViewMapGenerator = new SquareCellViewMapGenerator(assetsConfig);
        var cellViewMap = cellViewMapGenerator.GetCellViewMap(GetGameStartData());
        _generator.GenerateCells(cellViewMap);
    }

    [ContextMenu("GenerateContent")]
    public void GenerateContent()
    {
        _presenter = new CellsContentPresenter(_view, GetGame().GetBoard());
    }

}
