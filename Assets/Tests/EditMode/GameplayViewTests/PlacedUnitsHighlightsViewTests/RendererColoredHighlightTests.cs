﻿using NUnit.Framework;
using Src.GameplayView.Highlights;
using UnityEngine;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.PlacedUnitsHighlightsViewTests
{
    public class RendererColoredHighlightTests
    {
        private const string UnderlineRendererFieldName = "highlightRenderer";
        
        [Test]
        public void SetColor_ShouldSetColorToAllMaterials_InRenderer()
        {
            var expectedColor = Random.ColorHSV();
            var materialsCount = Random.Range(1, 10);
            var materials = GetMaterialsList(materialsCount);
            var renderer = GetRendererWithMultipleMaterials(materials);
            var underline = new GameObject().AddComponent<RendererColoredHighlight>();
            underline.SetPrivateField(UnderlineRendererFieldName, renderer);
            
            underline.SetColor(expectedColor);
            
            foreach (var material in renderer.materials)
            {
                Assert.AreEqual(expectedColor.r, material.color.r, 0.01f);
                Assert.AreEqual(expectedColor.g, material.color.g, 0.01f);
                Assert.AreEqual(expectedColor.b, material.color.b, 0.01f);
                Assert.AreEqual(expectedColor.a, material.color.a, 0.01f);
            }
        }
        
        [Test]
        public void Show_ShouldEnableRenderer()
        {
            var renderer = new GameObject().AddComponent<MeshRenderer>();
            renderer.enabled = false;
            var underline = new GameObject().AddComponent<RendererColoredHighlight>();
            underline.SetPrivateField(UnderlineRendererFieldName, renderer);
            
            underline.Show();
            
            Assert.IsTrue(renderer.enabled);
        }
        
        [Test]
        public void Hide_ShouldDisableRenderer()
        {
            var renderer = new GameObject().AddComponent<MeshRenderer>();
            renderer.enabled = true;
            var underline = new GameObject().AddComponent<RendererColoredHighlight>();
            underline.SetPrivateField(UnderlineRendererFieldName, renderer);
            
            underline.Hide();
            
            Assert.IsFalse(renderer.enabled);
        }
    }
}