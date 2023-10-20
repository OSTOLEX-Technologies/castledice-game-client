using Src.GameplayPresenter.Cells;
using Src.GameplayView.Cells;
using UnityEngine;

public class SquareCellsGenerator3D : MonoBehaviour, ICellsGenerator
{
    [SerializeField] private SquareCellAssetsConfig config;
    [SerializeField] private float xDelta;
    [SerializeField] private float yDelta;
    [SerializeField] private Vector3 startPosition;


    public void GenerateCells(CellViewData[,] cellsViewMap)
    {
        for (int i = 0; i < cellsViewMap.GetLength(0); i++)
        {
            for (int j = 0; j < cellsViewMap.GetLength(1); j++)
            {
                var cellData = cellsViewMap[i, j];
                if (cellData.IsNull) continue;
                var cellPrefab = config.GetAssetPrefab(cellData.AssetId);
                var spawnX = startPosition.x + j * xDelta;
                var spawnY = startPosition.y + i * yDelta;
                var spawnPosition = new Vector3(spawnX, startPosition.y, spawnY);
                Instantiate(cellPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
