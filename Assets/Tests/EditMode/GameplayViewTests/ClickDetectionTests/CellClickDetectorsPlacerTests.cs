using Moq;
using NUnit.Framework;
using Src.GameplayView.ClickDetection;
using Tests.Utils.Mocks;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode
{
    public class CellClickDetectorsPlacerTests
    {
        public static Vector2Int[][] PositionsArrays =
        {
            new Vector2Int[] {(1, 2), (3, 4), (5, 6)},
            new Vector2Int[] {(3, 4), (5, 6)}
        };

        public static int[] CellsCounts = { 1, 2, 3, 4, 5 };

        [Test]
        public void PlaceDetectors_ShouldCallGetDetectorOnGivenFactory_ForEveryPositionOnGrid([ValueSource(nameof(PositionsArrays))]Vector2Int[] positions)
        {
            var factoryMock = new Mock<IUnityCellClickDetectorsFactory>();
            var gameObject = new GameObject();
            var detector = gameObject.AddComponent<UnityCellClickDetector>();
            factoryMock.Setup(f => f.GetDetector(It.IsAny<Vector2Int>())).Returns(detector);
            var grid = new GridForTests();
            foreach (var position in positions)
            {
                grid.AddCell(position, new Vector3());
            }
            var placer = new CellClickDetectorsPlacer(grid, factoryMock.Object);
            
            placer.PlaceDetectors(); 
            
            foreach (var position in positions)
            {
                factoryMock.Verify(f => f.GetDetector(position), Times.Once);
            }
        }

        [Test]
        public void PlaceDetectors_ShouldPlaceDetectorsFromFactory_OnEveryCellOfGrid([ValueSource(nameof(CellsCounts))]int cellsCount)
        {
            var factoryMock = new Mock<IUnityCellClickDetectorsFactory>();
            var gameObject = new GameObject();
            var detector = gameObject.AddComponent<UnityCellClickDetector>();
            factoryMock.Setup(f => f.GetDetector(It.IsAny<Vector2Int>())).Returns(detector);
            var grid = new GridForTests();
            for (int i = 0; i < cellsCount; i++)
            {
                grid.AddCell((0, 0), Vector3.zero);
            }
            var placer = new CellClickDetectorsPlacer(grid, factoryMock.Object);
            
            placer.PlaceDetectors();
            
            foreach (var cell in grid)
            {
                var testCell = cell as GridCellForTests;
                Assert.Contains(detector.gameObject, testCell.Children);
            }
        }

        [Test]
        public void PlaceDetectors_ShouldReturnListOfPlacedDetectors([ValueSource(nameof(CellsCounts))] int cellsCount)
        {
            var factoryMock = new Mock<IUnityCellClickDetectorsFactory>();
            var gameObject = new GameObject();
            var detector = gameObject.AddComponent<UnityCellClickDetector>();
            factoryMock.Setup(f => f.GetDetector(It.IsAny<Vector2Int>())).Returns(detector);
            var grid = new GridForTests();
            for (int i = 0; i < cellsCount; i++)
            {
                grid.AddCell((0, 0), Vector3.zero);
            }

            var placer = new CellClickDetectorsPlacer(grid, factoryMock.Object);

            var placedDetectors = placer.PlaceDetectors();

            foreach (var placedDetector in placedDetectors)
            {
                Assert.AreSame(detector, placedDetector);
            }
        }
    }
    
}