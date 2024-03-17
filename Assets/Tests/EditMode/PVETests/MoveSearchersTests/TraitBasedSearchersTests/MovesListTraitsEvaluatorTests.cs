using System;
using System.Collections.Generic;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.PVE.MoveSearchers.TraitBasedSearchers;
using static Tests.Utils.ObjectCreationUtility;
using Src.PVE.MoveSearchers.TraitBasedSearchers.TraitsEvaluators;

namespace Tests.EditMode.PVETests.MoveSearchersTests.TraitBasedSearchersTests
{
    public class MovesListTraitsEvaluatorTests
    {
        [Test]
        public void EvaluateTraitsForMoves_ShouldReturnDictionaryWithMovesAndTheirTraitsValues_AccordingToTraitEvaluators()
        {
            var movesCount = new Random().Next(5, 15);
            var moves = GetMovesList(movesCount);
            var destructivenessEvaluatorMock = new Mock<IMoveTraitEvaluator>();
            var aggressivenessEvaluatorMock = new Mock<IMoveTraitEvaluator>();
            var defensivenessEvaluatorMock = new Mock<IMoveTraitEvaluator>();
            var enhancivenessEvaluatorMock = new Mock<IMoveTraitEvaluator>();
            var harmfulnessEvaluatorMock = new Mock<IMoveTraitEvaluator>();
            var expectedDestructivenessValues = GetListWithRandomFloats(movesCount);
            var expectedAggressivenessValues = GetListWithRandomFloats(movesCount);
            var expectedDefensivenessValues = GetListWithRandomFloats(movesCount);
            var expectedEnhancivenessValues = GetListWithRandomFloats(movesCount);
            var expectedHarmfulnessValues = GetListWithRandomFloats(movesCount);
            for (int i = 0; i < movesCount; i++)
            {
                var move = moves[i];
                destructivenessEvaluatorMock.Setup(evaluator => evaluator.EvaluateTrait(move)).Returns(expectedDestructivenessValues[i]);
                aggressivenessEvaluatorMock.Setup(evaluator => evaluator.EvaluateTrait(move)).Returns(expectedAggressivenessValues[i]);
                defensivenessEvaluatorMock.Setup(evaluator => evaluator.EvaluateTrait(move)).Returns(expectedDefensivenessValues[i]);
                enhancivenessEvaluatorMock.Setup(evaluator => evaluator.EvaluateTrait(move)).Returns(expectedEnhancivenessValues[i]);
                harmfulnessEvaluatorMock.Setup(evaluator => evaluator.EvaluateTrait(move)).Returns(expectedHarmfulnessValues[i]);
            }
            var evaluator = new MovesListTraitsEvaluator(
                destructivenessEvaluatorMock.Object,
                aggressivenessEvaluatorMock.Object,
                defensivenessEvaluatorMock.Object,
                enhancivenessEvaluatorMock.Object,
                harmfulnessEvaluatorMock.Object
            );
            
            var result = evaluator.EvaluateTraitsForMoves(moves);
            
            for (int i = 0; i < movesCount; i++)
            {
                var move = moves[i];
                var traitsValues = result[move];
                Assert.AreEqual(expectedDestructivenessValues[i], traitsValues.Destructiveness);
                Assert.AreEqual(expectedAggressivenessValues[i], traitsValues.Aggressiveness);
                Assert.AreEqual(expectedDefensivenessValues[i], traitsValues.Defensiveness);
                Assert.AreEqual(expectedEnhancivenessValues[i], traitsValues.Enhanciveness);
                Assert.AreEqual(expectedHarmfulnessValues[i], traitsValues.Harmfulness);
            }
        }

        private List<AbstractMove> GetMovesList(int movesCount)
        {
            var moves = new List<AbstractMove>();
            for (int i = 0; i < movesCount; i++)
            {
                moves.Add(GetMove());
            }
            return moves;
        }
        
        private List<float> GetListWithRandomFloats(int floatsCount)
        {
            var floats = new List<float>();
            for (int i = 0; i < floatsCount; i++)
            {
                floats.Add(new Random().Next(0, 100));
            }
            return floats;
        } 
    }
}