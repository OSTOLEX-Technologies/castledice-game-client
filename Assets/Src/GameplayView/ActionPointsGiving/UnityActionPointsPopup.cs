using TMPro;
using UnityEngine;

namespace Src.GameplayView.ActionPointsGiving
{
    public class UnityActionPointsPopup : MonoBehaviour, IActionPointsPopup
    {
        [SerializeField] private TextMeshProUGUI numberTextMesh;
        [SerializeField] private TextMeshProUGUI labelTextMesh;
        
        public void SetAmount(int amount)
        {
            numberTextMesh.text = amount.ToString();
            labelTextMesh.text = amount == 1 ? "action point" : "action points";
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}