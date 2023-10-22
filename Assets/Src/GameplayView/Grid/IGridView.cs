using castledice_game_logic;

namespace Src.GameplayView.Grid
{
    public interface IGridView
    {
        void GenerateGrid(CellType cellType, bool[,] cellsPresenceMatrix);
    }
}