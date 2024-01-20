using UnityEngine;

namespace Src.GameplayView.ContentVisuals
{
    public abstract class PlayerColoredContentVisual : ContentVisual
    {
        [SerializeField] protected CompoundRenderer coloringAffectedRenderers = new();
        

        public virtual void SetColor(Color color)
        {
            coloringAffectedRenderers.SetColor(color);
        }
    }
}