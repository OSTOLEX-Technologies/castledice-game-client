using System;
using castledice_game_data_logic;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src;
using Src.GameplayPresenter.Cells;
using Src.GameplayView;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode
{
    public class CellsPresenterTests
    {
        private static CellType[] CellTypes = new CellType[] { CellType.Square , CellType.Triangle};
        
        [Test]
        public void GenerateCells_ShouldPutGameStartDataFromSingleton_ToGivenCellViewMapProvider()
        {
            var gameStartData = GetGameStartData();
            var mapProviderMock = new Mock<ICellViewMapProvider>();
            var cellPresenter = new CellsPresenterBuilder
            {
                CellViewMapProvider = mapProviderMock.Object
            }.Build();
            Singleton<GameStartData>.Register(gameStartData);

            cellPresenter.GenerateCells();
            
            mapProviderMock.Verify(mp => mp.GetCellViewMap(gameStartData), Times.Once);
        }
        
        [Test]
        public void GenerateCells_ShouldPutCellViewMapFromGivenCellViewMapProvider_ToGivenCellsView()
        {
            var cellViewMap = new CellViewData[0, 0];
            var mapProviderMock = new Mock<ICellViewMapProvider>();
            mapProviderMock.Setup(mp => mp.GetCellViewMap(It.IsAny<GameStartData>())).Returns(cellViewMap);
            var cellsViewMock = new Mock<CellsView>();
            Singleton<GameStartData>.Register(GetGameStartData());
            var cellPresenter = new CellsPresenterBuilder
            {
                CellViewMapProvider = mapProviderMock.Object,
                CellsView = cellsViewMock.Object
            }.Build();

            cellPresenter.GenerateCells();
            
            cellsViewMock.Verify(cv => cv.GenerateCells(It.IsAny<CellType>(), cellViewMap), Times.Once);
        }
        
        [Test]
        public void GenerateCells_ShouldPutCellTypeFromGameStartData_ToGivenCellsView([ValueSource(nameof(CellTypes))]CellType cellType)
        {
            var gameStartData = GetGameStartData(cellType: cellType);
            var mapProviderMock = new Mock<ICellViewMapProvider>();
            var cellsViewMock = new Mock<CellsView>();
            Singleton<GameStartData>.Register(gameStartData);
            var cellPresenter = new CellsPresenterBuilder
            {
                CellViewMapProvider = mapProviderMock.Object,
                CellsView = cellsViewMock.Object
            }.Build();

            cellPresenter.GenerateCells();
            
            cellsViewMock.Verify(cv => cv.GenerateCells(cellType, It.IsAny<CellViewData[,]>()), Times.Once);
        }

        [TearDown]
        public void UnregisterSingletons()
        {
            try
            {
                Singleton<GameStartData>.Unregister();
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public class CellsPresenterBuilder
        {
            public ICellViewMapProvider CellViewMapProvider { get; set; } = new Mock<ICellViewMapProvider>().Object;
            public CellsView CellsView { get; set; } = new Mock<CellsView>().Object;

            public CellsPresenter Build()
            {
                return new CellsPresenter(CellViewMapProvider, CellsView);
            }
        }
    }
}