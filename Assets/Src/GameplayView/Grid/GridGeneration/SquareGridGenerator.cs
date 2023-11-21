using UnityEngine;

namespace Src.GameplayView.Grid.GridGeneration
{
    public class SquareGridGenerator : IGridGenerator
    {
        private readonly IGrid _grid;
        private readonly ISquareGridGenerationConfig _config;

        public SquareGridGenerator(IGrid grid, ISquareGridGenerationConfig config)
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
                    var position = startPos + new Vector3(j * length, 0, i * width);
                    _grid.AddCell((i, j), position);
                }
            }
        }
    }
}