using UnityEngine;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public class GameObjectHighlighter : Highlighter
    {
        [SerializeField] private GameObject highlight;
        
        public override void Highlight()
        {
            highlight.SetActive(true);
        }

        public override void Obscure()
        {
            highlight.SetActive(false);
        }
    }
}