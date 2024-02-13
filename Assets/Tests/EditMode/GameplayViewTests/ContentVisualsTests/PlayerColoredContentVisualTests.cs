using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.ContentVisuals;
using UnityEngine;
using static Tests.Utils.ObjectCreationUtility;
using Random = UnityEngine.Random;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests
{
    public class PlayerColoredContentVisualTests
    {
        private const string ColoringAffectedRenderersFieldName = "coloringAffectedRenderers";
        
        [Test]
        public void SetColor_ShouldSetColor_OnColoringAffectedCompoundRenderer()
        {
            var expectedColor = Random.ColorHSV();
            var renderers = GetRenderersList(Random.Range(1, 10));
            var compoundRenderer = GetCompoundRenderer(renderers);
            var playerColoredContentVisualMock = new Mock<PlayerColoredContentVisual>() { CallBase = true };
            var playerColoredContentVisual = playerColoredContentVisualMock.Object;
            playerColoredContentVisual.SetPrivateField(ColoringAffectedRenderersFieldName, compoundRenderer);
            
            playerColoredContentVisual.SetColor(expectedColor);
            
            foreach (var renderer in renderers)
            {
                foreach (var material in renderer.materials)
                {
                    var actualColor = material.color;
                    Assert.That(actualColor.r, Is.EqualTo(expectedColor.r).Within(0.01f));
                    Assert.That(actualColor.g, Is.EqualTo(expectedColor.g).Within(0.01f));
                    Assert.That(actualColor.b, Is.EqualTo(expectedColor.b).Within(0.01f));
                    Assert.That(actualColor.a, Is.EqualTo(expectedColor.a).Within(0.01f));
                }
            }
        }
    }
}