using NUnit.Framework;
using Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation;
using UnityEngine;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests.VisualsCreationTests.CastleVisualCreationTests
{
    public class CastleVisualPrefabConfigTests
    {
        private const string PrefabFieldName = "castleVisualPrefab";
        
        [Test]
        public void GetCastleVisualPrefab_ShouldReturnPrefab_FromPrivateField()
        {
            var expectedPrefab = GetCastleVisual();
            var prefabConfig = ScriptableObject.CreateInstance<CastleVisualPrefabConfig>();
            prefabConfig.SetPrivateField(PrefabFieldName, expectedPrefab);
            
            var actualPrefab = prefabConfig.GetCastleVisualPrefab();
            
            Assert.AreSame(expectedPrefab, actualPrefab);
        }
    }
}