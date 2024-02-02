using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.CellsContent.ContentViewsCreation.TreeViewCreation;
using UnityEngine;
using static Tests.Utils.ObjectCreationUtility;

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
        public void GetTreeView_ShouldReturnTreeView_WithModelFromProvider()
        {
            var expectedModel = new GameObject();
            var modelProviderMock = new Mock<ITreeModelProvider>();
            modelProviderMock.Setup(m => m.GetTreeModel()).Returns(expectedModel);
            var treeViewFactory = new TreeViewFactoryBuilder
            {
                ModelProvider = modelProviderMock.Object
            }.Build();
            
            var treeView = treeViewFactory.GetTreeView(GetTree());
            var fieldInfo = typeof(TreeView).GetField("Model", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var actualModel = fieldInfo.GetValue(treeView) as GameObject;
            
            Assert.AreSame(expectedModel, actualModel);
        }

        private class TreeViewFactoryBuilder
        {
            public ITreeModelProvider ModelProvider { get; set; }
            public TreeView Prefab { get; set; }
            public IInstantiator Instantiator { get; set; }

            public TreeViewFactoryBuilder()
            {
                var model = new GameObject();
                var prefab = new GameObject().AddComponent<TreeView>();
                var instantiatorMock = new Mock<IInstantiator>();
                instantiatorMock.Setup(i => i.Instantiate(prefab)).Returns(new GameObject().AddComponent<TreeView>());
                var modelProviderMock = new Mock<ITreeModelProvider>();
                modelProviderMock.Setup(m => m.GetTreeModel()).Returns(model);
                Instantiator = instantiatorMock.Object;
                Prefab = prefab;
                ModelProvider = modelProviderMock.Object;
            }
            
            public TreeViewFactory Build()
            {
                return new TreeViewFactory(ModelProvider, Prefab, Instantiator);
            }
            
        }
    }
}