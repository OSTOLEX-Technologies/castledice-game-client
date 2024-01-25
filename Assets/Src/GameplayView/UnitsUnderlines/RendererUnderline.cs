using UnityEngine;

namespace Src.GameplayView.UnitsUnderlines
{
    public class RendererUnderline : Underline
    {
        [SerializeField] private Renderer underlineRenderer;
        
        public override void SetColor(Color color)
        {
            foreach (var material in underlineRenderer.materials)
            {
                material.color = color;
            }
        }

        public override void Show()
        {
            underlineRenderer.enabled = true;
        }

        public override void Hide()
        {
            underlineRenderer.enabled = false;
        }
    }
}