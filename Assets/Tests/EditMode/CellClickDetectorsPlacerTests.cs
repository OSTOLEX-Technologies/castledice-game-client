using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.ClickDetection;
using Src.GameplayView.Grid;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode
{
    public class CellClickDetectorsPlacerTests
    {
        private class GridCellForTests : IGridCell
        {
            public Vector2Int Position { get; set; }
            public List<GameObject> Children { get; set; } = new();
            
            public GridCellForTests(Vector2Int position)
            {
                Position = position;
            }
            public IEnumerator<GameObject> GetEnumerator()
            {
                throw new System.NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void AddChild(GameObject child)
            {
                Children.Add(child);                
            }

            public bool RemoveChild(GameObject child)
            {
                return Children.Remove(child);
            }
        }
        
        private class GridForTests : IGrid
        {
            public List<GridCellForTests> Cells { get; set; } = new();
            
            public IEnumerator<IGridCell> GetEnumerator()
            {
                return Cells.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void AddCell(Vector2Int gamePosition, Vector3 scenePosition)
            {
                Cells.Add(new GridCellForTests(gamePosition));
            }

            public bool RemoveCell(Vector2Int gamePosition)
            {
                throw new System.NotImplementedException();
            }

            public IGridCell GetCell(Vector2Int gamePosition)
            {
                return Cells.Find(c => c.Position == gamePosition);
            }
        }

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