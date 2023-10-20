using System;
using NUnit.Framework;
using Src.GameplayPresenter.Cells;
using Src.GameplayPresenter.Cells.SquareCellsGeneration;

namespace Tests.EditMode
{
    public class SquareCellDataMapGeneratorTests
    {
        [TestCaseSource(nameof(GetSquareCellDataMapTestCases))]
        public void GetSquareCellDataMap_ShouldReturnAppropriateMatrixOfSquareCellData(bool[,] cellsPresenceMatrix,
            SquareCellData[,] expectedSquareCellDataMap)
        {
            var actualSquareCellDataMap = SquareCellDataMapGenerator.GetSquareCellDataMap(cellsPresenceMatrix);
            for (int i = 0; i < actualSquareCellDataMap.GetLength(0); i++)
            {
                for (int j = 0; j < actualSquareCellDataMap.GetLength(1); j++)
                {
                    Assert.AreEqual(expectedSquareCellDataMap[i, j], actualSquareCellDataMap[i, j]);
                }
            }
        }

        public static object[] GetSquareCellDataMapTestCases =
        {
            new object[]
            {
                new[,]
                {
                    { true, true },
                    { true, true }
                },
                new[,]
                {
                    {LowerLeftCorner(), LowerRightCorner()},
                    {UpperLeftCorner(), UpperRightCorner()}
                }
            },
            new object[]
            {
                new[,]
                {
                    {true, true, true},
                    {true, true, true},
                    {true, true, true}
                },
                new[,]
                {
                    {LowerLeftCorner(), LowerBorder(), LowerRightCorner()},
                    {LeftBorder(), MiddleCell(), RightBorder()},
                    {UpperLeftCorner(), UpperBorder(), UpperRightCorner()}
                }
            }
        };

        private static SquareCellData MiddleCell()
        {
            return new SquareCellData(VerticalOrientationType.Middle, HorizontalOrientationType.Middle, SquareCellBorderType.None);
        }

        private static SquareCellData UpperLeftCorner()
        {
            return new SquareCellData(VerticalOrientationType.Upper, HorizontalOrientationType.Left, SquareCellBorderType.Corner);
        }
        
        private static SquareCellData LowerLeftCorner()
        {
            return new SquareCellData(VerticalOrientationType.Lower, HorizontalOrientationType.Left, SquareCellBorderType.Corner);
        }
        
        private static SquareCellData UpperRightCorner()
        {
            return new SquareCellData(VerticalOrientationType.Upper, HorizontalOrientationType.Right, SquareCellBorderType.Corner);
        }
        
        private static SquareCellData LowerRightCorner()
        {
            return new SquareCellData(VerticalOrientationType.Lower, HorizontalOrientationType.Right, SquareCellBorderType.Corner);
        }

        private static SquareCellData UpperBorder()
        {
            return new SquareCellData(VerticalOrientationType.Upper, HorizontalOrientationType.Middle, SquareCellBorderType.OneBorder);
        }
        
        private static SquareCellData LowerBorder()
        {
            return new SquareCellData(VerticalOrientationType.Lower, HorizontalOrientationType.Middle, SquareCellBorderType.OneBorder);
        }
        
        private static SquareCellData LeftBorder()
        {
            return new SquareCellData(VerticalOrientationType.Middle, HorizontalOrientationType.Left, SquareCellBorderType.OneBorder);
        }
        
        private static SquareCellData RightBorder()
        {
            return new SquareCellData(VerticalOrientationType.Middle, HorizontalOrientationType.Right, SquareCellBorderType.OneBorder);
        }

        [TestCaseSource(nameof(UnsupportedCellsPresenceMatrixTestCases))]
        public void GetSquareCellDataMap_ShouldThrowInvalidOperationException_IfUnsupportedCellsPresenceMatrixIsGiven(bool[,] cellsPresenceMatrix)
        {
            Assert.Throws<InvalidOperationException>(() => SquareCellDataMapGenerator.GetSquareCellDataMap(cellsPresenceMatrix));
        }

        public static object[] UnsupportedCellsPresenceMatrixTestCases =
        {
            new object[]
            {
                new[,]
                {
                    { true }
                }
            },
            new object[]
            {
                new[,]
                {
                    { true, true, true }
                }
            },
            new object[]
            {
                new[,]
                {
                    { false, true, false }, 
                    { true, true, true }, 
                    { false, true, false }
                }
            },
            new object[]
            {
                new[,]
                {
                    { false, true, true }, 
                    { true, true, true }, 
                    { false, true, true }
                }
            },
            new object[]
            {
                new[,]
                {
                    { false, true, false }, 
                    { true, false, true }, 
                    { false, true, false }
                }
            },
            new object[]
            {
                new[,]
                {
                    { true, false, false }, 
                    { true, true, false }, 
                    { true, true, true }
                }
            }
        };
    }
}