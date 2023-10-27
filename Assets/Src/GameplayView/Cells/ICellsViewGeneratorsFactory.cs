using castledice_game_logic;

namespace Src.GameplayView.Cells
{
    public interface ICellsViewGeneratorsFactory
    {
        ICellsViewGenerator GetGenerator(CellType cellType);
    }
}