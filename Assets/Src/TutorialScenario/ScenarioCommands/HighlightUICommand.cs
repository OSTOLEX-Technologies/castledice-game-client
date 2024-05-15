using Src.Components.UI;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class HighlightUICommand : ITutorialScenarioCommand
    {
        private readonly UIElementsHighlighter _highlighter;
        private readonly bool _highlight;
        private bool _cachedHighlightState;
        
        public HighlightUICommand(UIElementsHighlighter highlighter, bool highlight)
        {
            _highlighter = highlighter;
            _highlight = highlight;
        }
        
        public void Do()
        {
            _cachedHighlightState = _highlighter.Highlighted;
            if (_highlight)
            {
                _highlighter.HighlightElements();
            }
            else
            {
                _highlighter.UnhighlightElements();
            }
        }

        public void Undo()
        {
            if (_cachedHighlightState)
            {
                _highlighter.HighlightElements();
            }
            else
            {
                _highlighter.UnhighlightElements();
            }
        }
    }
}