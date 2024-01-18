using Src.GameplayView.Updatables;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public interface IPlayerTimerView : IUpdatable
    {
        void Highlight();
        void Obscure();
    }
}