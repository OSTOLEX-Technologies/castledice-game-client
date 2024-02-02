using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsCreationTests.KnightViewCreationTests
{
    public class KnightModelPrefabConfigTests
    {
        [Test]
        [TestCase("redKnightModelPrefab", PlayerColor.Red)]
        [TestCase("blueKnightModelPrefab", PlayerColor.Blue)]
        public void GetKnightModelPrefab_ShouldReturnAppropriatePrefab(string prefabFieldName, PlayerColor color)
        {
            var expectedPrefab = new GameObject();
            var config = ScriptableObject.CreateInstance<KnightModelPrefabConfig>();
            SetPrivateFieldValue(expectedPrefab, config, prefabFieldName);
            
            var prefab = config.GetKnightModelPrefab(color);
            
            Assert.AreSame(expectedPrefab, prefab);
        }
    }
}