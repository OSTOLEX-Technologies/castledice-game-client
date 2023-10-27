using castledice_game_logic;

namespace Src.GameplayView.Cells
{
    public interface ICellsViewGeneratorsFactory
    {
        ICellsGenerator GetGenerator(CellType cellType);
    }
}