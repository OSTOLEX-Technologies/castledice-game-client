using TMPro;
using UnityEngine;

namespace Src.GameplayView.Errors
{
    public class UnityErrorPopup : MonoBehaviour, IErrorPopup
    {
        [SerializeField] private TextMeshProUGUI messageTextMesh;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetMessage(string message)
        {
            messageTextMesh.text = message;
        }
    }
}