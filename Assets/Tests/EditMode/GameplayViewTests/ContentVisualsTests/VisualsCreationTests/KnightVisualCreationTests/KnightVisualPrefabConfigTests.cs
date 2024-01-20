using NUnit.Framework;
using Src.GameplayView.ContentVisuals;
using Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests.VisualsCreationTests.KnightVisualCreationTests
{
    public class KnightVisualPrefabConfigTests
    {
        private const string KnightVisualPrefabFieldName = "knightVisualPrefab";
        
        [Test]
        public void GetKnightVisualPrefab_ShouldReturnKnightVisualPrefab()
        {
            var config = ScriptableObject.CreateInstance<KnightVisualPrefabConfig>();
            var prefab = new GameObject().AddComponent<KnightVisual>();
            config.SetPrivateField(KnightVisualPrefabFieldName, prefab);
            
            var actualPrefab = config.GetKnightVisualPrefab();
            
            Assert.AreSame(prefab, actualPrefab);
        }
    }
}