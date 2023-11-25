using Moq;
using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.CellsContent.ContentViewsCreation;
using Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation;
using Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation;
using Src.GameplayView.CellsContent.ContentViewsCreation.TreeViewCreation;
using UnityEngine;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsCreationTests
{
    public class ContentViewProviderTests
    {
        [Test]
        public void GetContentView_ShouldReturnViewFromGivenKnightViewFactory_IfGivenKnight()
        {
            var knight = GetKnight();
            var expectedView = new GameObject().AddComponent<KnightView>();
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
            var expectedView = new GameObject().AddComponent<CastleView>();
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
            var expectedView = new GameObject().AddComponent<TreeView>();
            var factoryMock = new Mock<ITreeViewFactory>();
            factoryMock.Setup(factory => factory.GetTreeView(tree)).Returns(expectedView);
            var provider = new ContentViewProvider(factoryMock.Object, new Mock<IKnightViewFactory>().Object, new Mock<ICastleViewFactory>().Object);
            
            var actualView = provider.GetContentView(tree);
            
            Assert.AreSame(expectedView, actualView);
        }
    }
}