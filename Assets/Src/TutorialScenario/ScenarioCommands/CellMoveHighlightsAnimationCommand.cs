using Src.Components;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class CellMoveHighlightsAnimationCommand : TutorialScenarioCommand
    {
        private enum AnimationActionType
        {
            PlayAnimation,
            StopAnimation
        }
        [SerializeField] private CellMovesHighlightsAnimation animation;
        [SerializeField] private AnimationActionType actionType;
        
        public override void Do()
        {
            switch (actionType)
            {
                case AnimationActionType.PlayAnimation:
                    animation.PlayAnimation();
                    break;
                case AnimationActionType.StopAnimation:
                    animation.StopAnimation();
                    break;
            }
        }

        public override void Undo()
        {
            switch (actionType)
            {
                case AnimationActionType.PlayAnimation:
                    animation.StopAnimation();
                    break;
                case AnimationActionType.StopAnimation:
                    animation.PlayAnimation();
                    break;
            }
        }
    }
}