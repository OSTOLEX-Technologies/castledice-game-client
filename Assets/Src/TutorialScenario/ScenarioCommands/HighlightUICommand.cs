using Src.Components.UI;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class HighlightUICommand : TutorialScenarioCommand
    {
        [SerializeField] private UIElementsHighlighter highlighter;
        [SerializeField] private float highlightTimeInSeconds;

        public override void Do()
        {
            highlighter.HighlightElementsForSeconds(highlightTimeInSeconds);
        }

        public override void Undo()
        {
            
        }
    }
}