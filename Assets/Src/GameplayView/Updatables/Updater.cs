using System.Collections.Generic;

namespace Src.GameplayView.Updatables
{
    public class Updater : IUpdater
    {
        private readonly List<IUpdatable> _updatables = new();
        
        public void AddUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void RemoveUpdatable(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }

        public void Update()
        {
            for (int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].Update();
            }
        }
    }
}