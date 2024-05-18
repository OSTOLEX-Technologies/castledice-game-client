using Src.Components.UI;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class HighlightUICommand : TutorialScenarioCommand
    {
        [SerializeField] private UIElementsHighlighter highlighter;
        [SerializeField] private float highlightTimeInSeconds;
        [SerializeField] private float disappearDelayInSeconds;

        public override void Do()
        {
            highlighter.HighlightElementsForSeconds(highlightTimeInSeconds, disappearDelayInSeconds);
        }

        public override void Undo()
        {
            
        }
    }
}