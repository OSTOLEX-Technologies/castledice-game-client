using System;
using UnityEngine;

namespace Src.GameplayView.Grid.GridGeneration
{
    public class MonoBehaviourSquareGridGenerator : MonoBehaviour, IGridGenerator
    {
        [SerializeField] private GameObjectsGrid grid;
        [SerializeField] private SquareGridGenerationConfig config;

        public bool IsGenerated { get; private set; }

        public void GenerateGrid(bool[,] cellsPresenceMatrix)
        {
            var startPos = config.StartPosition;
            var length = config.CellLength;
            var width = config.CellWidth;
            for (int i = 0; i < cellsPresenceMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < cellsPresenceMatrix.GetLength(1); j++)
                {
                    var position = startPos + new Vector3(j * length, 0, i * width);
                    grid.AddCell((i, j), position);
                }
            }

            IsGenerated = true;
            GridGenerated?.Invoke(grid);
        }

        public bool TryGetGeneratedGrid(out GameObjectsGrid generatedGrid)
        {
            if (!IsGenerated)
            {
                generatedGrid = null;
                return false;
            }

            generatedGrid = grid;
            return true;
        }

        public event Action<IGrid> GridGenerated;
    }
}