using Src.Components;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class ToggleRaycastCommand : TutorialScenarioCommand
    {
        [SerializeField] private BlockableRaycasterHandler raycasterHandler;
        [SerializeField] private bool block;
        private bool _cachedBlock;
        
        public override void Do()
        {
            _cachedBlock = raycasterHandler.IsBlocked;
            if (block)
            {
                raycasterHandler.BlockRaycast();
            }
            else
            {
                raycasterHandler.UnblockRaycast();
            }
        }

        public override void Undo()
        {
            if (_cachedBlock)
            {
                raycasterHandler.UnblockRaycast();
            }
            else
            {
                raycasterHandler.BlockRaycast();
            }
        }
    }
}