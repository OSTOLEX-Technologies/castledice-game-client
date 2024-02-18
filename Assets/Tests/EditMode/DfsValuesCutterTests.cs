using NUnit.Framework;
using Src.PVE.MoveSearchers.TraitsEvaluators;
using Src.PVE.TraitsEvaluators;

namespace Tests.EditMode
{
    public class DfsValuesCutterTests
    {
        private struct TestCase
        {
            public int UnitState;
            public int BaseState;
            public int FreeState;
            public int[,] InputMatrix;
            public int[,] ExpectedOutput;
        }

        [Test]
        public void CutUnconnectedUnits_ShouldReplaceUnconnectedUnits_WithFreeState([Range(1, 4)] int testCaseNumber)
        {
            var testCase = GetTestCase(testCaseNumber);
            var cutter = new DfsUnconnectedValuesCutter<int>();
            var actual = cutter.CutUnconnectedUnits(testCase.InputMatrix, testCase.UnitState, testCase.BaseState, testCase.FreeState);
            Assert.AreEqual(testCase.ExpectedOutput, actual);
        }

        private TestCase GetTestCase(int testCaseNumber)
        {
            var baseState = 1;
            var freeState = 0;
            var unitState = 2;
            if (testCaseNumber == 1)
            {
                return new TestCase
                {
                    UnitState = unitState,
                    FreeState = freeState,
                    BaseState = baseState,
                    InputMatrix = new int[,]
                    {
                        {1, 0, 0, 2},
                        {0, 2, 0, 2},
                        {0, 0, 0, 2},
                        {2, 2, 2, 2}
                    },
                    ExpectedOutput = new int[,]
                    {
                        {1, 0, 0, 0},
                        {0, 2, 0, 0},
                        {0, 0, 0, 0},
                        {0, 0, 0, 0}
                    },
                };
            }

            if (testCaseNumber == 2)
            {
                return new TestCase
                {
                    UnitState = unitState,
                    FreeState = freeState,
                    BaseState = baseState,
                    InputMatrix = new int[,]
                    {
                        {1, 0, 0, 2},
                        {0, 2, 0, 2},
                        {0, 0, 2, 2},
                        {2, 2, 2, 2}
                    },
                    ExpectedOutput = new int[,]
                    {
                        {1, 0, 0, 2},
                        {0, 2, 0, 2},
                        {0, 0, 2, 2},
                        {2, 2, 2, 2}
                    },
                };
            }

            if (testCaseNumber == 3)
            {
                return new TestCase()
                {
                    UnitState = unitState,
                    FreeState = freeState,
                    BaseState = baseState,
                    InputMatrix = new int[,]
                    {
                        { 3, 2, 2 },
                        { 2, 4, 0 },
                        { 2, 0, 1 },
                    },
                    ExpectedOutput = new int[,]
                    {
                        { 3, 0, 0 },
                        { 0, 4, 0 },
                        { 0, 0, 1 },
                    },
                };
            }
            else
            {
                return new TestCase()
                {
                    UnitState = unitState,
                    FreeState = freeState,
                    BaseState = baseState,
                    InputMatrix = new int[,]
                    {
                        {3, 0, 0, 0, 0, 0},
                        {0, 2, 0, 0, 0, 0},
                        {0, 0, 2, 0, 0, 0},
                        {0, 0, 0, 2, 0, 0},
                        {0, 0, 0, 0, 4, 0},
                        {0, 0, 0, 0, 0, 1},
                    },
                    ExpectedOutput = new int[,]
                    {
                        {3, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 4, 0},
                        {0, 0, 0, 0, 0, 1},
                    },
                };
            }
        }

        [Test]
        public void SpecialTestCase()
        {
            var inputMatrix = new CellState[,]
            {
                {CellState.FriendlyBase, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Enemy, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Enemy, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Enemy, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Friendly, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.EnemyBase}
            };
            var expectedMatrix = new CellState[,]
            {
                {CellState.FriendlyBase, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Friendly, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.EnemyBase}
            };
            var cutter = new DfsUnconnectedValuesCutter<CellState>();
            
            var actual = cutter.CutUnconnectedUnits(inputMatrix, CellState.Enemy, CellState.EnemyBase, CellState.Free);
            
            Assert.AreEqual(expectedMatrix, actual);
        }
    }  
}