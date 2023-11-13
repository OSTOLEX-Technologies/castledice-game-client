using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.Cells;
using Src.GameplayView.Cells;
using Src.GameplayView.Grid;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.GameplayPresenterTests.CellsGenerationTests.SquareCellsGenerationTests
{
    public class SquareCellsViewGenerator3DTests
    {
        public static CellViewData[][,] CellViewMaps =
        {
            new CellViewData[,]
            {
                {new (1, false), new (2, false)},
                {new (3, false), new (4, false)}
            },
            new CellViewData[,]
            {
                {new (1, false), new (2, false), new (3, false)},
                {new (4, false), new (5, false), new (6, false)},
                {new (7, false), new (8, false), new (9, false)}
            },
            new CellViewData[,]
            {
                {new (1, true), new (2, false), new (3, false), new (4, true)},
                {new (5, false), new (6, false), new (7, true), new (8, false)},
                {new (9, false), new (10, false), new (11, false), new (12, true)},
                {new (13, true), new (14, false), new (15, false), new (16, true)}
            }
        };
        
        [Test]
        public void GenerateCellsView_ShouldPassCellViewDataAssetId_ToGivenCellsFactoryIfDataIsNotNull([ValueSource(nameof(CellViewMaps))]CellViewData[,] cellViewMap)
        {
            var cellsFactory = new Mock<ISquareCellsFactory>();
            cellsFactory.Setup(c => c.GetSquareCell(It.IsAny<int>())).Returns(new GameObject());
            var gridMock = new Mock<IGrid>();
            gridMock.Setup(g => g.GetCell(It.IsAny<Vector2Int>())).Returns(new Mock<IGridCell>().Object);
            var generator = new SquareCellsViewGenerator3D(cellsFactory.Object, gridMock.Object);
            
            generator.GenerateCellsView(cellViewMap);

            foreach (var data in cellViewMap)
            {
                if (data.IsNull)
                {
                    continue;
                }
                cellsFactory.Verify(c => c.GetSquareCell(data.AssetId), Times.Once);
            }
        }
        
        [Test]
        public void GenerateCellsView_ShouldAddGameObjects_ToGivenGridWithAppropriateGamePosition([ValueSource(nameof(CellViewMaps))]CellViewData[,] cellViewMap)
        {
            //Setting up cells factory
            var cellsFactory = new Mock<ISquareCellsFactory>();
            var gameObjects = new GameObject[cellViewMap.GetLength(0), cellViewMap.GetLength(1)];
            for (int i = 0; i < cellViewMap.GetLength(0); i++)
            {
                for (int j = 0; j < cellViewMap.GetLength(1); j++)
                {
                    var data = cellViewMap[i, j];
                    if (data.IsNull)
                    {
                        continue;
                    }
                    gameObjects[i, j] = new GameObject();
                    cellsFactory.Setup(c => c.GetSquareCell(data.AssetId)).Returns(gameObjects[i, j]);
                }
            }
            //Setting up grid and everything else
            var gridMock = new Mock<IGrid>();
            var gridCellMock = new Mock<IGridCell>();
            gridMock.Setup(g => g.GetCell(It.IsAny<Vector2Int>())).Returns(gridCellMock.Object);
            var generator = new SquareCellsViewGenerator3D(cellsFactory.Object, gridMock.Object);
            
            generator.GenerateCellsView(cellViewMap);

            for (int i = 0; i < cellViewMap.GetLength(0); i++)
            {
                for (int j = 0; j < cellViewMap.GetLength(1); j++)
                {
                    var data = cellViewMap[i, j];
                    if (data.IsNull)
                    {
                        continue;
                    }
                    var expectedPosition = new Vector2Int(i, j);
                    var expectedGameObject = gameObjects[i, j];
                    gridMock.Verify(g => g.GetCell(expectedPosition), Times.Once);
                    gridCellMock.Verify(c => c.AddChild(expectedGameObject), Times.Once);
                }
            }
        }
    }
}