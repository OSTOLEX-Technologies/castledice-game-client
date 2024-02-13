using Moq;
using static Tests.Utils.ObjectCreationUtility;
using NUnit.Framework;
using Src.GameplayView.ContentVisuals.VisualsCreation;
using Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation;
using Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation;
using Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests.VisualsCreationTests
{
    public class VisitorContentVisualCreatorTests
    {
        [Test]
        public void GetVisual_ShouldReturnVisualFromTreeVisualCreator_IfGivenContentIsTree()
        {
            var tree = GetTree();
            var expectedVisual = GetTreeVisual();
            var treeVisualCreatorMock = new Mock<ITreeVisualCreator>();
            treeVisualCreatorMock.Setup(creator => creator.GetTreeVisual(tree)).Returns(expectedVisual);
            var creator = new VisitorContentVisualCreatorBuilder
            {
                TreeVisualCreator = treeVisualCreatorMock.Object
            }.Build();
            
            var actualVisual = creator.GetVisual(tree);
            
            Assert.AreSame(expectedVisual, actualVisual);
        }
        
        [Test]
        public void GetVisual_ShouldReturnVisualFromCastleVisualCreator_IfGivenContentIsCastle()
        {
            var castle = GetCastle();
            var expectedVisual = GetCastleVisual();
            var castleVisualCreatorMock = new Mock<ICastleVisualCreator>();
            castleVisualCreatorMock.Setup(creator => creator.GetCastleVisual(castle)).Returns(expectedVisual);
            var creator = new VisitorContentVisualCreatorBuilder
            {
                CastleVisualCreator = castleVisualCreatorMock.Object
            }.Build();
            
            var actualVisual = creator.GetVisual(castle);
            
            Assert.AreSame(expectedVisual, actualVisual);
        }
        
        [Test]
        public void GetVisual_ShouldReturnVisualFromKnightVisualCreator_IfGivenContentIsKnight()
        {
            var knight = GetKnight();
            var expectedVisual = GetKnightVisual();
            var knightVisualCreatorMock = new Mock<IKnightVisualCreator>();
            knightVisualCreatorMock.Setup(creator => creator.GetKnightVisual(knight)).Returns(expectedVisual);
            var creator = new VisitorContentVisualCreatorBuilder
            {
                KnightVisualCreator = knightVisualCreatorMock.Object
            }.Build();
            
            var actualVisual = creator.GetVisual(knight);
            
            Assert.AreSame(expectedVisual, actualVisual);
        }

        private class VisitorContentVisualCreatorBuilder
        {
            public IKnightVisualCreator KnightVisualCreator { get; set; } = new Mock<IKnightVisualCreator>().Object;
            public ITreeVisualCreator TreeVisualCreator { get; set; } = new Mock<ITreeVisualCreator>().Object;
            public ICastleVisualCreator CastleVisualCreator { get; set; } = new Mock<ICastleVisualCreator>().Object;
            
            public VisitorContentVisualCreator Build()
            {
                return new VisitorContentVisualCreator(KnightVisualCreator, TreeVisualCreator, CastleVisualCreator);
            }
        }
    }
}