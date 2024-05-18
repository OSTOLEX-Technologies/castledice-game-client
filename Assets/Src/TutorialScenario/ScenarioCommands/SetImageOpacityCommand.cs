using UnityEngine;
using UnityEngine.UI;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class SetImageOpacityCommand : TutorialScenarioCommand
    {
        [SerializeField] private Image target;
        [SerializeField, Range(0f, 1f)] private float targetOpacity;
        private float _cachedOpacity;

        public override void Do()
        {
            _cachedOpacity = target.color.a;
            UpdateColorOpacity(targetOpacity);
        }

        public override void Undo()
        {
            UpdateColorOpacity(_cachedOpacity);
        }

        private void UpdateColorOpacity(float newOpacity)
        {
            var tempColor = target.color;
            target.color = new Color(tempColor.r, tempColor.g, tempColor.b, targetOpacity);
        }
    }
}