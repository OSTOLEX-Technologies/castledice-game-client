using System;

namespace Src.GameplayView.Grid.GridGeneration
{
    public interface IGridGenerator
    {
        void GenerateGrid(bool[,] cellsPresenceMatrix);

        public event Action<IGrid> GridGenerated;
    }
}