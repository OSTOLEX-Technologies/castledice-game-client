using NUnit.Framework;
using Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation;
using UnityEngine;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests.VisualsCreationTests.TreeVisualCreationTests
{
    public class TreeVisualPrefabsConfigTests
    {
        private const string TreeVisualPrefabsListFieldName = "treeVisualPrefabs";
        
        [Test]
        public void GetTreeVisualPrefabsList_ShouldReturnTreeVisualPrefabsList()
        {
            var config = ScriptableObject.CreateInstance<TreeVisualPrefabsConfig>();
            var prefabsList = GetTreeVisualsList(10);
            config.SetPrivateField(TreeVisualPrefabsListFieldName, prefabsList);
            
            var actualPrefabsList = config.GetTreeVisualPrefabsList();
            
            Assert.AreSame(prefabsList, actualPrefabsList);
        }
    }
}