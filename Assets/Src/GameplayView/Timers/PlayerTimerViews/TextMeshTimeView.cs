using System;
using TMPro;
using UnityEngine;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public class TextMeshTimeView : TimeView
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        
        public override void SetTime(TimeSpan time)
        {
            textMesh.text = $"{time.Minutes:D2}:{time.Seconds:D2}";
        }
    }
}