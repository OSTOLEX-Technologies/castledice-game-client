using System.Collections.Generic;
using UnityEngine;

namespace Src.Components.UI
{
    public class ExploreScreenTabsSwitcher : MonoBehaviour
    {
        [SerializeField, InspectorName("Switchable Buttons Controller")]
        private SwitchableButtonsController switchableButtonsController;        
        [SerializeField, InspectorName("Referenced Widgets")]
        private List<RectTransform> widgets;

        private void OnValidate()
        {
            if (switchableButtonsController is null) return;
            
            while (widgets.Count > switchableButtonsController.ButtonsAmount)
            {
                widgets.RemoveAt(widgets.Count - 1);
            }
        }

        private void Awake()
        {
            switchableButtonsController.OptionSelected += OnOptionSelected;
            switchableButtonsController.OptionUnselected += OnOptionUnselected;
        }

        private void OnOptionSelected(int index)
        {
            for (var i = 0; i < widgets.Count; i++)
            {
                widgets[i].gameObject.SetActive(i == index);
            }
        }
        private void OnOptionUnselected()
        {
            foreach (var widget in widgets)
            {
                widget.gameObject.SetActive(false);
            }
        }
    }
}