namespace Src.GameplayPresenter.ActionPointsGiving
{
    public interface IActionPointsGivingPresenter
    {
        void GiveActionPoints(int playerId, int amount);
    }
}