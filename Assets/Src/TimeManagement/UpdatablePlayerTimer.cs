using System;
using castledice_game_logic.Time;
using JetBrains.Annotations;
using Src.GameplayView.Updatables;

namespace Src.TimeManagement
{
    public class UpdatablePlayerTimer : IPlayerTimer, IUpdatable
    {
        private readonly ITimeDeltaProvider _timeDeltaProvider;
        private TimeSpan _timeLeft;
        private bool _isRunning;
        
        public UpdatablePlayerTimer(TimeSpan timeSpan, ITimeDeltaProvider timeDeltaProvider)
        {
            _timeDeltaProvider = timeDeltaProvider;
            _timeLeft = timeSpan;
        }
        
        public void Start()
        {
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public TimeSpan GetTimeLeft()
        {
            return _timeLeft;
        }

        public void SetTimeLeft(TimeSpan timeSpan)
        {
            _timeLeft = timeSpan;
        }

        [CanBeNull] public event Action TimeIsUp;
        public void Update()
        {
            if (!_isRunning) return;
            
            var deltaTimeSeconds = _timeDeltaProvider.GetDeltaTime();
            _timeLeft -= TimeSpan.FromSeconds(deltaTimeSeconds);
            if (_timeLeft > TimeSpan.Zero) return;
            _isRunning = false;
            TimeIsUp?.Invoke();
        }
    }
}