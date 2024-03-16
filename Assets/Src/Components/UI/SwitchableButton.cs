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

        public UnityAction<SwitchableButton, bool> ButtonClicked;
        
        private UnityAction _clickAction;
        
        private Button _button;
        public bool Pressed { get; private set; }
        private bool _bEnabled = true;

        public void InvertPressState()
        {
            _clickAction?.Invoke();
        }

        public void DisableButton()
        {
            _button.interactable = false;
            _button.image.sprite = inactiveSprite;
            _button.colors = ColorBlock.defaultColorBlock;
            _bEnabled = false;
        }
        
        public void SetupButton(bool initialPressedState)
        {
            Pressed = initialPressedState;
            UpdateSprite();
            UpdateTextColor();
            UpdateInteractivity();
            if (Pressed)
            {
                ButtonClicked?.Invoke(this, Pressed);
            }
        }
        
        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();

            if (_bEnabled) SetupButton(false);
            
            _clickAction += UpdateActiveState;
            _clickAction += UpdateSprite;
            _clickAction += UpdateTextColor;
            _clickAction += UpdateInteractivity;
            _clickAction += () => ButtonClicked?.Invoke(this, Pressed);
            
            _button.onClick.AddListener(_clickAction);
        }

        private void UpdateActiveState()
        {
            Pressed = !Pressed;
        }
        private void UpdateSprite()
        {
            var selectedSprite = Pressed ? activeSprite : inactiveSprite;
            _button.image.sprite = selectedSprite;
        }
        
        private void UpdateTextColor()
        {
            var selectedTextColor = Pressed ? activeTextColor : inactiveTextColor;
            text.color = selectedTextColor;
        }

        private void UpdateInteractivity()
        {
            _button.interactable = !Pressed;
        }
    }
}
