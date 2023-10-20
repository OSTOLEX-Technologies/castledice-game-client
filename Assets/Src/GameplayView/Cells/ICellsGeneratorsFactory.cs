using castledice_game_logic;

namespace Src.GameplayView.Cells
{
    public interface ICellsGeneratorsFactory
    {
        ICellsGenerator GetGenerator(CellType cellType);
    }
}