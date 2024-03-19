using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Components.UI
{
    public class TimeoutButtonEnabler : MonoBehaviour
    {
        [SerializeField] private Button buttonToEnable;
        [SerializeField, Range(0, 10)] private float timeout;

        private void OnEnable()
        {
            buttonToEnable.interactable = false;
            StartTimeout();
        }

        public async void StartTimeout()
        {
            await Task.Delay((int)(timeout * 1000));
            
            buttonToEnable.interactable = true;
        }
    }
}
