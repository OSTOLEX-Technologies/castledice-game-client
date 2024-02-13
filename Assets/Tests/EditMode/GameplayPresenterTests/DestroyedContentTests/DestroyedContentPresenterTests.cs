using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.DestroyedContent;
using Src.GameplayView.DestroyedContent;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.DestroyedContentTests
{
    public class DestroyedContentPresenterTests
    {
        [Test]
        public void Presenter_ShouldShowDestroyedContentOnView_AfterTurnWasSwitched()
        {
            var gameMock = GetGameMock();
            var board = GetFullNByNBoard(10);
            gameMock.Setup(game => game.GetBoard()).Returns(board);
            gameMock.Setup(game => game.SwitchTurn()).Raises(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            var contentPositions = GetRandomVector2IntList(0, 9, 10);
            var content = GetCellContentList(10);
            var viewMock = new Mock<IDestroyedContentView>();
            var presenter = new DestroyedContentPresenter(gameMock.Object, viewMock.Object);
            
            AddContentToBoard(board, contentPositions, content);
            RemoveContentFromBoard(board, contentPositions, content);
            gameMock.Object.SwitchTurn();
            
            for (int i = 0; i < 10; i++)
            {
                var position = contentPositions[i];
                var cellContent = content[i];
                viewMock.Verify(view => view.ShowDestroyedContent(position, cellContent));
            }
        }

        [Test]
        public void Presenter_ShouldNotShowDestroyedContent_ThatHasAlreadyBeenShown()
        {
            var gameMock = GetGameMock();
            var board = GetFullNByNBoard(10);
            gameMock.Setup(game => game.GetBoard()).Returns(board);
            gameMock.Setup(game => game.SwitchTurn()).Raises(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            var contentPositions = GetRandomVector2IntList(0, 9, 10);
            var content = GetCellContentList(10);
            var viewMock = new Mock<IDestroyedContentView>();
            var presenter = new DestroyedContentPresenter(gameMock.Object, viewMock.Object);
            
            AddContentToBoard(board, contentPositions, content);
            RemoveContentFromBoard(board, contentPositions, content);
            gameMock.Object.SwitchTurn();
            gameMock.Object.SwitchTurn();
            
            //Checking if view was called only once with correct arguments
            for (int i = 0; i < 10; i++)
            {
                var position = contentPositions[i];
                var cellContent = content[i];
                viewMock.Verify(view => view.ShowDestroyedContent(position, cellContent), Times.Once);
            }
        }

        [Test]
        public void Presenter_ShouldNotShowDestroyedContent_IfThereIsOtherContentOnTheCell()
        {
            var gameMock = GetGameMock();
            var board = GetFullNByNBoard(10);
            gameMock.Setup(game => game.GetBoard()).Returns(board);
            gameMock.Setup(game => game.SwitchTurn()).Raises(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            var contentPosition = GetRandomVector2Int(0, 9);
            var content = GetCellContent();
            var viewMock = new Mock<IDestroyedContentView>();
            var presenter = new DestroyedContentPresenter(gameMock.Object, viewMock.Object);
            
            board[contentPosition].AddContent(content);
            board[contentPosition].AddContent(GetCellContent());
            board[contentPosition].RemoveContent(content);
            gameMock.Object.SwitchTurn();
            
            viewMock.Verify(view => view.ShowDestroyedContent(contentPosition, content), Times.Never);
        }

        [Test]
        public void Presenter_ShouldRemoveAlreadyShownDestroyedContentFromView_AfterMoveWasApplied()
        {
            var gameMock = GetGameMock();
            var board = GetFullNByNBoard(10);
            gameMock.Setup(game => game.GetBoard()).Returns(board);
            gameMock.Setup(game => game.SwitchTurn()).Raises(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            gameMock.Setup(game => game.TryMakeMove(It.IsAny<AbstractMove>())).Returns(true).Raises(g => g.MoveApplied+= null, gameMock.Object, GetMove());
            var contentPositions = GetRandomVector2IntList(0, 9, 10);
            var content = GetCellContentList(10);
            var viewMock = new Mock<IDestroyedContentView>();
            var presenter = new DestroyedContentPresenter(gameMock.Object, viewMock.Object);
            
            AddContentToBoard(board, contentPositions, content);
            RemoveContentFromBoard(board, contentPositions, content);
            //Turn switch should make presenter show destroyed content
            gameMock.Object.SwitchTurn();
            //Applying move should make presenter remove destroyed content
            gameMock.Object.TryMakeMove(GetMove());
            
            for (int i = 0; i < 10; i++)
            {
                var position = contentPositions[i];
                var cellContent = content[i];
                viewMock.Verify(view => view.RemoveDestroyedContent(position, cellContent));
            }
        }
        
        [Test]
        public void Presenter_ShouldNotRemoveDestroyedContentFromView_IfItWasAlreadyRemoved()
        {
            var gameMock = GetGameMock();
            var board = GetFullNByNBoard(10);
            gameMock.Setup(game => game.GetBoard()).Returns(board);
            gameMock.Setup(game => game.SwitchTurn()).Raises(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            gameMock.Setup(game => game.TryMakeMove(It.IsAny<AbstractMove>())).Returns(true).Raises(g => g.MoveApplied+= null, gameMock.Object, GetMove());
            var contentPositions = GetRandomVector2IntList(0, 9, 10);
            var content = GetCellContentList(10);
            var viewMock = new Mock<IDestroyedContentView>();
            var presenter = new DestroyedContentPresenter(gameMock.Object, viewMock.Object);
            
            AddContentToBoard(board, contentPositions, content);
            RemoveContentFromBoard(board, contentPositions, content);
            //Turn switch should make presenter show destroyed content
            gameMock.Object.SwitchTurn();
            //Applying move should make presenter remove destroyed content
            gameMock.Object.TryMakeMove(GetMove());
            //Applying move again should not make presenter remove destroyed content again
            gameMock.Object.TryMakeMove(GetMove());
            
            for (int i = 0; i < 10; i++)
            {
                var position = contentPositions[i];
                var cellContent = content[i];
                //If second time we applied move didn't make presenter remove destroyed content again, it should be called only once
                viewMock.Verify(view => view.RemoveDestroyedContent(position, cellContent), Times.Once);
            }
        }
        
        private static void AddContentToBoard(Board board, List<Vector2Int> position, List<Content> content)
        {
            for (int i = 0; i < position.Count; i++)
            {
                board[position[i].X, position[i].Y].AddContent(content[i]);
            }
        }
        
        private static void RemoveContentFromBoard(Board board, List<Vector2Int> position, List<Content> content)
        {
            for (int i = 0; i < position.Count; i++)
            {
                board[position[i].X, position[i].Y].RemoveContent(content[i]);
            }
        }
    }
}