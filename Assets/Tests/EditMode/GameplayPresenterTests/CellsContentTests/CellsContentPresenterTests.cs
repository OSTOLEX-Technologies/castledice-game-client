using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.CellsContent;
using Src.GameplayView.CellsContent;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.CellsContentTests
{
    public class CellsContentPresenterTests
    {
        [Test]
        public void Presenter_ShouldCallAddViewForContentWithProperParameters_IfContentAddedToTheBoard()
        {
            var viewMock = new Mock<ICellsContentView>();
            var board = GetFullNByNBoard(10);
            var presenter = new CellsContentPresenter(viewMock.Object, board);
            var contentToAdd = GetCellContent();
            var addPosition = new Vector2Int(1, 3);
            
            board[addPosition].AddContent(contentToAdd);
            
            viewMock.Verify(view => view.AddViewForContent(addPosition, contentToAdd), Times.Once);
        }

        [Test]
        public void Presenter_ShouldCallRemoveViewForContentWithProperParameters_IfContentRemovedFromTheBoard()
        {
            var viewMock = new Mock<ICellsContentView>();
            var board = GetFullNByNBoard(10);
            var presenter = new CellsContentPresenter(viewMock.Object, board);
            var contentToRemove = GetCellContent();
            
            board[(0, 0)].AddContent(contentToRemove);
            board[(0, 0)].RemoveContent(contentToRemove);
            
            viewMock.Verify(view => view.RemoveViewForContent(contentToRemove), Times.Once);
        }

        [Test]
        public void Presenter_ShouldCallUpdateViewForContent_IfContentStateWasModified()
        {
            var viewMock = new Mock<ICellsContentView>();
            var board = GetFullNByNBoard(10);
            var presenter = new CellsContentPresenter(viewMock.Object, board);
            var content = new ContentStub();
            
            board[(0, 0)].AddContent(content);
            content.ModifyState();
            
            viewMock.Verify(view => view.UpdateViewForContent(content), Times.Once);
        }

        [Test]
        public void Presenter_ShouldNotCallUpdateViewForContent_IfContentWasRemoved()
        {
            var viewMock = new Mock<ICellsContentView>();
            var board = GetFullNByNBoard(10);
            var presenter = new CellsContentPresenter(viewMock.Object, board);
            var content = new ContentStub();
            
            board[(0, 0)].AddContent(content);
            board[(0, 0)].RemoveContent(content);
            content.ModifyState();
            
            viewMock.Verify(view => view.UpdateViewForContent(content), Times.Never);
        }

        [Test]
        public void Presenter_ShouldCallAddViewForContent_IfContentAlreadyExistsOnBoard()
        {
            var viewMock = new Mock<ICellsContentView>();
            var content = GetCellContent();
            var board = GetFullNByNBoard(10);
            var position = new Vector2Int(0, 0);
            board[position].AddContent(content);
            
            var presenter = new CellsContentPresenter(viewMock.Object, board);
            
            viewMock.Verify(view => view.AddViewForContent(position, content), Times.Once);
        }

        private class ContentStub : Content
        {
            public override void Update()
            {
                throw new System.NotImplementedException();
            }

            public void ModifyState()
            {
                OnStateModified();
            }

            public override T Accept<T>(IContentVisitor<T> visitor)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}