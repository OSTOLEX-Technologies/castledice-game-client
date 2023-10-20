using System;

namespace Src.GameplayPresenter.Cells.SquareCellsGeneration
{
    public static class SquareCellDataMapGenerator
    {
        public static SquareCellData[,] GetSquareCellDataMap(bool[,] cellsPresenceMatrix)
        {
            if (cellsPresenceMatrix.GetLength(0) < 2 || cellsPresenceMatrix.GetLength(1) < 2)
            {
                throw new InvalidOperationException("Unsupported cells presence matrix. Matrix must be at least 2x2.");
            }
            var squareCellDataMap = new SquareCellData[cellsPresenceMatrix.GetLength(0), cellsPresenceMatrix.GetLength(1)];
            for (var i = 0; i < cellsPresenceMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < cellsPresenceMatrix.GetLength(1); j++)
                {
                    if (!cellsPresenceMatrix[i, j]) throw new InvalidOperationException("Unsupported cells presence matrix. All cells must be present.");
                    var verticalOrientationType = GetVerticalOrientationType(cellsPresenceMatrix, i, j);
                    var horizontalOrientationType = GetHorizontalOrientationType(cellsPresenceMatrix, i, j);
                    var borderType = GetBorderType(cellsPresenceMatrix, i, j);
                    squareCellDataMap[i, j] = new SquareCellData(verticalOrientationType, horizontalOrientationType, borderType);
                }
            }

            return squareCellDataMap;
        }

        private static VerticalOrientationType GetVerticalOrientationType(bool[,] cellsPresenceMatrix, int i, int j)
        {
            if (i == 0)
            {
                return VerticalOrientationType.Lower;
            }

            if (i == cellsPresenceMatrix.GetLength(0) - 1)
            {
                return VerticalOrientationType.Upper;
            }
            
            return VerticalOrientationType.Middle;
        }
        
        private static HorizontalOrientationType GetHorizontalOrientationType(bool[,] cellsPresenceMatrix, int i, int j)
        {
            if (j == 0)
            {
                return HorizontalOrientationType.Left;
            }

            if (j == cellsPresenceMatrix.GetLength(0) - 1)
            {
                return HorizontalOrientationType.Right;
            }
            
            return HorizontalOrientationType.Middle;
        }

        private static SquareCellBorderType GetBorderType(bool[,] cellsPresenceMatrix, int i, int j)
        {
            var cellIsCorner = (i == 0 || i == cellsPresenceMatrix.GetLength(0) - 1) && (j == 0 || j == cellsPresenceMatrix.GetLength(1) - 1);
            var cellIsMiddle = (i > 0 && i < cellsPresenceMatrix.GetLength(0) - 1) && (j > 0 && j < cellsPresenceMatrix.GetLength(1) - 1);
            if (cellIsCorner)
            {
                return SquareCellBorderType.Corner;
            }
            if(cellIsMiddle)
            {
                return SquareCellBorderType.NoBorder;
            }
            return SquareCellBorderType.OneBorder;
        }
    }
}