using UnityEngine;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public abstract class Highlighter : MonoBehaviour
    {
        public abstract void Highlight();
        public abstract void Obscure();
    }
}