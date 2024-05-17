using Src.Tutorial.ActionPointsGiving;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class ChangeActionPointsCommand : TutorialScenarioCommand 
    {
        private enum ActionType
        {
            Give,
            Take
        }
        
        [SerializeField] private int playerId;
        [SerializeField] private int actionPointsAmount;
        [SerializeField] private ActionType actionType;
        [SerializeField] private TutorialActionPointsChanger tutorialActionPointsChanger;
        private int _cachedActionPointsChange;
        
        public override void Do()
        {
            if (actionType == ActionType.Give)
            {
                _cachedActionPointsChange = actionPointsAmount;
                tutorialActionPointsChanger.GiveActionPointsToPlayer(playerId, actionPointsAmount);
            }
            else
            {
                _cachedActionPointsChange = -actionPointsAmount;
                tutorialActionPointsChanger.TakeActionPointsFromPlayer(playerId, actionPointsAmount);
            }
        }

        public override void Undo()
        {
            if (actionType == ActionType.Give)
            {
                tutorialActionPointsChanger.TakeActionPointsFromPlayer(playerId, _cachedActionPointsChange);
            }
            else
            {
                tutorialActionPointsChanger.GiveActionPointsToPlayer(playerId, -_cachedActionPointsChange);
            }
        }
    }
}