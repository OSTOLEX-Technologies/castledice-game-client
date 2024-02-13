using Src.GameplayView.Highlights;
using UnityEngine;

namespace Tests.Utils.Mocks
{
    public class ColoredHighlightForTests : ColoredHighlight
    {
        public Color Color { get; private set; }
        public bool IsShown { get; private set; }
        
        public override void SetColor(Color color)
        {
            Color = color;
        }

        public override void Show()
        {
            IsShown = true;    
        }

        public override void Hide()
        {
            IsShown = false;
        }
    }
}