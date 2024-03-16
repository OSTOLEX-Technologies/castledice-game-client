using System.Collections.Generic;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.CellMovesHighlights;
using Src.GameplayView.CellMovesHighlights;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.CellMovesHighlightsTests
{
    public class CellMovesHighlightPresenterTests
    {
        [Test]
        public void HighlightCellMoves_ShouldBeCalledOnce_WhenTimeToHighlightEventRaised()
        {
            var observerMock = new Mock<ICellMovesHighlightObserver>();
            var viewMock = new Mock<ICellMovesHighlightView>();
            var cellMovesListProviderMock = new Mock<ICellMovesListProvider>();
            cellMovesListProviderMock.Setup(x => x.GetCellMovesList(It.IsAny<int>())).Returns(new List<CellMove>());
            var presenter = new CellMovesHighlightPresenter(GetPlayer(), cellMovesListProviderMock.Object, observerMock.Object, viewMock.Object);
            
            observerMock.Raise(x => x.TimeToHighlight += () => { });
            
            viewMock.Verify(x => x.HighlightCellMoves(It.IsAny<List<CellMove>>()), Times.Once);
        }
        
        [Test]
        public void HideHighlights_ShouldBeCalledOnce_WhenTimeToHideEventRaised()
        {
            var observerMock = new Mock<ICellMovesHighlightObserver>();
            var viewMock = new Mock<ICellMovesHighlightView>();
            var cellMovesListProviderMock = new Mock<ICellMovesListProvider>();
            var presenter = new CellMovesHighlightPresenter(GetPlayer(), cellMovesListProviderMock.Object, observerMock.Object, viewMock.Object);
            
            observerMock.Raise(x => x.TimeToHide += () => { });
            
            viewMock.Verify(x => x.HideHighlights(), Times.Once);
        }

        [Test]
        public void HideHighlights_ShouldBeCalledOnceBeforeHighlightCellMoves_WhenTimeToHighlightEventRaised()
        {
            var observerMock = new Mock<ICellMovesHighlightObserver>();
            var viewMock = new Mock<ICellMovesHighlightView>(MockBehavior.Strict);
            var mockSequence = new MockSequence();
            viewMock.InSequence(mockSequence).Setup(x => x.HideHighlights());
            viewMock.InSequence(mockSequence).Setup(x => x.HighlightCellMoves(It.IsAny<List<CellMove>>()));
            var cellMovesListProviderMock = new Mock<ICellMovesListProvider>();
            cellMovesListProviderMock.Setup(x => x.GetCellMovesList(It.IsAny<int>())).Returns(new List<CellMove>());
            var presenter = new CellMovesHighlightPresenter(GetPlayer(), cellMovesListProviderMock.Object, observerMock.Object, viewMock.Object);
            
            observerMock.Raise(x => x.TimeToHighlight += () => { });
            
            viewMock.Verify(x => x.HideHighlights(), Times.Once);
            viewMock.Verify(x => x.HighlightCellMoves(It.IsAny<List<CellMove>>()), Times.Once);
        }
    }
}