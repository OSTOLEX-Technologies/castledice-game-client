using Moq;
using NUnit.Framework;
using Src.GameplayView.ContentVisuals;
using Random = UnityEngine.Random;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests
{
    public class ContentVisualTests
    {
        [Test]
        public void SetTransparency_ShouldSetTransparency_OnTransparencyAffectedCompoundRenderer()
        {
            var expectedTransparency = Random.value;
            var renderers = GetRenderersList(Random.Range(1, 10));
            var compoundRenderer = GetCompoundRenderer(renderers);
            var contentVisualMock = new Mock<ContentVisual>() { CallBase = true };
            var contentVisual = contentVisualMock.Object;
            contentVisual.SetPrivateField("transparencyAffectedRenderers", compoundRenderer);
            
            contentVisual.SetTransparency(expectedTransparency);
            
            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    var actualTransparency = material.color.a;
                    Assert.That(actualTransparency, Is.EqualTo(expectedTransparency).Within(0.01f));
                }
            }
        }
    }
}