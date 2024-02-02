using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentViews;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsTests
{
    public class TreeViewTests
    {
        [Test]
        public void Init_ShouldSetGivenModelAsChildObjectWithZeroLocalPosition()
        {
            var model = new GameObject();
            model.transform.position = Random.insideUnitSphere;
            var treeView = new GameObject().AddComponent<TreeView>();
            treeView.Init(GetTree(), model);
            
            Assert.AreSame(treeView.transform, model.transform.parent);
            Assert.AreEqual(Vector3.zero, model.transform.localPosition);
        }
        
        [Test]
        public void ContentProperty_ShouldReturnTree_GivenInInit()
        {
            var treeView = new GameObject().AddComponent<TreeView>();
            var tree = GetTree();
            treeView.Init(tree, new GameObject());
            
            Assert.AreSame(tree, treeView.Content);
        }
    }
}