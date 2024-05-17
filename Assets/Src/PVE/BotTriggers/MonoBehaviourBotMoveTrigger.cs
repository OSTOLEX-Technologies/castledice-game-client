using System;

namespace Src.PVE.BotTriggers
{
    public class MonoBehaviourBotMoveTrigger : IBotMoveTrigger
    {
        public event Action ShouldMakeMove;
        
        public void TriggerBot()
        {
            ShouldMakeMove?.Invoke();
        }
    }
}