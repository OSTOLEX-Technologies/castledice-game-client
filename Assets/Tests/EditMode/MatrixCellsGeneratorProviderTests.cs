using castledice_game_logic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;

namespace Tests.EditMode
{
    public class MatrixCellsGeneratorProviderTests
    {
        public static bool[][,] Matrices =
        {
            new[,]
            {
                { true, true },
                { true, true }
            },
            new[,]
            {
                { true, false },
                { false, true }
            },
            new[,]
            {
                { false, false },
                { false, false }
            },
            new[,]
            {
                { true, true, true },
                { true, true, true },
                { true, true, true }
            },
        };
        
        [Test]
        public void GetCellsGenerator_ShouldReturnMatrixCellsGenerator()
        {
            var provider = new MatrixCellsGeneratorProvider();
            
            var generator = provider.GetCellsGenerator(new bool[0, 0]);
            
            Assert.IsInstanceOf<MatrixCellsGenerator>(generator);
        }

        [Test]
        public void ReturnedGenerator_ShouldGenerateCells_AccordingToGivenMatrix([ValueSource(nameof(Matrices))]bool[,] matrix)
        {
            var board = new Board(CellType.Square);
            var provider = new MatrixCellsGeneratorProvider();
            
            var generator = provider.GetCellsGenerator(matrix);
            generator.GenerateCells(board);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Assert.AreEqual(matrix[i, j], board.HasCell(i, j));
                }
            }
        }
    }
}