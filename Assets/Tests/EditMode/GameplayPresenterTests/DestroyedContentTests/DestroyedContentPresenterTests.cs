using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.DestroyedContent;
using Src.GameplayView.DestroyedContent;
using static Tests.ObjectCreationUtility;

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
        public void Presenter_ShouldRemoveAlreadyShownDestroyedContentFromView_AfterTurnWasSwitched()
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
            //First turn switch should make presenter show destroyed content
            gameMock.Object.SwitchTurn();
            //Second turn switch should make presenter remove destroyed content
            gameMock.Object.SwitchTurn();
            
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
            var contentPositions = GetRandomVector2IntList(0, 9, 10);
            var content = GetCellContentList(10);
            var viewMock = new Mock<IDestroyedContentView>();
            var presenter = new DestroyedContentPresenter(gameMock.Object, viewMock.Object);
            
            AddContentToBoard(board, contentPositions, content);
            RemoveContentFromBoard(board, contentPositions, content);
            //First turn switch should make presenter show destroyed content
            gameMock.Object.SwitchTurn();
            //Second turn switch should make presenter remove destroyed content
            gameMock.Object.SwitchTurn();
            //Third turn switch should not make presenter remove destroyed content again
            gameMock.Object.SwitchTurn();
            
            for (int i = 0; i < 10; i++)
            {
                var position = contentPositions[i];
                var cellContent = content[i];
                //If third turn switch didn't make presenter remove destroyed content again, it should be called only once
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