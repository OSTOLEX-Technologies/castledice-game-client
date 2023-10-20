using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.Cells;
using Src.GameplayView.Cells;
using UnityEngine;

namespace Tests.PlayMode
{
    public class UnityCellsViewTests
    {
        public static CellType[] CellTypes = { CellType.Square, CellType.Triangle };
        
        [Test]
        public void GenerateCells_ShouldPassCellType_ToGivenCellsGeneratorsFactory([ValueSource(nameof(CellTypes))]CellType cellType)
        {
            var cellsGeneratorsFactoryMock = new Mock<ICellsGeneratorsFactory>();
            var cellsGeneratorMock = new Mock<ICellsGenerator>();
            cellsGeneratorsFactoryMock.Setup(factory => factory.GetGenerator(It.IsAny<CellType>())).Returns(cellsGeneratorMock.Object);
            var gameObject = new GameObject();
            var unityCellsView = gameObject.AddComponent<UnityCellsView>();
            unityCellsView.Init(cellsGeneratorsFactoryMock.Object);
            var cellViewMap = new CellViewData[0, 0];
            
            unityCellsView.GenerateCells(cellType, cellViewMap);
            
            cellsGeneratorsFactoryMock.Verify(factory => factory.GetGenerator(cellType), Times.Once);
        }
        
        [Test]
        public void GenerateCells_ShouldPassCellViewMap_ToGivenCellsGenerator()
        {
            var cellsGeneratorsFactoryMock = new Mock<ICellsGeneratorsFactory>();
            var cellsGeneratorMock = new Mock<ICellsGenerator>();
            cellsGeneratorsFactoryMock.Setup(factory => factory.GetGenerator(It.IsAny<CellType>())).Returns(cellsGeneratorMock.Object);
            var gameObject = new GameObject();
            var unityCellsView = gameObject.AddComponent<UnityCellsView>();
            unityCellsView.Init(cellsGeneratorsFactoryMock.Object);
            var cellViewMap = new CellViewData[0, 0];
            
            unityCellsView.GenerateCells(CellType.Square, cellViewMap);
            
            cellsGeneratorMock.Verify(generator => generator.GenerateCells(cellViewMap), Times.Once);
        }
    }
}