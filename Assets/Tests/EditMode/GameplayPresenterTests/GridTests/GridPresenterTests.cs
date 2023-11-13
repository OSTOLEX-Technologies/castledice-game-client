using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.Grid;
using Src.GameplayView.Grid;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.GridTests
{
    public class GridPresenterTests
    {
        [Test]
        public void GenerateGrid_ShouldPassCellTypeAndCellsPresenceMatrixFromBoardData_ToGridView()
        {
            var viewMock = new Mock<IGridView>();
            var boardData = GetBoardData();
            var gridPresenter = new GridPresenter(viewMock.Object, boardData);
            
            gridPresenter.GenerateGrid();
            
            viewMock.Verify(v => v.GenerateGrid(boardData.CellType, boardData.CellsPresence), Times.Once);
        }
    }
}