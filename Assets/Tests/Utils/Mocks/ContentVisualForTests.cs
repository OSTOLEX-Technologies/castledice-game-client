using Src.GameplayView.ContentVisuals;
using UnityEditor.UIElements;

namespace Tests.Utils.Mocks
{
    public class ContentVisualForTests : ContentVisual
    {
        public float Transparency { get; private set; }

        public override void SetTransparency(float transparency)
        {
            Transparency = transparency;
        }
    }
}