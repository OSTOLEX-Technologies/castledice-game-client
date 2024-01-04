using castledice_game_logic.BoardGeneration.CellsGeneration;

namespace Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators
{
    public interface ICellsGeneratorCreator
    {
        ICellsGenerator GetCellsGenerator(bool[,] cellsPresence);
    }
}