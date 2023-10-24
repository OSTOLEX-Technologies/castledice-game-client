using Moq;
using NUnit.Framework;
using Src.GameplayView.Grid;
using Src.GameplayView.Grid.GridGeneration;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.PlayMode
{
    public class SquareGridGeneratorTests
    {
        private const float TOLERANCE = 0.01f;

        [TestCaseSource(nameof(AppropriateGridPositionsCases))]
        public void GenerateGrid_ShouldAddCells_ToAppropriateGamePositionsOnGrid(bool[,] cellsPresenceMatrix,
            Vector2Int[] expectedPositions)
        {
            var gridMock = new Mock<IGrid>();
            var configMock = new Mock<ISquareGridGenerationConfig>();
            var generator = new SquareGridGenerator(gridMock.Object, configMock.Object);

            generator.GenerateGrid(cellsPresenceMatrix);

            foreach (var expectedPosition in expectedPositions)
            {
                gridMock.Verify(grid => grid.AddCell(expectedPosition, It.IsAny<Vector3>()));
            }
        }

        public static object[] AppropriateGridPositionsCases =
        {
            new object[]
            {
                new[,]
                {
                    { true, true },
                    { true, true }
                },
                new Vector2Int[]
                {
                    (0, 0), (1, 0),
                    (0, 1), (1, 1)
                }
            },
            new object[]
            {
                new[,]
                {
                    { true, false },
                    { true, true }
                },
                new Vector2Int[]
                {
                    (0, 0),
                    (0, 1), (1, 1)
                }
            },
            new object[]
            {
                new[,]
                {
                    { true, true },
                    { false, true }
                },
                new Vector2Int[]
                {
                    (0, 0), (1, 0),
                    (1, 1)
                }
            },
            new object[]
            {
                new[,]
                {
                    { true, true, true },
                    { true, true, true },
                    { true, true, true }
                },
                new Vector2Int[]
                {
                    (0, 0), (1, 0), (2, 0),
                    (0, 1), (1, 1), (2, 1),
                    (0, 2), (1, 2), (2, 2)
                }
            },
        };

        [TestCaseSource(nameof(AppropriateScenePositionsCases))]
        public void GenerateGrid_ShouldAddCells_OnAppropriateScenePositionsOnGrid(bool[,] cellsPresenceMatrix,
            ISquareGridGenerationConfig config, Vector3[] expectedPositions)
        {
            var gridMock = new Mock<IGrid>();
            var generator = new SquareGridGenerator(gridMock.Object, config);

            generator.GenerateGrid(cellsPresenceMatrix);

            foreach (var position in expectedPositions)
            {
                gridMock.Verify(g => g.AddCell(It.IsAny<Vector2Int>(), position), Times.Once);
            }
        }

        public static object[] AppropriateScenePositionsCases =
        {
            new object[]
            {
                new[,]
                {
                    { true, true },
                    { true, true }
                },
                new TestSquareGenerationConfig
                {
                    CellLength = 1f,
                    CellWidth = 1f,
                    StartPosition = new Vector3(0, 0, 0)
                },
                new Vector3[]
                {
                    new(0, 0, 0), new(1, 0, 0),
                    new(0, 0, 1), new(1, 0, 1)
                }
            },
            new object[]
            {
                new[,]
                {
                    { true, true },
                    { true, true }
                },
                new TestSquareGenerationConfig
                {
                    CellLength = 2f,
                    CellWidth = 2f,
                    StartPosition = new Vector3(1, 0, 1)
                },
                new Vector3[]
                {
                     new(1, 0, 1), new(3, 0, 1),
                     new(1, 0, 3), new(3, 0, 3) 
                }
            }
        };

        public class TestSquareGenerationConfig : ISquareGridGenerationConfig
        {
            public float CellLength { get; set; }
            public float CellWidth { get; set; }
            public Vector3 StartPosition { get; set; }
        }
    }
}