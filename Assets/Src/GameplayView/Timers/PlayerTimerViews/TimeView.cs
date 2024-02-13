using System;
using UnityEngine;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public abstract class TimeView : MonoBehaviour
    {
        public abstract void SetTime(TimeSpan time);
    }
}