using castledice_game_logic.Time;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public class PlayerTimerView : IPlayerTimerView
    {
        private readonly TimeView _timeView;
        private readonly Highlighter _highlighter;
        private readonly IPlayerTimer _playerTimer;
        
        public PlayerTimerView(TimeView timeView, Highlighter highlighter, IPlayerTimer playerTimer)
        {
            _timeView = timeView;
            _highlighter = highlighter;
            _playerTimer = playerTimer;
            _timeView.SetTime(_playerTimer.GetTimeLeft());
        }

        public void Update()
        {
            _timeView.SetTime(_playerTimer.GetTimeLeft());
        }

        public void Highlight()
        {
            _highlighter.Highlight();
        }

        public void Obscure()
        {
            _highlighter.Obscure();
        }
    }
}