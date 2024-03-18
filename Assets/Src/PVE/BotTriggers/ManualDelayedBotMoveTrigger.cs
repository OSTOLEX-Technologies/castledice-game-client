using System;
using System.Threading.Tasks;

namespace Src.PVE.BotTriggers
{
    public class ManualDelayedBotMoveTrigger : IBotMoveTrigger
    {
        private readonly int _delayMilliseconds;
        
        public event Action ShouldMakeMove;
        
        public ManualDelayedBotMoveTrigger(int delayMilliseconds)
        {
            _delayMilliseconds = delayMilliseconds;
        }

        public async void TriggerBot()
        {
            await Task.Delay(_delayMilliseconds);
            ShouldMakeMove?.Invoke();
        }
    }
}