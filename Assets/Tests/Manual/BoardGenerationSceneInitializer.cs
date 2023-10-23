using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using Src.GameplayView.Cells;
using Src.GameplayView.Grid;
using Src.GameplayView.Grid.GridGeneration;
using static Tests.ObjectCreationUtility;
using UnityEngine;

public class BoardGenerationSceneInitializer : MonoBehaviour
{
    [SerializeField] private GameObjectsGrid grid;
    [SerializeField] private SquareGridGenerator gridGenerator;
    [SerializeField] private UnitySquareGridGenerationConfig gridGenerationConfig;
    [SerializeField] private UnitySquareCellAssetsConfig assetsConfig;
    [SerializeField] private SquareCellsGenerator3D generator;

    [ContextMenu("GenerateGrid")]

    public void GenerateGrid()
    {
        gridGenerator.GenerateGrid(GetGameStartData().CellsPresence);
    }
    
    [ContextMenu("GenerateCells")]
    public void GenerateCells()
    {
        
        var cellViewMapGenerator = new SquareCellViewMapGenerator(assetsConfig);
        var cellViewMap = cellViewMapGenerator.GetCellViewMap(GetGameStartData());
        generator.GenerateCells(cellViewMap);
    }
}
