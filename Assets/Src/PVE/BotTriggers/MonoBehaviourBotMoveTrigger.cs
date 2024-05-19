using System;
using UnityEngine;

namespace Src.PVE.BotTriggers
{
    public class MonoBehaviourBotMoveTrigger : MonoBehaviour, IBotMoveTrigger
    {
        public event Action ShouldMakeMove;
        
        public void TriggerBot()
        {
            ShouldMakeMove?.Invoke();
        }
    }
}