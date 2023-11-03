using UnityEngine;

public class UnityCellHightlight : MonoBehaviour
{
    [SerializeField] private GameObject moveHighlight;
    [SerializeField] private GameObject attackHighlight;
    
    public void ShowMoveHighlight()
    {
        moveHighlight.SetActive(true);
    }
    
    public void HideMoveHighlight()
    {
        moveHighlight.SetActive(false);
    }
    
    public void ShowAttackHighlight()
    {
        attackHighlight.SetActive(true);
    }
    
    public void HideAttackHighlight()
    {
        attackHighlight.SetActive(false);
    }
    
    public void HideAllHighlights()
    {
        HideMoveHighlight();
        HideAttackHighlight();
    }
}
