using System;

namespace Src.GameplayPresenter.Timers
{
    public interface ITimersPresenter
    {/// <summary>
     /// 
     /// </summary>
     /// <param name="playerId"></param>
     /// <param name="timeLeft"></param>
     /// <param name="switchTo">If true, than timer should be started. If false, than timer should be stopped.</param>
        void SwitchTimerForPlayer(int playerId, TimeSpan timeLeft, bool switchTo);
    }
}