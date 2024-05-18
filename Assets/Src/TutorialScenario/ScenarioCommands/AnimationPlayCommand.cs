using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class AnimationPlayCommand : TutorialScenarioCommand
    {
        [SerializeField] private Animation animationComponent;
        
        public override void Do()
        {
            animationComponent.Play();
        }

        public override void Undo()
        {
            
        }
    }
}