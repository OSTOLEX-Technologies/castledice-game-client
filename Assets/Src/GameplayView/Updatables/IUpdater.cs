namespace Src.GameplayView.Updatables
{
    public interface IUpdater : IUpdatable
    {
        void AddUpdatable(IUpdatable updatable);
        void RemoveUpdatable(IUpdatable updatable);
    }
}