using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.Cells;
using Src.GameplayView.Cells;
using UnityEngine;

namespace Tests.PlayMode
{
    public class CellsViewTests
    {
        public static CellType[] CellTypes = { CellType.Square, CellType.Triangle };
        
        [Test]
        public void GenerateCells_ShouldPassCellType_ToGivenCellsGeneratorsFactory([ValueSource(nameof(CellTypes))]CellType cellType)
        {
            var cellsGeneratorsFactoryMock = new Mock<ICellsViewGeneratorsFactory>();
            var cellsGeneratorMock = new Mock<ICellsViewGenerator>();
            cellsGeneratorsFactoryMock.Setup(factory => factory.GetGenerator(It.IsAny<CellType>())).Returns(cellsGeneratorMock.Object);
            var cellsView = new CellsView(cellsGeneratorsFactoryMock.Object);
            var cellViewMap = new CellViewData[0, 0];
            
            cellsView.GenerateCells(cellType, cellViewMap);
            
            cellsGeneratorsFactoryMock.Verify(factory => factory.GetGenerator(cellType), Times.Once);
        }
        
        [Test]
        public void GenerateCells_ShouldPassCellViewMap_ToGivenCellsGenerator()
        {
            var cellsGeneratorsFactoryMock = new Mock<ICellsViewGeneratorsFactory>();
            var cellsGeneratorMock = new Mock<ICellsViewGenerator>();
            cellsGeneratorsFactoryMock.Setup(factory => factory.GetGenerator(It.IsAny<CellType>())).Returns(cellsGeneratorMock.Object);
            var cellsView = new CellsView(cellsGeneratorsFactoryMock.Object);
            var cellViewMap = new CellViewData[0, 0];
            
            cellsView.GenerateCells(CellType.Square, cellViewMap);
            
            cellsGeneratorMock.Verify(generator => generator.GenerateCellsView(cellViewMap), Times.Once);
        }
    }
}