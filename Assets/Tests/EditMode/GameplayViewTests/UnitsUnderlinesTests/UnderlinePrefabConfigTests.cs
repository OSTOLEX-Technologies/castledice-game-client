using NUnit.Framework;
using Src.GameplayView.UnitsUnderlines;
using Tests.Utils.Mocks;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.UnitsUnderlinesTests
{
    public class UnderlinePrefabConfigTests
    {
        [Test]
        public void GetUnderlinePrefab_ShouldReturnUnderlinePrefab()
        {
            var underlinePrefab = new GameObject().AddComponent<UnderlineForTests>();
            var config = ScriptableObject.CreateInstance<UnderlinePrefabConfig>();
            config.SetPrivateField("underlinePrefab", underlinePrefab);
            
            var prefab = config.GetUnderlinePrefab();
            
            Assert.AreSame(underlinePrefab, prefab);
        }
    }
}