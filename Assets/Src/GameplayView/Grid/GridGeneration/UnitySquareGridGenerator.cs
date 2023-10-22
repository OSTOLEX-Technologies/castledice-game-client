using System;
using UnityEngine;

namespace Src.GameplayView.Grid.GridGeneration
{
    public class UnitySquareGridGenerator : MonoBehaviour, IGridGenerator
    {
        private IGameObjectsGrid _grid;
        private ISquareGridGenerationConfig _config;

        public void Init(IGameObjectsGrid grid, ISquareGridGenerationConfig config)
        {
            _grid = grid;
            _config = config;
        }
        
        public void GenerateGrid(bool[,] cellsPresenceMatrix)
        {
            var startPos = _config.StartPosition;
            var length = _config.CellLength;
            var width = _config.CellWidth;
            for (int i = 0; i < cellsPresenceMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < cellsPresenceMatrix.GetLength(1); j++)
                {
                    var go = new GameObject();
                    var position = startPos + new Vector3(j * length, 0, i * width);
                    go.transform.position = position;
                    _grid.AddParent((i, j), go);
                }
            }
        }
    }
}