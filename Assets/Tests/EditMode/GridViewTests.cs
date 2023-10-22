using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.Grid;

namespace Tests.EditMode
{
    public class GridViewTests
    {
        public static CellType[] CellTypes = { CellType.Square, CellType.Triangle };

        [Test]
        public void GenerateGrid_ShouldPassGivenCellType_ToGivenGridGeneratorsFactory([ValueSource(nameof(CellTypes))]CellType cellType)
        {
            var factoryMock = new Mock<IGridGeneratorsFactory>();
            factoryMock.Setup(f => f.GetGridGenerator(It.IsAny<CellType>())).Returns(new Mock<IGridGenerator>().Object);
            var view = new GridView(factoryMock.Object);
            
            view.GenerateGrid(cellType, new bool[0, 0]);
            
            factoryMock.Verify(f => f.GetGridGenerator(cellType), Times.Once);
        }

        [Test]
        public void GenerateGrid_ShouldPassGivenCellsPresenceMatrix_ToGridGeneratorObtainedFromFactory()
        {
            var factoryMock = new Mock<IGridGeneratorsFactory>();
            var generatorMock = new Mock<IGridGenerator>();
            factoryMock.Setup(f => f.GetGridGenerator(It.IsAny<CellType>())).Returns(generatorMock.Object);
            var view = new GridView(factoryMock.Object);
            var cellsPresenceMatrix = new bool[0, 0];
            
            view.GenerateGrid(CellType.Square, cellsPresenceMatrix);
            
            generatorMock.Verify(g => g.GenerateGrid(cellsPresenceMatrix), Times.Once);
        }
    }
}