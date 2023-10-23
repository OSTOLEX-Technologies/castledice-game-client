using castledice_game_logic;

namespace Src.GameplayView.Grid.GridGeneration
{
    public interface IGridGeneratorsFactory
    {
        IGridGenerator GetGridGenerator(CellType cellType);
    }
}