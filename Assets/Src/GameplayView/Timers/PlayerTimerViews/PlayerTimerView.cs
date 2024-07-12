using System;
using castledice_game_logic.Time;
using TMPro;
using UnityEngine;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public class PlayerTimerView : IPlayerTimerView
    {
        private readonly TextMeshProUGUI _textMeshActive;
        private readonly TextMeshProUGUI _textMeshInactive;
        private readonly GameObject _backgroundActive;
        private readonly GameObject _backgroundInactive;
        private readonly GameObject _glow;
        private readonly IPlayerTimer _playerTimer;
        private readonly TimeSpan _glowTime;
        private bool _isGlowing;
        
        public PlayerTimerView(TextMeshProUGUI textMeshActive, TextMeshProUGUI textMeshInactive, GameObject backgroundActive, GameObject backgroundInactive, GameObject glow, IPlayerTimer playerTimer, TimeSpan glowTime)
        {
            _textMeshActive = textMeshActive;
            _textMeshInactive = textMeshInactive;
            _backgroundActive = backgroundActive;
            _backgroundInactive = backgroundInactive;
            _glow = glow;
            _playerTimer = playerTimer;
            _glowTime = glowTime;
        }

        public void Update()
        {
            var timeLeft = _playerTimer.GetTimeLeft();
            SetTime(timeLeft);
            if (timeLeft <= _glowTime)
            {
                _glow.SetActive(true);
                _isGlowing = true;
            }
            else if (_isGlowing)
            {
                _glow.SetActive(false);
                _isGlowing = false;
            }
        }

        private void SetTime(TimeSpan time)
        {
            _textMeshActive.text = time.ToString(@"mm\:ss");
            _textMeshInactive.text = time.ToString(@"mm\:ss");
        }

        public void Highlight()
        {
            _backgroundActive.SetActive(true);
            _backgroundInactive.SetActive(false);
            _textMeshActive.gameObject.SetActive(true);
            _textMeshInactive.gameObject.SetActive(false);
            if (_isGlowing)
            {
                _glow.SetActive(true);
            }
        }

        public void Obscure()
        {
            _backgroundActive.SetActive(false);
            _backgroundInactive.SetActive(true);
            _textMeshActive.gameObject.SetActive(false);
            _textMeshInactive.gameObject.SetActive(true);
            _glow.SetActive(false);
        }
    }
}