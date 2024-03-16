using NUnit.Framework;
using Src.PVE;

namespace Tests.EditMode.PVETests
{
    public class DfsUnconnectedValuesCutterTests
    {
        private struct CutUnconnectedValuesTestCase<T>
        {
            public T[,] Matrix;
            public T UnitState;
            public T BaseState;
            public T FreeState;
            public T[,] ExpectedMatrix;
        }
        
        [Test]
        public void CutUnconnectedValues_ShouldCutUnconnectedValues_AccordingToTheMethodDescription([Range(1, 4)]int testCaseNumber)
        {
            var testCase = GetCutUnconnectedValuesTestCase(testCaseNumber);
            var cutter = new DfsUnconnectedValuesCutter<int>();
            var matrix = testCase.Matrix;
            var unitState = testCase.UnitState;
            var baseState = testCase.BaseState;
            var freeState = testCase.FreeState;
            var expectedMatrix = testCase.ExpectedMatrix;
            
            cutter.CutUnconnectedValues(matrix, unitState, baseState, freeState);
            
            Assert.AreEqual(expectedMatrix, matrix);
        }
        
        private static CutUnconnectedValuesTestCase<int> GetCutUnconnectedValuesTestCase(int testCaseNumber)
        {
            //Checking cutting of unconnected values in a matrix
            if (testCaseNumber == 1)
            {
                return new CutUnconnectedValuesTestCase<int>
                {
                    Matrix = new [,]
                    {
                        {2, 1, 1, 0, 1},
                        {1, 1, 1, 0, 1},
                        {0, 0, 0, 0, 1},
                        {1, 1, 1, 1, 1}
                    },
                    UnitState = 1,
                    BaseState = 2,
                    FreeState = 0,
                    ExpectedMatrix = new [,]
                    {
                        {2, 1, 1, 0, 0},
                        {1, 1, 1, 0, 0},
                        {0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0}
                    }
                };
            }
            //Checking edge case when matrix has only one cell
            if (testCaseNumber == 2)
            {
                return new CutUnconnectedValuesTestCase<int>
                {
                    Matrix = new[,]
                    {
                        { 2 }
                    },
                    UnitState = 1,
                    BaseState = 2,
                    FreeState = 0,
                    ExpectedMatrix = new[,]
                    {
                        { 2 }
                    }
                };
            }
            //Checking ability to take into account diagonal connections
            if (testCaseNumber == 3)
            {
                return new CutUnconnectedValuesTestCase<int>
                {
                    Matrix = new[,]
                    {
                        { 1, 0, 0, 0, 0 },
                        { 0, 1, 1, 0, 1 },
                        { 0, 0, 2, 0, 1 },
                        { 0, 0, 0, 0, 1 }
                    },
                    UnitState = 1,
                    BaseState = 2,
                    FreeState = 0,
                    ExpectedMatrix = new[,]
                    {
                        { 1, 0, 0, 0, 0 },
                        { 0, 1, 1, 0, 0 },
                        { 0, 0, 2, 0, 0 },
                        { 0, 0, 0, 0, 0 }
                    }
                };
            }
            //Checking ability to ignore cells with states different than unitState, baseState and freeState
            else
            {
                return new CutUnconnectedValuesTestCase<int>
                {
                    Matrix = new[,]
                    {
                        { 1, 2, 0, 0, 0 },
                        { 0, 1, 1, 3, 1 },
                        { 0, 3, 3, 0, 1 },
                        { 0, 4, 0, 0, 1 }
                    },
                    UnitState = 1,
                    BaseState = 2,
                    FreeState = 0,
                    ExpectedMatrix = new[,]
                    {
                        { 1, 2, 0, 0, 0 },
                        { 0, 1, 1, 3, 0 },
                        { 0, 3, 3, 0, 0 },
                        { 0, 4, 0, 0, 0 }
                    }
                };
            }
        }
    }
}