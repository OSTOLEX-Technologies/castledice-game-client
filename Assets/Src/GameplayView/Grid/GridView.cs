using castledice_game_logic;
using Src.GameplayView.Grid.GridGeneration;

namespace Src.GameplayView.Grid
{
    public class GridView :  IGridView
    {
        private IGridGeneratorsFactory _gridGeneratorsFactory;

        public GridView(IGridGeneratorsFactory gridGeneratorsFactory)
        {
            _gridGeneratorsFactory = gridGeneratorsFactory;
        }

        public void GenerateGrid(CellType cellType, bool[,] cellsPresenceMatrix)
        {
            var generator = _gridGeneratorsFactory.GetGridGenerator(cellType);
            generator.GenerateGrid(cellsPresenceMatrix);
        }
    }
}