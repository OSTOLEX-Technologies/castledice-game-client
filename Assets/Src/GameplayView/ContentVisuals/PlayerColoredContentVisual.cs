using System;
using System.Collections.Generic;
using Src.Constants;
using UnityEngine;

namespace Src.GameplayView.ContentVisuals
{
    public abstract class PlayerColoredContentVisual : ContentVisual
    {
        [SerializeField] protected List<Renderer> coloringAffectedRenderers = new();
        
        private bool RenderersListIsEmpty => coloringAffectedRenderers.Count == 0;

        /// <summary>
        /// This property sets color for all renderers in <see cref="coloringAffectedRenderers"/> list
        /// </summary>
        public virtual Color Color
        {
            get
            {
                if (RenderersListIsEmpty)
                {
                    throw new InvalidOperationException("ColoringAffectedRenderers list is empty");
                }
                return coloringAffectedRenderers[0].material.color;
            }
            set
            {
                if (RenderersListIsEmpty)
                {
                    throw new InvalidOperationException("ColoringAffectedRenderers list is empty");
                }
                foreach (var rend in coloringAffectedRenderers)
                {
                    rend.material.SetColor(ShadersPropertiesNames.Color, value);
                }
            }
        }
    }
}