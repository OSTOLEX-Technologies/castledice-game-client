using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.PlacedUnitsHighlights;
using Src.GameplayView.PlacedUnitsHighlights;
using UnityEngine;
using static Tests.ObjectCreationUtility;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.GameplayPresenterTests.PlacedUnitsHighlightsTests
{
    public class PlacedUnitsHighlightsPresenterTests
    {
        [Test]
        public void Presenter_ShouldShowHighlightOnView_WhenPlayerOwnedContentAddedOnCell()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IPlacedUnitsHighlightsView>();
            var presenter = new PlacedUnitsHighlightsPresenter(board, viewMock.Object);
            
            board[randomPosition].AddContent(GetPlayerOwnedContent());
            
            viewMock.Verify(x => x.ShowHighlight(It.IsAny<Vector2Int>(), It.IsAny<Player>()), Times.Once);
        }

        [Test]
        public void Presenter_ShouldNotShowHighlightOnView_IfAddedContentIsNotPlayerOwned()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IPlacedUnitsHighlightsView>();
            var presenter = new PlacedUnitsHighlightsPresenter(board, viewMock.Object);
            
            board[randomPosition].AddContent(GetCellContent());
            
            viewMock.Verify(x => x.ShowHighlight(It.IsAny<Vector2Int>(), It.IsAny<Player>()), Times.Never);
        }

        [Test]
        public void Presenter_ShouldShowHighlightOnView_WithCorrectPositionAndPlayer()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IPlacedUnitsHighlightsView>();
            var player = GetPlayer();
            var presenter = new PlacedUnitsHighlightsPresenter(board, viewMock.Object);
            
            board[randomPosition].AddContent(GetPlayerOwnedContent(player));
            
            viewMock.Verify(x => x.ShowHighlight(randomPosition, player), Times.Once);
        }

        [Test]
        public void Presenter_ShouldHideHighlightOnView_IfPlayerOwnedContentRemovedFromCell()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IPlacedUnitsHighlightsView>();
            var presenter = new PlacedUnitsHighlightsPresenter(board, viewMock.Object);
            var playerOwnedContent = GetPlayerOwnedContent();
            
            board[randomPosition].AddContent(playerOwnedContent);
            board[randomPosition].RemoveContent(playerOwnedContent);
            
            viewMock.Verify(x => x.HideHighlight(It.IsAny<Vector2Int>()), Times.Once);
        }
        
        [Test]
        public void Presenter_ShouldNotHideHighlightOnView_IfRemovedContentIsNotPlayerOwned()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IPlacedUnitsHighlightsView>();
            var presenter = new PlacedUnitsHighlightsPresenter(board, viewMock.Object);
            var content = GetCellContent();
            
            board[randomPosition].AddContent(content);
            board[randomPosition].RemoveContent(content);
            
            viewMock.Verify(x => x.HideHighlight(It.IsAny<Vector2Int>()), Times.Never);
        }
        
        [Test]
        public void Presenter_ShouldHideHighlightOnView_WithCorrectPosition()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IPlacedUnitsHighlightsView>();
            var presenter = new PlacedUnitsHighlightsPresenter(board, viewMock.Object);
            var playerOwnedContent = GetPlayerOwnedContent();
            
            board[randomPosition].AddContent(playerOwnedContent);
            board[randomPosition].RemoveContent(playerOwnedContent);
            
            viewMock.Verify(x => x.HideHighlight(randomPosition), Times.Once);
        }
    }
}