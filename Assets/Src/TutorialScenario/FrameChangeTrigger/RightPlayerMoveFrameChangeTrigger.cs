using castledice_game_logic.MovesLogic;
using Src.Tutorial;

namespace Src.TutorialScenario.FrameChangeTrigger
{
    public class RightPlayerMoveFrameChangeTrigger : FrameChangeTriggerBase
    {
        private TutorialMovesPresenter _movesPresenter;
        
        public void Init(TutorialMovesPresenter movesPresenter)
        {
            _movesPresenter = movesPresenter;
            _movesPresenter.RightMovePicked += OnRightMovePicked;
        }

        private void OnRightMovePicked(object sender, AbstractMove e)
        {
            RequestNextFrame();
        }
    }
}