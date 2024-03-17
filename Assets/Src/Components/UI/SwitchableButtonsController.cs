using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Src.Components.UI
{
    public class SwitchableButtonsController : MonoBehaviour
    {
        [SerializeField, InspectorName("Buttons List")] private SwitchableButton[] buttons;
        [SerializeField, InspectorName("Involved Buttons")] private List<bool> involvedButtons;
        [SerializeField, InspectorName("First Button Enabled Initially")] private bool bFirstEnabledInitially;

        public int ButtonsAmount => buttons.Length;
        
        public UnityAction<int> OptionSelected;
        public UnityAction OptionUnselected;

        private void OnValidate()
        {
            if (buttons is null) return;
            
            while (involvedButtons.Count > buttons.Length)
            {
                involvedButtons.RemoveAt(involvedButtons.Count - 1);
            }
        }

        private void Start()
        {
            var bFirstOptionActivated = false;
            for (var i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];
                button.ButtonClicked += OnButtonClicked;
                
                if (!involvedButtons[i])
                {
                    button.DisableButton();
                    continue;
                }

                if (bFirstEnabledInitially && !bFirstOptionActivated)
                {
                    bFirstOptionActivated = true;
                    button.SetupButton(true);
                }
                else
                {
                    button.SetupButton(false);
                }
            }
        }

        private void OnButtonClicked(SwitchableButton sender, bool pressed)
        {
            if (!pressed)
            {
                OptionUnselected?.Invoke();
                return;
            }

            var index = 0;
            for (var i = 0; i < buttons.Length; i++)
            {
                if (!buttons[i].Pressed) continue;
                
                if (buttons[i] == sender)
                {
                    index = i;
                    continue;
                }
                
                buttons[i].InvertPressState();
            }
            OptionSelected?.Invoke(index);
        }
    }
}
