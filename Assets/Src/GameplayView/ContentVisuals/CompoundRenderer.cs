using System;
using System.Collections.Generic;
using UnityEngine;

namespace Src.GameplayView.ContentVisuals
{
    [Serializable]
    public class CompoundRenderer
    {
        [SerializeField] private List<Renderer> renderers = new();

        public void SetColor(Color color)
        {
            foreach (var rend in renderers)
            {
                foreach (var material in rend.materials)
                {
                    material.color = color;
                }
            }
        }
        
        public void SetTransparency(float transparency)
        {
            foreach (var rend in renderers)
            {
                foreach (var material in rend.materials)
                {
                    var color = material.color;
                    color.a = transparency;
                    material.color = color;
                }
            }
        }
    }
}