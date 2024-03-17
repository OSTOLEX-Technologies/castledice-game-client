using System.Collections.Generic;
using castledice_game_logic.Math;
using NUnit.Framework;
using Src.PVE.BoardStateCalculation;

namespace Tests.EditMode.PVETests.BoardStateCalculationTests
{
    public class BoardStateTests
    {
        [Test]
        [TestCaseSource(nameof(CellStateMatrices))]
        public void Indexer_ShouldReturnValues_AccordingToGivenCellStatesMatrix(CellState[,] matrix)
        {
            var boardState = new BoardState(matrix);
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Assert.AreEqual(matrix[i, j], boardState[i, j]);
                }
            }
        }
        
        public static IEnumerable<CellState[,]> CellStateMatrices()
        {
            yield return new [,]
            {
                { CellState.Base, CellState.Free },
                { CellState.Unit, CellState.Free }
            };
            yield return new [,]
            {
                { CellState.Unit, CellState.Base },
                { CellState.Free, CellState.Unit }
            };
            yield return new [,]
            {
                { CellState.Unit, CellState.Base, CellState.Free },
                { CellState.Free, CellState.Unit, CellState.Base },
                { CellState.Base, CellState.Free, CellState.Unit }
            };
            yield return new [,]
            {
                { CellState.Unit, CellState.Base, CellState.Free, CellState.Unit },
                { CellState.Free, CellState.Unit, CellState.Base, CellState.Free },
                { CellState.Base, CellState.Free, CellState.Unit, CellState.Base },
                { CellState.Unit, CellState.Base, CellState.Free, CellState.Unit }
            };
        }
        
        [Test]
        public void Indexer_ShouldThrowIndexOutOfRangeException_WhenGivenInvalidIndex()
        {
            var boardState = new BoardState(new [,] {{CellState.Base}});
            
            Assert.Throws<System.IndexOutOfRangeException>(() =>
            {
                var cellState = boardState[1, 0];
            });
        }
        
        [Test]
        public void Indexer_ShouldSetValues_ToGivenCellStatesMatrix()
        {
            var boardState = new BoardState(new [,] {{CellState.Base}});
            
            boardState[0, 0] = CellState.Unit;
            
            Assert.AreEqual(CellState.Unit, boardState[0, 0]);
        }
        
        [Test]
        [TestCaseSource(nameof(CountCellsWithStateTestCases))]
        public void CountCellsWithState_ShouldReturnNumberOfCells_WithGivenState((CellState[,] matrix, CellState state, int expectedCount) testCase)
        {
            var boardState = new BoardState(testCase.matrix);
            
            var count = boardState.CountCellsWithState(testCase.state);
            
            Assert.AreEqual(testCase.expectedCount, count);
        }

        public static IEnumerable<(CellState[,] matrix, CellState state, int expectedCount)> CountCellsWithStateTestCases()
        {
            yield return (new [,]
            {
                {CellState.Base, CellState.Free}
            }, CellState.Base, 1);
            yield return (new [,]
            {
                {CellState.Unit, CellState.Base}, 
                {CellState.Free, CellState.Unit}
            }, CellState.Unit, 2);
            yield return (new [,]
            {
                {CellState.Unit, CellState.Base, CellState.Free}, 
                {CellState.Free, CellState.Unit, CellState.Base}, 
                {CellState.Base, CellState.Free, CellState.Unit}
            }, CellState.Free, 3);
            yield return (new [,]
            {
                {CellState.Unit, CellState.Base, CellState.Free, CellState.Unit}, 
                {CellState.Free, CellState.Unit, CellState.Base, CellState.Free}, 
                {CellState.Base, CellState.Free, CellState.Unit, CellState.Base}, 
                {CellState.Unit, CellState.Base, CellState.Free, CellState.Unit}
            }, CellState.Base, 5);
        }
        
        [Test]
        [TestCaseSource(nameof(GetPositionsWithStateTestCases))]
        public void GetPositionsWithState_ShouldReturnPositions_WithGivenState((CellState[,] matrix, CellState state, List<Vector2Int> expectedPositions) testCase)
        {
            var boardState = new BoardState(testCase.matrix);
            
            var positions = boardState.GetPositionsWithState(testCase.state);
            
            CollectionAssert.AreEqual(testCase.expectedPositions, positions);
        }
        
        public static IEnumerable<(CellState[,] matrix, CellState state, List<Vector2Int> expectedPositions)> GetPositionsWithStateTestCases()
        {
            yield return (new [,]
            {
                {CellState.Base, CellState.Free}
            }, CellState.Base, new List<Vector2Int> {new Vector2Int(0, 0)});
            yield return (new [,]
            {
                {CellState.Unit, CellState.Base}, 
                {CellState.Free, CellState.Unit}
            }, CellState.Unit, new List<Vector2Int> {new Vector2Int(0, 0), new Vector2Int(1, 1)});
            yield return (new [,]
            {
                {CellState.Unit, CellState.Base, CellState.Free}, 
                {CellState.Free, CellState.Unit, CellState.Base}, 
                {CellState.Base, CellState.Free, CellState.Unit}
            }, CellState.Free, new List<Vector2Int> {new Vector2Int(0, 2), new Vector2Int(1, 0), new Vector2Int(2, 1)});
            yield return (new [,]
            {
                {CellState.Unit, CellState.Base, CellState.Free, CellState.Unit}, 
                {CellState.Free, CellState.Unit, CellState.Base, CellState.Free}, 
                {CellState.Base, CellState.Free, CellState.Unit, CellState.Base}, 
                {CellState.Unit, CellState.Base, CellState.Free, CellState.Unit}
            }, CellState.Base, new List<Vector2Int> {new Vector2Int(0, 1), new Vector2Int(1, 2), new Vector2Int(2, 0), new Vector2Int(2, 3), new Vector2Int(3, 1)});
        }
    }
}