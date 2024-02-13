using UnityEngine;

namespace Src.GameplayView.Highlights
{
    public abstract class ColoredHighlight : MonoBehaviour
    {
        public abstract void SetColor(Color color);
        public abstract void Show();
        public abstract void Hide();
    }
}