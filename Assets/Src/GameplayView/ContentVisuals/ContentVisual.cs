using System.Collections.Generic;
using Src.Constants;
using UnityEngine;

namespace Src.GameplayView.ContentVisuals
{
    public abstract class ContentVisual : MonoBehaviour
    {
        [SerializeField] protected List<Renderer> transparencyAffectedRenderers = new();

        private bool RenderersListIsEmpty => transparencyAffectedRenderers.Count == 0;
        
        /// <summary>
        /// This property sets color alpha for all renderers in <see cref="transparencyAffectedRenderers"/> list
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public virtual float Transparency
        {
            get
            {
                if(RenderersListIsEmpty)
                    throw new System.InvalidOperationException("TransparencyAffectedRenderers list is empty");
                return transparencyAffectedRenderers[0].material.color.a;
            }
            set
            {
                if (RenderersListIsEmpty)
                    throw new System.InvalidOperationException("TransparencyAffectedRenderers list is empty");
                foreach (var rend in transparencyAffectedRenderers)
                {
                    var color = rend.material.color;
                    color.a = value;
                    rend.material.SetColor(ShadersPropertiesNames.Color, color);
                }
            }
        }

    }
}