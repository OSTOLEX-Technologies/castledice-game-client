using System;
using System.Collections.Generic;
using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.ContentVisuals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tests.EditMode.GameplayViewTests.ContentVisualsTests
{
    public class CompoundRendererTests
    {
        [Test]
        public void SetColor_ShouldSetGivenColor_ToAllRenderersMaterials()
        {
            var renderersList = GetRenderersWithRandomMaterials(Random.Range(1, 10), 10);
            var compoundRenderer = GetCompoundRenderer(renderersList);
            var color = Random.ColorHSV();
            
            compoundRenderer.SetColor(color);
            
            foreach (var renderer in renderersList)
            {
                foreach (var material in renderer.materials)
                {
                    var actualColor = material.color;
                    Assert.That(actualColor.r, Is.EqualTo(color.r).Within(0.01f));
                    Assert.That(actualColor.g, Is.EqualTo(color.g).Within(0.01f));
                    Assert.That(actualColor.b, Is.EqualTo(color.b).Within(0.01f));
                    Assert.That(actualColor.a, Is.EqualTo(color.a).Within(0.01f));
                }
            }
        }
        
        [Test]
        public void SetTransparency_ShouldSetGivenTransparency_ToAllRenderersMaterials()
        {
            var renderersList = GetRenderersWithRandomMaterials(Random.Range(1, 10), 10);
            var compoundRenderer = GetCompoundRenderer(renderersList);
            var transparency = Random.value;
            
            compoundRenderer.SetTransparency(transparency);
            
            foreach (var renderer in renderersList)
            {
                foreach (var material in renderer.materials)
                {
                    var actualTransparency = material.color.a;
                    Assert.That(actualTransparency, Is.EqualTo(transparency).Within(0.01f));
                }
            }
        }
        
        [Test]
        public void SetTransparency_ShouldNotChangeRGBValues_OfRenderersMaterials()
        {
            var renderersList = GetRenderersWithRandomMaterials(Random.Range(1, 10), 10);
            var oldColors = new List<List<Color>>();
            foreach (var renderer in renderersList)
            {
                var materials = renderer.materials;
                var colors = new List<Color>();
                foreach (var material in materials)
                {
                    colors.Add(material.color);
                }
                oldColors.Add(colors);
            }
            var compoundRenderer = GetCompoundRenderer(renderersList);
            var transparency = Random.value;
            
            compoundRenderer.SetTransparency(transparency);
            
            for (var i = 0; i < renderersList.Count; i++)
            {
                var renderer = renderersList[i];
                var materials = renderer.materials;
                var colors = oldColors[i];
                for (var j = 0; j < materials.Length; j++)
                {
                    var material = materials[j];
                    var color = colors[j];
                    var actualColor = material.color;
                    Assert.That(actualColor.r, Is.EqualTo(color.r).Within(0.01f));
                    Assert.That(actualColor.g, Is.EqualTo(color.g).Within(0.01f));
                    Assert.That(actualColor.b, Is.EqualTo(color.b).Within(0.01f));
                }
            }
        }
        
        private static List<Renderer> GetRenderersWithRandomMaterials(int renderersCount, int maxMaterialsCount)
        {
            var renderersList = GetRenderersList(renderersCount);
            foreach (var renderer in renderersList)
            {
                renderer.materials = GetMaterialsList(Random.Range(1, maxMaterialsCount)).ToArray();
            }

            return renderersList;
        }
    }
}