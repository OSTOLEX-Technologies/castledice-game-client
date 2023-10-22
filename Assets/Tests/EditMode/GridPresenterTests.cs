using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.Grid;
using Src.GameplayView.Grid;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode
{
    public class GridPresenterTests
    {
        [Test]
        public void GenerateGrid_ShouldPassCellTypeAndCellsPresenceMatrixFromGameStartData_ToGridView()
        {
            var viewMock = new Mock<IGridView>();
            var gameStartData = GetGameStartData();
            var gridPresenter = new GridPresenter(viewMock.Object, gameStartData);
            
            gridPresenter.GenerateGrid();
            
            viewMock.Verify(v => v.GenerateGrid(gameStartData.CellType, gameStartData.CellsPresence), Times.Once);
        }
    }
}