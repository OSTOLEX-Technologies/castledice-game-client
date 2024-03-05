using System;
using castledice_game_logic.Math;
using NUnit.Framework;
using Src.PVE;

namespace Tests.EditMode.PVETests
{
    public class DijkstraPathMinCostSearcherTests
    {
        [Test]
        public void GetMinCost_ShouldThrowIndexOutOfRangeException_IfFromPositionIsOutsideMatrix()
        {
            var matrix = new int[1, 1]{{0}};
            var from = new Vector2Int(1, 1);
            var to = new Vector2Int(0, 0);
            var searcher = new DijkstraPathMinCostSearcher();
            
            Assert.Throws<IndexOutOfRangeException>(() => searcher.GetMinCost(matrix, from, to));
        }
        
        [Test]
        public void GetMinCost_ShouldThrowIndexOutOfRangeException_IfToPositionIsOutsideMatrix()
        {
            var matrix = new int[1, 1]{{0}};
            var from = new Vector2Int(0, 0);
            var to = new Vector2Int(1, 1);
            var searcher = new DijkstraPathMinCostSearcher();
            
            Assert.Throws<IndexOutOfRangeException>(() => searcher.GetMinCost(matrix, from, to));
        }
        
        [Test]
        public void GetMinCost_ShouldReturnZero_IfFromAndToAreTheSame()
        {
            var matrix = new int[,]
            {
                {3, 4},
                {5, 6}
            };
            var from = new Vector2Int(0, 0);
            var to = new Vector2Int(0, 0);
            var searcher = new DijkstraPathMinCostSearcher();
            
            Assert.AreEqual(0, searcher.GetMinCost(matrix, from, to));
        }

        
        private struct GetMinCostTestCase
        {
            public int[,] Matrix { get; set; }
            public Vector2Int From { get; set; }
            public Vector2Int To { get; set; }
            public int ExpectedResult { get; set; }
        }
        
        [Test]
        //This test checks the general behaviour of the class.
        //For more details check the IMatrixPathMinCostSearcher interface summary.
        public void GetMinCost_ShouldReturnCorrectSum_OfPathValuesBetweenFromAndTo([Range(1, 3)] int testCaseNumber)
        {
            var testCase = GetTestCase(testCaseNumber);
            var searcher = new DijkstraPathMinCostSearcher();
            var matrix = testCase.Matrix;
            var from = testCase.From;
            var to = testCase.To;
            var expectedResult = testCase.ExpectedResult;
            
            var actualResult = searcher.GetMinCost(matrix, from, to);

            Assert.AreEqual(expectedResult, actualResult);
        }
        
        private GetMinCostTestCase GetTestCase(int testCaseNumber)
        {
            if (testCaseNumber == 1)
            {
                return new GetMinCostTestCase
                {
                    Matrix = new [, ]
                    {
                        {1, 2},
                        {3, 4}
                    },
                    From = new Vector2Int(0, 0),
                    To = new Vector2Int(1, 1),
                    ExpectedResult = 4
                };
            }

            if (testCaseNumber == 2)
            {
                return new GetMinCostTestCase
                {
                    Matrix = new [, ]
                    {
                        {1, 1, 4},
                        {2, 3, 4},
                        {3, 3, 3}
                    },
                    From = new Vector2Int(0, 2),
                    To = new Vector2Int(2, 0),
                    ExpectedResult = 6
                };
            }
            else
            {
                return new GetMinCostTestCase
                {
                    Matrix = new [, ]
                    {
                        {0, 3, 4, 4, 1},
                        {1, 3, 4, 4, 1},
                        {1, 3, 3, 3, 1},
                        {1, 1, 1, 1, 3},
                        {1, 1, 1, 1, 0}
                    },
                    From = new Vector2Int(0, 0),
                    To = new Vector2Int(4, 4),
                    ExpectedResult = 5
                };
            }
        }
    }
}