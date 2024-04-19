using System;
using castledice_game_logic.Time;

namespace Src.TimeManagement
{
    /// <summary>
    /// This is a timer that does not count down time.
    /// GetTimeLeft always returns TimeSpan.MaxValue.
    /// SetTimeLeft does nothing.
    /// Use it when you don't need to restrict players by time.
    /// </summary>
    public class InfinityPlayerTimer : IPlayerTimer
    {
        public void Start()
        {
        }

        public void Stop()
        {
        }

        public TimeSpan GetTimeLeft()
        {
            return TimeSpan.MaxValue;
        }

        public void SetTimeLeft(TimeSpan timeSpan)
        {
        }

        public event Action TimeIsUp;
    }
}