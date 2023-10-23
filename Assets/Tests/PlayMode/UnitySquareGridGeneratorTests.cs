using Moq;
using NUnit.Framework;
using Src.GameplayView.Grid;
using Src.GameplayView.Grid.GridGeneration;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.PlayMode
{
    public class UnitySquareGridGeneratorTests
    {
        private const float TOLERANCE = 0.01f;

        [TestCaseSource(nameof(AppropriateGridPositionsCases))]
        public void GenerateGrid_ShouldAddParentObjects_ToAppropriatePositionsOnGrid(bool[,] cellsPresenceMatrix,
            Vector2Int[] expectedPositions)
        {
            var gridMock = new Mock<IGameObjectsGrid>();
            var configMock = new Mock<ISquareGridGenerationConfig>();
            var gameObject = new GameObject();
            var generator = gameObject.AddComponent<UnitySquareGridGenerator>();
            generator.Init(gridMock.Object, configMock.Object);

            generator.GenerateGrid(cellsPresenceMatrix);

            foreach (var expectedPosition in expectedPositions)
            {
                gridMock.Verify(grid => grid.AddParent(expectedPosition, It.IsAny<GameObject>()));
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
        public void GenerateGrid_ShouldCreateGameObjects_OnAppropriatePositionsOnScene(bool[,] cellsPresenceMatrix,
            ISquareGridGenerationConfig config, Vector3[,] expectedPositions)
        {
            var grid = new TestGrid(cellsPresenceMatrix.GetLength(0), cellsPresenceMatrix.GetLength(1));
            var gameObject = new GameObject();
            var generator = gameObject.AddComponent<UnitySquareGridGenerator>();
            generator.Init(grid, config);

            generator.GenerateGrid(cellsPresenceMatrix);

            for (int i = 0; i < cellsPresenceMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < cellsPresenceMatrix.GetLength(1); j++)
                {
                    if (!cellsPresenceMatrix[i, j])
                    {
                        continue;
                    }

                    var actualPosition = grid.GameObjects[i, j].transform.position;

                    Assert.That(actualPosition.x, Is.EqualTo(expectedPositions[i, j].x).Within(TOLERANCE));
                    Assert.That(actualPosition.y, Is.EqualTo(expectedPositions[i, j].y).Within(TOLERANCE));
                    Assert.That(actualPosition.z, Is.EqualTo(expectedPositions[i, j].z).Within(TOLERANCE));
                }
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
                new Vector3[,]
                {
                    {new (0, 0, 0), new (1, 0, 0)},
                    {new (0, 0, 1), new (1, 0, 1)}
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
                new Vector3[,]
                {
                    {new (1, 0, 1), new (3, 0, 1)},
                    {new (1, 0, 3), new (3, 0, 3)}
                }
            }
        };

    private class TestGrid : IGameObjectsGrid
        {
            public GameObject[,] GameObjects;
            
            public TestGrid(int length, int width)
            {
                GameObjects = new GameObject[length, width];
            }
            
            public void AddParent(Vector2Int position, GameObject parent)
            {
                GameObjects[position.X, position.Y] = parent;
            }

            public GameObject GetParent(Vector2Int position)
            {
                throw new System.NotImplementedException();
            }

            public bool RemoveParent(Vector2Int position)
            {
                throw new System.NotImplementedException();
            }

            public void AddChild(Vector2Int position, GameObject child)
            {
                throw new System.NotImplementedException();
            }

            public bool RemoveChild(Vector2Int position, GameObject child)
            {
                throw new System.NotImplementedException();
            }
        }
    }

    public class TestSquareGenerationConfig : ISquareGridGenerationConfig
    {
        public float CellLength { get; set; }
        public float CellWidth { get; set; }
        public Vector3 StartPosition { get; set; }
    }
}