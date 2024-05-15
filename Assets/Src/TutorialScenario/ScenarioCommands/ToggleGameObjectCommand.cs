using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class ToggleGameObjectCommand : TutorialScenarioCommand
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool enable;
        private bool _cachedEnableState;

        public override void Do()
        {
            _cachedEnableState = target.gameObject.activeSelf;
            target.gameObject.SetActive(enable);
        }

        public override void Undo()
        {
            target.gameObject.SetActive(_cachedEnableState);
        }
    }
}