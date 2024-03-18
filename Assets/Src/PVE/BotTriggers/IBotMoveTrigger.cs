using System;

namespace Src.PVE.BotTriggers
{
    public interface IBotMoveTrigger
    {
        public event Action ShouldMakeMove;
    }
}