using castledice_game_logic;

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
            throw new System.NotImplementedException();
        }
    }
}