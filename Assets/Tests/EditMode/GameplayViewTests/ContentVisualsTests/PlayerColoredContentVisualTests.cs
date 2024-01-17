using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.ContentVisuals;
using UnityEngine;
using static Tests.ObjectCreationUtility;
using Random = UnityEngine.Random;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests
{
    public class PlayerColoredContentVisualTests
    {
        private const string ColoringAffectedRenderersFieldName = "coloringAffectedRenderers";
        
        [Test]
        //By "Renderers" in this test we mean those renderers from "coloringAffectedRenderers" list
        public void ColorProperty_ShouldSetColorsOfRenderersMaterials_ToGivenColor()
        {
            var color = Random.ColorHSV();
            var renderers = GetRenderersListWithMaterial(GetMaterialWithColor(Random.ColorHSV()), Random.Range(1, 10));
            var contentVisual = new Mock<PlayerColoredContentVisual> { CallBase = true }.Object; //By setting CallBase to true, we can real implementation of Color property
            contentVisual.SetPrivateField(ColoringAffectedRenderersFieldName, renderers);
            
            contentVisual.Color = color;
            
            foreach (var renderer in renderers)
            {
                var actualColor = renderer.material.color;
                Assert.That(actualColor.r, Is.EqualTo(color.r).Within(0.01f));
                Assert.That(actualColor.g, Is.EqualTo(color.g).Within(0.01f));
                Assert.That(actualColor.b, Is.EqualTo(color.b).Within(0.01f));
                Assert.That(actualColor.a, Is.EqualTo(color.a).Within(0.01f));
            }
        }
        
        [Test]
        //By "Renderers" in this test we mean those renderers from "coloringAffectedRenderers" list
        public void ColorProperty_ShouldReturnColorOfFirstRendererMaterial()
        {
            var color = Random.ColorHSV();
            var material = GetMaterialWithColor(color);
            var renderer = GetRendererWithMaterial(material);
            var contentVisual = new Mock<PlayerColoredContentVisual> { CallBase = true }.Object; //By setting CallBase to true, we can real implementation of Color property
            contentVisual.SetPrivateField(ColoringAffectedRenderersFieldName, new List<Renderer> { renderer });
            
            var actualColor = contentVisual.Color;
            
            Assert.That(actualColor.r, Is.EqualTo(color.r).Within(0.01f));
            Assert.That(actualColor.g, Is.EqualTo(color.g).Within(0.01f));
            Assert.That(actualColor.b, Is.EqualTo(color.b).Within(0.01f));
            Assert.That(actualColor.a, Is.EqualTo(color.a).Within(0.01f));
        }
        
        [Test]
        public void ColorPropertyGet_ShouldThrowInvalidOperationException_IfRenderersListIsEmpty()
        {
            var contentVisual = new Mock<PlayerColoredContentVisual> { CallBase = true }.Object; //By setting CallBase to true, we can real implementation of Color property
            contentVisual.SetPrivateField(ColoringAffectedRenderersFieldName, new List<Renderer>());
            
            Assert.Throws<InvalidOperationException>(() => contentVisual.Color = Random.ColorHSV());
        }
        
        [Test]
        public void ColorPropertySet_ShouldThrowInvalidOperationException_IfRenderersListIsEmpty()
        {
            var contentVisual = new Mock<PlayerColoredContentVisual> { CallBase = true }.Object; //By setting CallBase to true, we can real implementation of Color property
            contentVisual.SetPrivateField(ColoringAffectedRenderersFieldName, new List<Renderer>());
            
            Assert.Throws<InvalidOperationException>(() => contentVisual.Color = Random.ColorHSV());
        }
    }
}