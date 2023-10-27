using castledice_game_logic.BoardGeneration.CellsGeneration;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public interface ICellsGeneratorProvider
    {
        ICellsGenerator GetCellsGenerator(bool[,] cellsPresence);
    }
}