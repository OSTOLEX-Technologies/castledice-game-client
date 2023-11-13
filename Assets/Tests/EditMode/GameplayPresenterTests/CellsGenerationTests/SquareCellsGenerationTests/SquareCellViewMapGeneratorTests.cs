using System.Collections.Generic;
using castledice_game_data_logic.ConfigsData;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.CellsGeneration;
using Src.GameplayPresenter.CellsGeneration.SquareCellsGeneration;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.CellsGenerationTests.SquareCellsGenerationTests
{
    public class SquareCellViewMapGeneratorTests
    {
        [TestCaseSource(nameof(GetCellViewMapTestCases))]
        public void GetCellViewMap_ShouldGenerateAppropriateCellViewDataMap(BoardData boardData, ISquareCellAssetIdProvider assetIdProvider, CellViewData[,] expectedCellViewDataMap)
        {
            var mapGenerator = new SquareCellViewMapGenerator(assetIdProvider);
            var actualCellViewDataMap = mapGenerator.GetCellViewMap(boardData);
            for (int i = 0; i < actualCellViewDataMap.GetLength(0); i++)
            {
                for (int j = 0; j < actualCellViewDataMap.GetLength(1); j++)
                {
                    Assert.AreEqual(expectedCellViewDataMap[i, j], actualCellViewDataMap[i, j]);
                }
            }
        }
        
        public static object[] GetCellViewMapTestCases =
        {
            new object[]
            {
                GetBoardData(new[,]
                {
                    { true, true },
                    { true, true }
                }),
                GetSquareCellAssetIdProvider(),
                new[,]
                {
                    {new CellViewData(3, false), new CellViewData(4, false)},
                    {new CellViewData(1, false), new CellViewData(2, false)}
                }
            },
            new object[]
            {
                GetBoardData(new[,]
                {
                    { true, true, true },
                    { true, true, true },
                    { true, true, true }

                }),
                GetSquareCellAssetIdProvider(),
                new[,]
                {
                    {new CellViewData(3, false), new CellViewData(6, false), new CellViewData(4, false)},
                    {new CellViewData(7, false), new CellViewData(9, false), new CellViewData(8, false)},
                    {new CellViewData(1, false), new CellViewData(5, false), new CellViewData(2, false)}
                }
            }
        };
        
        
        /// <summary>
        /// This method returns CellAssetIdProvider that provides following ids for different SquareCellData:
        /// 1 - upper left corner
        /// 2 - upper right corner
        /// 3 - lower left corner
        /// 4 - lower right corner
        /// 5 - upper border
        /// 6 - lower border
        /// 7 - left border
        /// 8 - right border
        /// 9 - middle cell
        /// </summary>
        /// <returns></returns>
        private static ISquareCellAssetIdProvider GetSquareCellAssetIdProvider()
        {
            var provider = new Mock<ISquareCellAssetIdProvider>();
            var upperLeftCorner = new SquareCellData(VerticalOrientationType.Upper, HorizontalOrientationType.Left,
                SquareCellBorderType.Corner);
            var upperRightCorner = new SquareCellData(VerticalOrientationType.Upper, HorizontalOrientationType.Right,
                SquareCellBorderType.Corner);
            var lowerLeftCorner = new SquareCellData(VerticalOrientationType.Lower, HorizontalOrientationType.Left,
                SquareCellBorderType.Corner);
            var lowerRightCorner = new SquareCellData(VerticalOrientationType.Lower, HorizontalOrientationType.Right,
                SquareCellBorderType.Corner);
            var upperBorder = new SquareCellData(VerticalOrientationType.Upper, HorizontalOrientationType.Middle,
                SquareCellBorderType.OneBorder);
            var lowerBorder = new SquareCellData(VerticalOrientationType.Lower, HorizontalOrientationType.Middle,
                SquareCellBorderType.OneBorder);
            var leftBorder = new SquareCellData(VerticalOrientationType.Middle, HorizontalOrientationType.Left,
                SquareCellBorderType.OneBorder);
            var rightBorder = new SquareCellData(VerticalOrientationType.Middle, HorizontalOrientationType.Right,
                SquareCellBorderType.OneBorder);
            var middleCell = new SquareCellData(VerticalOrientationType.Middle, HorizontalOrientationType.Middle,   
                SquareCellBorderType.None);
            provider.Setup(p => p.GetAssetIds(upperLeftCorner)).Returns(new List<int>(new[] {1}));
            provider.Setup(p => p.GetAssetIds(upperRightCorner)).Returns(new List<int>(new[] {2}));
            provider.Setup(p => p.GetAssetIds(lowerLeftCorner)).Returns(new List<int>(new[] {3}));
            provider.Setup(p => p.GetAssetIds(lowerRightCorner)).Returns(new List<int>(new[] {4}));
            provider.Setup(p => p.GetAssetIds(upperBorder)).Returns(new List<int>(new[] {5}));
            provider.Setup(p => p.GetAssetIds(lowerBorder)).Returns(new List<int>(new[] {6}));
            provider.Setup(p => p.GetAssetIds(leftBorder)).Returns(new List<int>(new[] {7}));
            provider.Setup(p => p.GetAssetIds(rightBorder)).Returns(new List<int>(new[] {8}));
            provider.Setup(p => p.GetAssetIds(middleCell)).Returns(new List<int>(new[] {9}));
            return provider.Object;
        }
    }
}