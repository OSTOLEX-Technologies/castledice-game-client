using UnityEngine;

namespace Src.GameplayView.Updatables
{
    public class UpdaterBehaviour : MonoBehaviour
    {
        private IUpdater _updater;

        public void Init(IUpdater updater)
        {
            _updater = updater;
        }
        
        private void Update()
        {
            _updater.Update();
        }
    }
}