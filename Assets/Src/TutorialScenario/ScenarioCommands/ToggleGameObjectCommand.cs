using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class ToggleGameObjectCommand : ITutorialScenarioCommand
    {
        private readonly GameObject _target;
        private readonly bool _enable;
        private bool _cachedEnableState;
        
        public ToggleGameObjectCommand(GameObject target, bool enable)
        {
            _target = target;
            _enable = enable;
        }
        
        public void Do()
        {
            _cachedEnableState = _target.activeSelf;
            _target.SetActive(_enable);
        }

        public void Undo()
        {
            _target.SetActive(_cachedEnableState);
        }
    }
}