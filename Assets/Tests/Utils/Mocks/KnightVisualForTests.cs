using Src.GameplayView.ContentVisuals;
using UnityEngine;

namespace Tests.Utils.Mocks
{
    public class KnightVisualForTests : KnightVisual
    {
        public Color Color { get; private set; }

        public override void SetColor(Color color)
        {
            Color = color;
        }
    }
}