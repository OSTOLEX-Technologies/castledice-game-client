using Src.GameplayPresenter.Cells;
using Src.GameplayView.Cells;
using Src.GameplayView.Grid;
using UnityEngine;

public class UnitySquareCellsGenerator3D : MonoBehaviour, ICellsGenerator
{
    private SquareCellAssetsConfig _config;
    private IGameObjectsGrid _grid;

    public void Init(SquareCellAssetsConfig config, IGameObjectsGrid grid)
    {
        _config = config;
        _grid = grid;
    }
    
    public void GenerateCells(CellViewData[,] cellsViewMap)
    {
        for (int i = 0; i < cellsViewMap.GetLength(0); i++)
        {
            for (int j = 0; j < cellsViewMap.GetLength(1); j++)
            {
                var cellData = cellsViewMap[i, j];
                if (cellData.IsNull) continue;
                var cellPrefab = _config.GetAssetPrefab(cellData.AssetId);
                var cell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity);
                _grid.AddChild((i, j), cell);
            }
        }
    }
}
