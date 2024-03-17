using UnityEngine;

namespace Src.Components.UI
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasSwitcher : MonoBehaviour
    {
        private Canvas _sender;

        public void SmartEnableCanvas(Canvas target)
        {
            target.sortingOrder = _sender.sortingOrder + 1;
            target.gameObject.SetActive(true);
        }

        private void Awake()
        {
            _sender = gameObject.GetComponent<Canvas>();
        }
    }
}
