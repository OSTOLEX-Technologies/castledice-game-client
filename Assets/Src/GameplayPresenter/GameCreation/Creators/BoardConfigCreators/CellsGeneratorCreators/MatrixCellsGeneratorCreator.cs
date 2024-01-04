using castledice_game_logic.BoardGeneration.CellsGeneration;

namespace Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators
{
    public class MatrixCellsGeneratorCreator : ICellsGeneratorCreator
    {
        public ICellsGenerator GetCellsGenerator(bool[,] cellsPresence)
        {
            return new MatrixCellsGenerator(cellsPresence);
        }
    }
}