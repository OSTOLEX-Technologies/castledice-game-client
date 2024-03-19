using System;
using System.Collections.Generic;

namespace Src.PVE.BotTriggers
{
    /// <summary>
    /// Sequentially reacts to triggers, switching to the next trigger after the current one is invoked.
    /// </summary>
    public class QueueBotMoveTrigger : IBotMoveTrigger
    {
        private readonly Queue<IBotMoveTrigger> _triggers;
        private IBotMoveTrigger _currentTrigger;
        
        public event Action ShouldMakeMove;
        
        public QueueBotMoveTrigger(Queue<IBotMoveTrigger> triggers)
        {
            _triggers = triggers;
            if (_triggers.Count <= 0) return;
            _currentTrigger = _triggers.Dequeue();
            _currentTrigger.ShouldMakeMove += InvokeAndSwitchToNextTrigger;
        }
        
        private void InvokeAndSwitchToNextTrigger()
        {
            if (_currentTrigger != null)
            {
                _currentTrigger.ShouldMakeMove -= InvokeAndSwitchToNextTrigger;
            }

            if (_triggers.Count <= 0) return;
            _currentTrigger = _triggers.Dequeue();
            _currentTrigger.ShouldMakeMove += InvokeAndSwitchToNextTrigger;
            ShouldMakeMove?.Invoke();
        }
    }
}