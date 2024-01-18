using NUnit.Framework;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentViews;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsTests
{
    public class TreeViewTests
    {
        [Test]
        public void Init_ShouldSetGivenVisualAsChildObjectWithZeroLocalPosition()
        {
            var visual = GetTreeVisual();
            visual.transform.position = Random.insideUnitSphere;
            var treeView = new GameObject().AddComponent<TreeView>();
            treeView.Init(GetTree(), visual);
            
            Assert.AreSame(treeView.transform, visual.transform.parent);
            Assert.AreEqual(Vector3.zero, visual.transform.localPosition);
        }
        
        [Test]
        public void ContentProperty_ShouldReturnTree_GivenInInit()
        {
            var treeView = new GameObject().AddComponent<TreeView>();
            var tree = GetTree();
            treeView.Init(tree, GetTreeVisual());
            
            Assert.AreSame(tree, treeView.Content);
        }
    }
}