using castledice_game_logic.GameObjects;
using Moq;
using NUnit.Framework;
using Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests.VisualsCreationTests.TreeVisualCreationTests
{
    public class CachingTreeVisualCreatorTests
    {
        [Test]
        public void GetTreeVisual_ShouldReturnSameTeeVisual_ForSameTree()
        {
            var tree = GetTree();
            var treeVisualCreatorMock = new Mock<ITreeVisualCreator>();
            treeVisualCreatorMock.Setup(x => x.GetTreeVisual(It.IsAny<Tree>())).Returns(GetTreeVisual);
            var cachingTreeVisualCreator = new CachingTreeVisualCreator(treeVisualCreatorMock.Object);
            
            var firstTreeVisual = cachingTreeVisualCreator.GetTreeVisual(tree);
            var secondTreeVisual = cachingTreeVisualCreator.GetTreeVisual(tree);
            
            Assert.AreSame(firstTreeVisual, secondTreeVisual);
        }
        
        [Test]
        public void GetTreeVisual_ShouldReturnDifferentTeeVisual_ForDifferentTrees()
        {
            var treeVisualCreatorMock = new Mock<ITreeVisualCreator>();
            treeVisualCreatorMock.Setup(x => x.GetTreeVisual(It.IsAny<Tree>())).Returns(GetTreeVisual);
            var cachingTreeVisualCreator = new CachingTreeVisualCreator(treeVisualCreatorMock.Object);
            
            var firstTreeVisual = cachingTreeVisualCreator.GetTreeVisual(GetTree());
            var secondTreeVisual = cachingTreeVisualCreator.GetTreeVisual(GetTree());
            
            Assert.AreNotSame(firstTreeVisual, secondTreeVisual);
        }
    }
}