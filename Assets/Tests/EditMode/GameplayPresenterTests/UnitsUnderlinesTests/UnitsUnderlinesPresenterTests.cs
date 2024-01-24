using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.UnitsUnderlines;
using Src.GameplayView.UnitsUnderlines;
using UnityEngine;
using static Tests.ObjectCreationUtility;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.GameplayPresenterTests.UnitsUnderlinesTests
{
    public class UnitsUnderlinesPresenterTests
    {
        [Test]
        public void Presenter_ShouldShowUnderlineOnView_WhenPlayerOwnedContentAddedOnCell()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IUnitsUnderlinesView>();
            var presenter = new UnitsUnderlinesPresenter(board, viewMock.Object);
            
            board[randomPosition].AddContent(GetPlayerOwnedContent());
            
            viewMock.Verify(x => x.ShowUnderline(It.IsAny<Vector2Int>(), It.IsAny<Player>()), Times.Once);
        }

        [Test]
        public void Presenter_ShouldNotShowUnderlineOnView_IfAddedContentIsNotPlayerOwned()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IUnitsUnderlinesView>();
            var presenter = new UnitsUnderlinesPresenter(board, viewMock.Object);
            
            board[randomPosition].AddContent(GetCellContent());
            
            viewMock.Verify(x => x.ShowUnderline(It.IsAny<Vector2Int>(), It.IsAny<Player>()), Times.Never);
        }

        [Test]
        public void Presenter_ShouldShowUnderlineOnView_WithCorrectPositionAndPlayer()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IUnitsUnderlinesView>();
            var player = GetPlayer();
            var presenter = new UnitsUnderlinesPresenter(board, viewMock.Object);
            
            board[randomPosition].AddContent(GetPlayerOwnedContent(player));
            
            viewMock.Verify(x => x.ShowUnderline(randomPosition, player), Times.Once);
        }

        [Test]
        public void Presenter_ShouldHideUnderlineOnView_IfPlayerOwnedContentRemovedFromCell()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IUnitsUnderlinesView>();
            var presenter = new UnitsUnderlinesPresenter(board, viewMock.Object);
            var playerOwnedContent = GetPlayerOwnedContent();
            
            board[randomPosition].AddContent(playerOwnedContent);
            board[randomPosition].RemoveContent(playerOwnedContent);
            
            viewMock.Verify(x => x.HideUnderline(It.IsAny<Vector2Int>()), Times.Once);
        }
        
        [Test]
        public void Presenter_ShouldNotHideUnderlineOnView_IfRemovedContentIsNotPlayerOwned()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IUnitsUnderlinesView>();
            var presenter = new UnitsUnderlinesPresenter(board, viewMock.Object);
            var content = GetCellContent();
            
            board[randomPosition].AddContent(content);
            board[randomPosition].RemoveContent(content);
            
            viewMock.Verify(x => x.HideUnderline(It.IsAny<Vector2Int>()), Times.Never);
        }
        
        [Test]
        public void Presenter_ShouldHideUnderlineOnView_WithCorrectPosition()
        {
            var board = GetFullNByNBoard(10);
            var randomPosition = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
            var viewMock = new Mock<IUnitsUnderlinesView>();
            var presenter = new UnitsUnderlinesPresenter(board, viewMock.Object);
            var playerOwnedContent = GetPlayerOwnedContent();
            
            board[randomPosition].AddContent(playerOwnedContent);
            board[randomPosition].RemoveContent(playerOwnedContent);
            
            viewMock.Verify(x => x.HideUnderline(randomPosition), Times.Once);
        }
    }
}