using Moq;
using NUnit.Framework;
using Src.General;
using Src.PVE.MoveSearchers.TraitBasedSearchers.TraitsEvaluators;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.MoveSearchersTests.TraitsEvaluatorsTests
{
    public class PositionValidityEvaluatorTests
    {
        [Test]
        public void EvaluateTrait_ShouldReturnZero_IfMovePositionIsNotInProvidedList()
        {
            var allowedPositions = GetRandomVector2IntList(0, 10, 10);
            var positionsProviderMock = new Mock<IPositionsProvider>();
            positionsProviderMock.Setup(p => p.GetPositions()).Returns(allowedPositions);
            var movePosition = GetRandomVector2Int(10, 20);
            var move = GetMove(GetPlayer(), movePosition);
            var positionValidityEvaluator = new PositionValidityEvaluator(positionsProviderMock.Object);
            
            var result = positionValidityEvaluator.EvaluateTrait(move);
            
            Assert.AreEqual(0, result);
        }
        
        [Test]
        public void EvaluateTrait_ShouldReturnOne_IfMovePositionIsInProvidedList()
        {
            var allowedPositions = GetRandomVector2IntList(0, 10, 10);
            var positionsProviderMock = new Mock<IPositionsProvider>();
            positionsProviderMock.Setup(p => p.GetPositions()).Returns(allowedPositions);
            var movePosition = allowedPositions[0];
            var move = GetMove(GetPlayer(), movePosition);
            var positionValidityEvaluator = new PositionValidityEvaluator(positionsProviderMock.Object);
            
            var result = positionValidityEvaluator.EvaluateTrait(move);
            
            Assert.AreEqual(1, result);
        }
    }
}