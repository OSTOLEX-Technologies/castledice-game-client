using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.ContentVisuals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests
{
    public class ContentVisualTests
    {
        private const string TransparencyAffectedRenderersFieldName = "transparencyAffectedRenderers";
        
        [Test]
        public void TransparencyProperty_ShouldReturnAlphaValueOfColor_FromRenderersMaterials()
        {
            var expectedAlpha = Random.value;
            var material = GetMaterialWithColor(new Color(0, 0, 0, expectedAlpha));
            var renderer = GetRendererWithMaterial(material);
            var contentVisual = new Mock<ContentVisual>() { CallBase = true }.Object; //By setting CallBase to true, we can real implementation of Transparency property
            contentVisual.SetPrivateField(TransparencyAffectedRenderersFieldName, new List<Renderer> { renderer });
            
            var actualAlpha = contentVisual.Transparency;
            
            Assert.AreEqual(expectedAlpha, actualAlpha);
        }

        [Test]
        public void TransparencyProperty_ShouldSetAlphaValueForColors_FromRenderersMaterials()
        {
            var expectedAlpha = Random.value;
            var renderers = GetRenderersListWithMaterial(GetMaterialWithColor(new Color(0, 0, 0, 0)), Random.Range(1, 10));
            var contentVisual = new Mock<ContentVisual>() { CallBase = true }.Object; //By setting CallBase to true, we can real implementation of Transparency property
           contentVisual.SetPrivateField(TransparencyAffectedRenderersFieldName, renderers);

           contentVisual.Transparency = expectedAlpha;

           foreach (var renderer in renderers)
           {
                Assert.AreEqual(expectedAlpha, renderer.material.color.a);
           }
        }
        
        [Test]
        public void TransparencySet_ShouldThrowInvalidOperationException_IfRenderersListIsEmpty()
        {
            var contentVisual = new Mock<ContentVisual>() { CallBase = true }.Object; //By setting CallBase to true, we can real implementation of Transparency property
            contentVisual.SetPrivateField(TransparencyAffectedRenderersFieldName, new List<Renderer>());

            Assert.Throws<InvalidOperationException>(() => contentVisual.Transparency = Random.value);
        }
        
        [Test]
        public void TransparencyGet_ShouldThrowInvalidOperationException_IfRenderersListIsEmpty()
        {
            var contentVisual = new Mock<ContentVisual>() { CallBase = true }.Object; //By setting CallBase to true, we can real implementation of Transparency property
            contentVisual.SetPrivateField(TransparencyAffectedRenderersFieldName, new List<Renderer>());

            float value = 0;
            
            Assert.Throws<InvalidOperationException>(() =>  value = contentVisual.Transparency );
        }
        
        private static List<Renderer> GetRenderersListWithMaterial(Material material, int count)
        {
            var renderers = new List<Renderer>();
            for (var i = 0; i < count; i++)
            {
                renderers.Add(GetRendererWithMaterial(material));
            }
            return renderers;
        }
        
        private static Renderer GetRendererWithMaterial(Material material)
        {
            var gameObject = new GameObject();
            var renderer = gameObject.AddComponent<MeshRenderer>();
            renderer.material = material;
            return renderer;
        }
        
        private static Material GetMaterialWithColor(Color color)
        {
            var material = new Material(Shader.Find("Standard"))
            {
                color = color
            };
            return material;
        }
    }
}