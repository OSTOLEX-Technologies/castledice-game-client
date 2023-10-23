using castledice_game_logic.GameObjects;
using Moq;
using NUnit.Framework;
using Src.GameplayView.CellsContent;
using Src.GameplayView.Grid;
using UnityEngine;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode
{
    public class CellsContentViewTests
    {
        private class ContentViewStub : ContentView
        {
            public bool StartViewCalled { get; private set; }
            public bool UpdateViewCalled { get; private set; }
            public bool DestroyViewCalled { get; private set; }
            
            public override void StartView()
            {
                StartViewCalled = true;
            }

            public override void UpdateView()
            {
                UpdateViewCalled = true;
            }

            public override void DestroyView()
            {
                DestroyViewCalled = true;
            }
        }
        
        [Test]
        public void AddViewForContent_ShouldThrowInvalidOperationException_IfViewForContentAlreadyExists()
        {
            var girdMock = new Mock<IGameObjectsGrid>();
            var contentViewProviderMock = new Mock<IContentViewProvider>();
            contentViewProviderMock.Setup(p => p.GetContentView(It.IsAny<Content>()))
                .Returns(GetContentView());
            var view = new CellsContentView(girdMock.Object, contentViewProviderMock.Object);
            var content = GetCellContent();
            
            view.AddViewForContent((0, 0), content);
            
            Assert.Throws<System.InvalidOperationException>(() => view.AddViewForContent((0, 0), content));
        }
        
        [Test]
        public void AddViewForContent_ShouldCallGetViewForContent_OnGivenContentViewProvider()
        {
            var girdMock = new Mock<IGameObjectsGrid>();
            var contentViewProviderMock = new Mock<IContentViewProvider>();
            contentViewProviderMock.Setup(p => p.GetContentView(It.IsAny<Content>()))
                .Returns(GetContentView());
            var view = new CellsContentView(girdMock.Object, contentViewProviderMock.Object);
            var content = GetCellContent();
            
            view.AddViewForContent((0, 0), content);
            
            contentViewProviderMock.Verify(provider => provider.GetContentView(content), Times.Once);
        }
        
        [Test]
        public void AddViewForContent_ShouldAddViewForContent_ToTheGivenGrid()
        {
            var girdMock = new Mock<IGameObjectsGrid>();
            var contentViewProviderMock = new Mock<IContentViewProvider>();
            var contentView = GetContentView();
            contentViewProviderMock.Setup(p => p.GetContentView(It.IsAny<Content>()))
                .Returns(contentView);
            var view = new CellsContentView(girdMock.Object, contentViewProviderMock.Object);
            var content = GetCellContent();
            var position = (0, 0);
            
            view.AddViewForContent(position, content);
            
            girdMock.Verify(grid => grid.AddChild(position, contentView.gameObject), Times.Once);
        }

        [Test]
        public void AddViewForContent_ShouldCallStartView_OnCreatedContentView()
        {
            var girdMock = new Mock<IGameObjectsGrid>();
            var contentViewProviderMock = new Mock<IContentViewProvider>();
            var contentView = GetContentView();
            contentViewProviderMock.Setup(p => p.GetContentView(It.IsAny<Content>()))
                .Returns(contentView);
            var view = new CellsContentView(girdMock.Object, contentViewProviderMock.Object);
            var content = GetCellContent();
            
            view.AddViewForContent((0, 0), content);
            
            Assert.True(contentView.StartViewCalled);
        }
        
        [Test]
        public void RemoveViewForContent_ShouldThrowInvalidOperationException_IfViewForContentDoesNotExist()
        {
            var girdMock = new Mock<IGameObjectsGrid>();
            var contentViewProviderMock = new Mock<IContentViewProvider>();
            var view = new CellsContentView(girdMock.Object, contentViewProviderMock.Object);
            var content = GetCellContent();
            
            Assert.Throws<System.InvalidOperationException>(() => view.RemoveViewForContent(content));
        }

        [Test]
        public void RemoveViewForContent_ShouldCallDestroyView_IfViewForContentExists()
        {
            var girdMock = new Mock<IGameObjectsGrid>();
            var contentViewProviderMock = new Mock<IContentViewProvider>();
            var contentView = GetContentView();
            contentViewProviderMock.Setup(p => p.GetContentView(It.IsAny<Content>()))
                .Returns(contentView);
            var view = new CellsContentView(girdMock.Object, contentViewProviderMock.Object);
            var content = GetCellContent();
            
            view.AddViewForContent((0, 0), content);
            view.RemoveViewForContent(content);
            
            Assert.True(contentView.DestroyViewCalled);
        }
        
        [Test]
        public void UpdateViewForContent_ShouldThrowInvalidOperationException_IfViewForContentDoesNotExist()
        {
            var girdMock = new Mock<IGameObjectsGrid>();
            var contentViewProviderMock = new Mock<IContentViewProvider>();
            var view = new CellsContentView(girdMock.Object, contentViewProviderMock.Object);
            var content = GetCellContent();
            
            Assert.Throws<System.InvalidOperationException>(() => view.UpdateViewForContent(content));
        }
        
        [Test]
        public void UpdateViewForContent_ShouldCallUpdateView_IfViewForContentExists()
        {
            var girdMock = new Mock<IGameObjectsGrid>();
            var contentViewProviderMock = new Mock<IContentViewProvider>();
            var contentView = GetContentView();
            contentViewProviderMock.Setup(p => p.GetContentView(It.IsAny<Content>()))
                .Returns(contentView);
            var view = new CellsContentView(girdMock.Object, contentViewProviderMock.Object);
            var content = GetCellContent();
            
            view.AddViewForContent((0, 0), content);
            view.UpdateViewForContent(content);
            
            Assert.True(contentView.UpdateViewCalled);
        }

        private ContentViewStub GetContentView()
        {
            var gameObject = new GameObject();
            return gameObject.AddComponent<ContentViewStub>();
        }
    }
}