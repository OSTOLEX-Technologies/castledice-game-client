using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentCreation.TreesCreation;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentCreationTests.TreesCreationTests
{
    public class UnityTreeModelConfigTests
    {
        [Test]
        public void GetTreeModel_ShouldReturnTreeModel()
        {
            var expectedModel = new GameObject();
            var config = ScriptableObject.CreateInstance<UnityTreeModelConfig>();
            var fieldInfo = typeof(UnityTreeModelConfig).GetField("treeModel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(config, expectedModel);
            
            var actualModel = config.GetTreeModel();
            
            Assert.AreSame(expectedModel, actualModel);
        }
    }
}