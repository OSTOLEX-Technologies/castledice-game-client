using Src.GameplayPresenter.GameWrappers;
using Src.GameplayView.ActionPointsGiving;

namespace Src.GameplayPresenter.ActionPointsGiving
{
    public class ActionPointsGivingPresenter : IActionPointsGivingPresenter
    {
        private readonly IPlayerProvider _playerProvider;
        private readonly IActionPointsGiver _actionPointsGiver;
        private readonly IActionPointsGivingView _view;

        public ActionPointsGivingPresenter(IPlayerProvider playerProvider, IActionPointsGiver actionPointsGiver, IActionPointsGivingView view)
        {
            _playerProvider = playerProvider;
            _actionPointsGiver = actionPointsGiver;
            _view = view;
        }

        public void GiveActionPoints(int playerId, int amount)
        {
            throw new System.NotImplementedException();
        }
    }
}