using Moq;
using NUnit.Framework;
using Src.GameplayView.CellsContent;
using Src.GameplayView.CellsContent.ContentCreation;
using Src.GameplayView.CellsContent.ContentCreation.CastlesCreation;
using Src.GameplayView.CellsContent.ContentCreation.KnightsCreation;
using Src.GameplayView.CellsContent.ContentCreation.TreesCreation;
using Src.GameplayView.CellsContent.ContentViews;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentCreationTests
{
    public class ContentViewProviderTests
    {
        [Test]
        public void GetContentView_ShouldReturnViewFromGivenKnightViewFactory_IfGivenKnight()
        {
            var knight = GetKnight();
            var expectedView = new KnightView();
            var factoryMock = new Mock<IKnightViewFactory>();
            factoryMock.Setup(factory => factory.GetKnightView(knight)).Returns(expectedView);
            var provider = new ContentViewProvider(new Mock<ITreeViewFactory>().Object, factoryMock.Object, new Mock<ICastleViewFactory>().Object);
            
            var actualView = provider.GetContentView(knight);
            
            Assert.AreSame(expectedView, actualView);
        }
        
        [Test]
        public void GetContentView_ShouldReturnViewFromGivenCastleViewFactory_IfGivenCastle()
        {
            var castle = GetCastle();
            var expectedView = new CastleView();
            var factoryMock = new Mock<ICastleViewFactory>();
            factoryMock.Setup(factory => factory.GetCastleView(castle)).Returns(expectedView);
            var provider = new ContentViewProvider(new Mock<ITreeViewFactory>().Object, new Mock<IKnightViewFactory>().Object, factoryMock.Object);
            
            var actualView = provider.GetContentView(castle);
            
            Assert.AreSame(expectedView, actualView);
        }
        
        [Test]
        public void GetContentView_ShouldReturnViewFromGivenTreeViewFactory_IfGivenTree()
        {
            var tree = GetTree();
            var expectedView = new TreeView();
            var factoryMock = new Mock<ITreeViewFactory>();
            factoryMock.Setup(factory => factory.GetTreeView(tree)).Returns(expectedView);
            var provider = new ContentViewProvider(factoryMock.Object, new Mock<IKnightViewFactory>().Object, new Mock<ICastleViewFactory>().Object);
            
            var actualView = provider.GetContentView(tree);
            
            Assert.AreSame(expectedView, actualView);
        }
    }
}