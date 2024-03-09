using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using static Tests.Utils.ContentMocksCreationUtility;
using static Tests.Utils.ObjectCreationUtility;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.PVE.Checkers;

namespace Tests.EditMode.PVETests.CheckersTests
{
    public class BaseCaptureCheckerTests
    {
        [Test]
        public void WillCaptureBase_ShouldReturnFalse_IfMoveIsNotCaptureMove()
        {
            var moveMock = new Mock<AbstractMove>(GetPlayer(), new Vector2Int(0, 0));
            var board = GetFullNByNBoard(10);
            var capturableContent = GetCapturableContent(1, It.IsAny<Player>());
            board[0, 0].AddContent(capturableContent);
            var baseCaptureChecker = new BaseCaptureChecker(board);
            
            var result = baseCaptureChecker.WillCaptureBase(moveMock.Object);
            
            Assert.IsFalse(result);
        }

        [Test]
        public void WillCaptureBase_ShouldReturnFalse_IfNoCapturableContentOnCell()
        {
            var board = GetFullNByNBoard(10);
            var content = GetContent();
            board[0, 0].AddContent(content);
            var move = new CaptureMove(GetPlayer(), (0, 0));
            var baseCaptureChecker = new BaseCaptureChecker(board);
            
            var result = baseCaptureChecker.WillCaptureBase(move);
            
            Assert.IsFalse(result);
        }

        [Test]
        public void WillCaptureBase_ShouldReturnFalse_IfCanBeCapturedReturnsFalseOnCapturable()
        {
            var player = GetPlayer();
            var move = new CaptureMove(player, (0, 0));
            var board = GetFullNByNBoard(10);
            var contentMock = new Mock<Content>();
            var capturableMock = contentMock.As<ICapturable>();
            capturableMock.Setup(c => c.CanBeCaptured(player)).Returns(false);
            board[0, 0].AddContent(contentMock.Object);
            var baseCaptureChecker = new BaseCaptureChecker(board);
            
            var result = baseCaptureChecker.WillCaptureBase(move);
            
            Assert.IsFalse(result);
        }
    }
}