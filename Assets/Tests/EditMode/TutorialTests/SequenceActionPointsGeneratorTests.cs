using System;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.General;
using Src.General.NumericSequences;
using Src.Tutorial;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.TutorialTests
{
    public class SequenceActionPointsGeneratorTests
    {
        [Test]
        public void GetActionPoints_ShouldReturnNumberFromSequence_ObtainedFromProvider()
        {
            var rnd = new Random();
            var expectedActionPoints = rnd.Next();
            var sequenceMock = new Mock<IIntSequence>();
            sequenceMock.Setup(x => x.Next()).Returns(expectedActionPoints);
            var sequenceProviderMock = new Mock<IPlayerIntSequenceProvider>();
            sequenceProviderMock.Setup(x => x.GetSequence(It.IsAny<Player>())).Returns(sequenceMock.Object);
            var generator = new SequenceActionPointsGenerator(sequenceProviderMock.Object);
            
            var actualActionPoints = generator.GetActionPoints(It.IsAny<Player>());
            
            Assert.AreEqual(expectedActionPoints, actualActionPoints);
        }

        [Test]
        public void GetActionPoints_ShouldGetSequenceFromProvider_WithGivenPlayer()
        {
            var player = GetPlayer();
            var sequenceProviderMock = new Mock<IPlayerIntSequenceProvider>();
            sequenceProviderMock.Setup(x => x.GetSequence(It.IsAny<Player>())).Returns(new Mock<IIntSequence>().Object);
            var generator = new SequenceActionPointsGenerator(sequenceProviderMock.Object);
            
            generator.GetActionPoints(player);
            
            sequenceProviderMock.Verify(x => x.GetSequence(player), Times.Once);
        }

        [Test]
        public void GetActionPoints_ShouldGetSequenceFromProvider_OncePerCall()
        {
            var sequenceProviderMock = new Mock<IPlayerIntSequenceProvider>();
            sequenceProviderMock.Setup(x => x.GetSequence(It.IsAny<Player>())).Returns(new Mock<IIntSequence>().Object);
            var generator = new SequenceActionPointsGenerator(sequenceProviderMock.Object);
            
            generator.GetActionPoints(It.IsAny<Player>());
            
            sequenceProviderMock.Verify(x => x.GetSequence(It.IsAny<Player>()), Times.Once);
        }
    }
}