using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Src.Components.UI
{
    [RequireComponent(typeof(Button))]
    public class SwitchableButton : MonoBehaviour
    {
        [SerializeField, InspectorName("Active Sprite")] private Sprite activeSprite;
        [SerializeField, InspectorName("Inactive Sprite")] private Sprite inactiveSprite;

        [SerializeField, InspectorName("Button Text")] private TextMeshProUGUI text;
        [SerializeField, InspectorName("Active Text Color")] private Color activeTextColor;
        [SerializeField, InspectorName("Inactive Text Color")] private Color inactiveTextColor;

        [SerializeField, InspectorName("Disable On Selection")] private bool bDisableOnSelection;

        private UnityAction _clickAction;
        
        private Button _button;
        private bool _bActive;

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
            UpdateActiveState();

            _clickAction += UpdateActiveState;
            _clickAction += UpdateSprite;
            _clickAction += UpdateTextColor;
            _clickAction += UpdateInteractivity;
            _button.onClick.AddListener(_clickAction);
        }

        private void UpdateActiveState()
        {
            _bActive = _button.interactable;
        }
        private void UpdateSprite()
        {
            var selectedSprite = _bActive ? activeSprite : inactiveSprite;
            _button.image.sprite = selectedSprite;
        }
        
        private void UpdateTextColor()
        {
            var selectedTextColor = _bActive ? activeTextColor : inactiveTextColor;
            text.color = selectedTextColor;
        }

        private void UpdateInteractivity()
        {
            _button.interactable = _bActive;
        }
    }
}
