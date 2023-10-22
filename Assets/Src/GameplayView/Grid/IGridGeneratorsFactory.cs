using castledice_game_logic;

namespace Src.GameplayView.Grid
{
    public interface IGridGeneratorsFactory
    {
        IGridGenerator GetGridGenerator(CellType cellType);
    }
}