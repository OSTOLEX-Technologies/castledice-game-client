using Src.GameplayPresenter.Cells.SquareCellsGeneration;
using Src.GameplayView.Cells;
using static Tests.ObjectCreationUtility;
using UnityEngine;

public class BoardGenerationSceneInitializer : MonoBehaviour
{
    [SerializeField] private SquareCellAssetsConfig config;
    [SerializeField] private SquareCellsGenerator3D generator;

    [ContextMenu("GenerateCells")]
    public void GenerateCells()
    {
        var cellViewMapGenerator = new SquareCellViewMapGenerator(config);
        var cellViewMap = cellViewMapGenerator.GetCellViewMap(GetGameStartData());
        generator.GenerateCells(cellViewMap);
    }
}
