using UnityEngine;

namespace Src.GameplayView.ContentVisuals
{
    public abstract class ContentVisual : MonoBehaviour
    {
        [SerializeField] protected CompoundRenderer transparencyAffectedRenderers = new();

        public virtual void SetTransparency(float transparency)
        {
            transparencyAffectedRenderers.SetTransparency(transparency);
        }

    }
}