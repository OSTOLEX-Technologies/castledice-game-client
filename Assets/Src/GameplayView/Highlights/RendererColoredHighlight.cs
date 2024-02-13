using UnityEngine;

namespace Src.GameplayView.Highlights
{
    public class RendererColoredHighlight : ColoredHighlight
    {
        [SerializeField] private Renderer highlightRenderer;
        
        public override void SetColor(Color color)
        {
            foreach (var material in highlightRenderer.materials)
            {
                material.color = color;
            }
        }

        public override void Show()
        {
            highlightRenderer.enabled = true;
        }

        public override void Hide()
        {
            highlightRenderer.enabled = false;
        }
    }
}