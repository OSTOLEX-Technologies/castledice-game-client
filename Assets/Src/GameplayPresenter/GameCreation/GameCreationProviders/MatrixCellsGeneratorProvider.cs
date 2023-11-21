using castledice_game_logic.BoardGeneration.CellsGeneration;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public class MatrixCellsGeneratorProvider : ICellsGeneratorProvider
    {
        public ICellsGenerator GetCellsGenerator(bool[,] cellsPresence)
        {
            return new MatrixCellsGenerator(cellsPresence);
        }
    }
}