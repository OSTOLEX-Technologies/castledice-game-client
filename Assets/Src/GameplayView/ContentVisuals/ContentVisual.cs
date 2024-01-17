using System.Collections.Generic;
using Src.Constants;
using UnityEngine;

namespace Src.GameplayView.ContentVisuals
{
    public abstract class ContentVisual : MonoBehaviour
    {
        public virtual float Transparency
        {
            get
            {
                if(transparencyAffectedRenderers.Count == 0)
                    throw new System.InvalidOperationException("TransparencyAffectedRenderers list is empty");
                return transparencyAffectedRenderers[0].material.color.a;
            }
            set
            {
                if (transparencyAffectedRenderers.Count == 0)
                    throw new System.InvalidOperationException("TransparencyAffectedRenderers list is empty");
                foreach (var rend in transparencyAffectedRenderers)
                {
                    var color = rend.material.color;
                    color.a = value;
                    rend.material.SetColor(ShadersPropertiesNames.Color, color);
                }
            }
        }

        [SerializeField] protected List<Renderer> transparencyAffectedRenderers = new();
    }
}