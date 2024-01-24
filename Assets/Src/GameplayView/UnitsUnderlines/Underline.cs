using UnityEngine;

namespace Src.GameplayView.UnitsUnderlines
{
    public abstract class Underline : MonoBehaviour
    {
        public abstract void SetColor(Color color);
        public abstract void Show();
        public abstract void Hide();
    }
}