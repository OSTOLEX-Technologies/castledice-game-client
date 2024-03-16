using UnityEngine;
using UnityEngine.Events;

namespace Src.Components.UI
{
    public class SwitchableButtonsController : MonoBehaviour
    {
        [SerializeField, InspectorName("Buttons")] private SwitchableButton[] buttons;
        [SerializeField, InspectorName("First Button Enabled Initially")] private bool bFirstEnabledInitially;

        public UnityAction<int> OptionSelected;
        public UnityAction OptionUnselected;
        
        private void Start()
        {
            if (bFirstEnabledInitially)
            {
                buttons[0].SetupButton(true);
            }
            
            foreach (var button in buttons)
            {
                button.ButtonClicked += OnButtonClicked;
            }
        }

        private void OnButtonClicked(SwitchableButton sender, bool pressed)
        {
            if (!pressed)
            {
                OptionUnselected?.Invoke();
                return;
            }
            
            for (var i = 0; i < buttons.Length; i++)
            {
                if (!buttons[i].Pressed || buttons[i] == sender) continue;
                
                buttons[i].InvertPressState();
                OptionSelected?.Invoke(i);
                return;
            }
        }
    }
}
