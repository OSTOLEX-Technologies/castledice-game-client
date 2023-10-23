﻿using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.CellsContent;
using Src.GameplayView.CellsContent;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode
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
            var removePosition = new Vector2Int(1, 3);
            
            board[removePosition].AddContent(contentToRemove);
            board[removePosition].RemoveContent(contentToRemove);
            
            viewMock.Verify(view => view.RemoveViewForContent(contentToRemove), Times.Once);
        }

        [Test]
        public void Presenter_ShouldCallUpdateViewForContent_IfContentStateWasModified()
        {
            var viewMock = new Mock<ICellsContentView>();
            var board = GetFullNByNBoard(10);
            var presenter = new CellsContentPresenter(viewMock.Object, board);
            var content = new ContentStub();
            var position = new Vector2Int(1, 3);
            
            board[position].AddContent(content);
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
            var position = new Vector2Int(1, 3);
            
            board[position].AddContent(content);
            board[position].RemoveContent(content);
            content.ModifyState();
            
            viewMock.Verify(view => view.UpdateViewForContent(content), Times.Never);
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