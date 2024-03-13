using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.General.MoveConditions;
using static Tests.Utils.ContentMocksCreationUtility;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GeneralTests.MoveConditionsTests
{
    public class BaseCaptureConditionTests
    {
        [Test]
        public void IsSatisfiedBy_ShouldReturnFalse_IfMoveIsNotCaptureMove()
        {
            var moveMock = new Mock<AbstractMove>(GetPlayer(), new Vector2Int(0, 0));
            var board = GetFullNByNBoard(10);
            var capturableContent = GetCapturableContent(1, It.IsAny<Player>());
            board[0, 0].AddContent(capturableContent);
            var baseCaptureCondition = new BaseCaptureCondition(board);
            
            var result = baseCaptureCondition.IsSatisfiedBy(moveMock.Object);
            
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSatisfiedBy_ShouldReturnFalse_IfNoCapturableContentOnCell()
        {
            var board = GetFullNByNBoard(10);
            var content = GetContent();
            board[0, 0].AddContent(content);
            var move = new CaptureMove(GetPlayer(), (0, 0));
            var baseCaptureCondition = new BaseCaptureCondition(board);
            
            var result = baseCaptureCondition.IsSatisfiedBy(move);
            
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSatisfiedBy_ShouldReturnFalse_IfCanBeCapturedReturnsFalseOnCapturable()
        {
            var player = GetPlayer();
            var move = new CaptureMove(player, (0, 0));
            var board = GetFullNByNBoard(10);
            var contentMock = new Mock<Content>();
            var capturableMock = contentMock.As<ICapturable>();
            capturableMock.Setup(c => c.CanBeCaptured(player)).Returns(false);
            board[0, 0].AddContent(contentMock.Object);
            var baseCaptureCondition = new BaseCaptureCondition(board);
            
            var result = baseCaptureCondition.IsSatisfiedBy(move);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void IsSatisfiedBy_ShouldReturnFalse_IfMoreThanOneCaptureHitLeftOnCapturable()
        {
            var player = GetPlayer();
            var move = new CaptureMove(player, (0, 0));
            var board = GetFullNByNBoard(10);
            var contentMock = new Mock<Content>();
            var capturableMock = contentMock.As<ICapturable>();
            capturableMock.Setup(c => c.CanBeCaptured(player)).Returns(true);
            capturableMock.Setup(c => c.CaptureHitsLeft(player)).Returns(2);
            board[0, 0].AddContent(contentMock.Object);
            var baseCaptureCondition = new BaseCaptureCondition(board);
            
            var result = baseCaptureCondition.IsSatisfiedBy(move);
            
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSatisfiedBy_ShouldReturnTrue_IfOneCaptureHitLeftOnCapturable()
        {
            var player = GetPlayer();
            var move = new CaptureMove(player, (0, 0));
            var board = GetFullNByNBoard(10);
            var contentMock = new Mock<Content>();
            var capturableMock = contentMock.As<ICapturable>();
            capturableMock.Setup(c => c.CanBeCaptured(player)).Returns(true);
            capturableMock.Setup(c => c.CaptureHitsLeft(player)).Returns(1);
            board[0, 0].AddContent(contentMock.Object);
            var baseCaptureCondition = new BaseCaptureCondition(board);
            
            var result = baseCaptureCondition.IsSatisfiedBy(move);
            
            Assert.IsTrue(result);
        }
    }
}