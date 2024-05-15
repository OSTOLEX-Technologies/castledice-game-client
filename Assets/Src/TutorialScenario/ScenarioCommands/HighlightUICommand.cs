using Src.Components.UI;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class HighlightUICommand : TutorialScenarioCommand
    {
        [SerializeField] private UIElementsHighlighter highlighter;
        [SerializeField] private bool highlight;
        private bool _cachedHighlightState;

        public override void Do()
        {
            _cachedHighlightState = highlighter.Highlighted;
            if (highlight)
            {
                highlighter.HighlightElements();
            }
            else
            {
                highlighter.UnhighlightElements();
            }
        }

        public override void Undo()
        {
            if (_cachedHighlightState)
            {
                highlighter.HighlightElements();
            }
            else
            {
                highlighter.UnhighlightElements();
            }
        }
    }
}