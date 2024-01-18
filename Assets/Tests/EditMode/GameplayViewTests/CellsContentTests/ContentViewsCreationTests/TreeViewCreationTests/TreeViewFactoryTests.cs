using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.CellsContent.ContentViewsCreation.TreeViewCreation;
using Src.GameplayView.ContentVisuals;
using Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation;
using UnityEngine;
using Tree = castledice_game_logic.GameObjects.Tree;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsCreationTests.TreeViewCreationTests
{
    public class TreeViewFactoryTests
    {
        [Test]
        public void GetTreeView_ShouldReturnInstantiatedTreeViewPrefab()
        {
            var prefab = new GameObject().AddComponent<TreeView>();
            var instantiatedTreeView = new GameObject().AddComponent<TreeView>();
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(i => i.Instantiate(prefab)).Returns(instantiatedTreeView);
            var treeViewFactory = new TreeViewFactoryBuilder
            {
                Prefab = prefab,
                Instantiator = instantiatorMock.Object
            }.Build();
            
            var treeView = treeViewFactory.GetTreeView(GetTree());
            
            Assert.AreSame(instantiatedTreeView, treeView);
        }

        [Test]
        public void GetTreeView_ShouldReturnTreeView_WithGivenTreeAsContent()
        {
            var expectedTree = GetTree();
            var treeViewFactory = new TreeViewFactoryBuilder().Build();
            
            var treeView = treeViewFactory.GetTreeView(expectedTree);
            
            Assert.AreSame(expectedTree, treeView.Content);
        }

        [Test]
        public void GetTreeView_ShouldReturnTreeView_WithVisualFromCreator()
        {
            var expectedVisual = GetTreeVisual();
            var visualCreatorMock = new Mock<ITreeVisualCreator>();
            var tree = GetTree();
            visualCreatorMock.Setup(m => m.GetTreeVisual(tree)).Returns(expectedVisual);
            var treeViewFactory = new TreeViewFactoryBuilder
            {
                VisualCreator = visualCreatorMock.Object
            }.Build();
            
            var treeView = treeViewFactory.GetTreeView(tree);
            var fieldInfo = typeof(TreeView).GetField("_visual", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var actualVisual = fieldInfo.GetValue(treeView) as TreeVisual;
            
            Assert.AreSame(expectedVisual, actualVisual);
        }

        private class TreeViewFactoryBuilder
        {
            public ITreeVisualCreator VisualCreator { get; set; }
            public TreeView Prefab { get; set; }
            public IInstantiator Instantiator { get; set; }

            public TreeViewFactoryBuilder()
            {
                var visual = GetTreeVisual();
                var prefab = new GameObject().AddComponent<TreeView>();
                var instantiatorMock = new Mock<IInstantiator>();
                instantiatorMock.Setup(i => i.Instantiate(prefab)).Returns(new GameObject().AddComponent<TreeView>());
                var visualCreatorMock = new Mock<ITreeVisualCreator>();
                visualCreatorMock.Setup(m => m.GetTreeVisual(It.IsAny<Tree>())).Returns(visual);
                Instantiator = instantiatorMock.Object;
                Prefab = prefab;
                VisualCreator = visualCreatorMock.Object;
            }
            
            public TreeViewFactory Build()
            {
                return new TreeViewFactory(VisualCreator, Prefab, Instantiator);
            }
        }
    }
}