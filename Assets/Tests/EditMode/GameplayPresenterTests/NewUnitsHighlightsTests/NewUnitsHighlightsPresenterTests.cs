using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.NewUnitsHighlights;
using Src.GameplayView.NewUnitsHighlights;
using UnityEngine;
using static Tests.Utils.ObjectCreationUtility;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.GameplayPresenterTests.NewUnitsHighlightsTests
{
    public class NewUnitsHighlightsPresenterTests
    {
        [Test]
        //New in this test means that the content was added during previous turn
        public void Presenter_ShouldCallShowHighlightOnView_WithPositionsOfNewPlayerOwnedContent()
        {
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.SwitchTurn()).Raises(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            var positions = new List<Vector2Int>
                { new(Random.Range(0, 5), Random.Range(0, 5)), new(Random.Range(6, 9), Random.Range(6, 9)) };
            var board = GetFullNByNBoard(10);
            gameMock.Setup(g => g.GetBoard()).Returns(board);
            var viewMock = new Mock<INewUnitsHighlightsView>();
            var presenter = new NewUnitsHighlightsPresenter(gameMock.Object, viewMock.Object);

            foreach (var pos in positions)
            {
                board[pos].AddContent(GetPlayerOwnedContent());
            }

            gameMock.Object.SwitchTurn();

            foreach (var pos in positions)
            {
                viewMock.Verify(view => view.ShowHighlight(pos, It.IsAny<Player>()), Times.Once);
            }
        }

        [Test]
        public void Presenter_ShouldNotCallShowHighlightOnView_IfNewContentIsNotPlayerOwned()
        {
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.SwitchTurn()).Raises(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            var positions = new List<Vector2Int>
                { new(Random.Range(0, 5), Random.Range(0, 5)), new(Random.Range(6, 9), Random.Range(6, 9)) };
            var board = GetFullNByNBoard(10);
            gameMock.Setup(g => g.GetBoard()).Returns(board);
            var viewMock = new Mock<INewUnitsHighlightsView>();
            var presenter = new NewUnitsHighlightsPresenter(gameMock.Object, viewMock.Object);

            foreach (var pos in positions)
            {
                board[pos].AddContent(GetCellContent());
            }

            gameMock.Object.SwitchTurn();

            viewMock.Verify(view => view.ShowHighlight(It.IsAny<Vector2Int>(), It.IsAny<Player>()), Times.Never);
        }

        [Test]
        public void Presenter_ShouldCallShowHighlightOnView_WithPreviousPlayer()
        {
            var gameMock = GetGameMock();
            var expectedPlayer = GetPlayer();
            gameMock.Setup(g => g.SwitchTurn()).Raises(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            gameMock.Setup(g => g.GetPreviousPlayer()).Returns(expectedPlayer);
            var board = GetFullNByNBoard(10);
            gameMock.Setup(g => g.GetBoard()).Returns(board);
            var viewMock = new Mock<INewUnitsHighlightsView>();
            var presenter = new NewUnitsHighlightsPresenter(gameMock.Object, viewMock.Object);
            
            board[new Vector2Int(0, 0)].AddContent(GetPlayerOwnedContent());
            gameMock.Object.SwitchTurn();
            
            viewMock.Verify(view => view.ShowHighlight(It.IsAny<Vector2Int>(), expectedPlayer), Times.Once);
        }

        [Test]
        public void Presenter_ShouldCallHideHighlightsOnView_IfMoveIsAppliedInGame()
        {
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.TryMakeMove(It.IsAny<AbstractMove>()))
                .Returns(true).Raises(g => g.MoveApplied += null, gameMock.Object, GetMove());
            var board = GetFullNByNBoard(10);
            gameMock.Setup(g => g.GetBoard()).Returns(board);
            var viewMock = new Mock<INewUnitsHighlightsView>();
            var presenter = new NewUnitsHighlightsPresenter(gameMock.Object, viewMock.Object);
            
            gameMock.Object.TryMakeMove(GetMove());
            
            viewMock.Verify(view => view.HideHighlights(), Times.Once);
        }
    }
}