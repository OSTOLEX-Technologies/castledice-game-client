using UnityEngine;

namespace Src.GameplayView.Updatables
{
    public class FixedUpdaterBehaviour : MonoBehaviour
    {
        private IUpdater _updater;
        
        public void Init(IUpdater updater)
        {
            _updater = updater;
        }
        
        public void FixedUpdate()
        {
            _updater.Update();
        }
    }
}