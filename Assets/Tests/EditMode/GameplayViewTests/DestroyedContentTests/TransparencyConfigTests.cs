using NUnit.Framework;
using Src.GameplayView.DestroyedContent;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.DestroyedContentTests
{
    public class TransparencyConfigTests
    {
        [Test]
        public void GetTransparency_ShouldReturnValue_FromSerializedField()
        {
            var expectedTransparency = Random.value;
            var config = ScriptableObject.CreateInstance<TransparencyConfig>();
            config.SetPrivateField("transparency", expectedTransparency);
            
            var transparency = config.GetTransparency();
            
            Assert.AreEqual(expectedTransparency, transparency);
        }
    }
}