using System.Collections.Generic;
using NUnit.Framework;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentViewsCreation.TreeViewCreation;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsCreationTests.TreeViewCreationTests
{
    public class TreeModelPrefabsConfigTests
    {
        [Test]
        public void GetTreeModelPrefabsList_ShouldReturnAppropriateList()
        {
            var expectedList = new List<GameObject>();
            var config = ScriptableObject.CreateInstance<TreeModelPrefabsConfig>();
            SetPrivateFieldValue(expectedList, config, "treeModelPrefabsList");
            
            var list = config.GetTreeModelPrefabsList();
            
            Assert.AreSame(expectedList, list);
        }
    }
}