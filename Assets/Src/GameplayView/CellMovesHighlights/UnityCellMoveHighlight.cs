using castledice_game_logic.MovesLogic;
using UnityEngine;

namespace Src.GameplayView.CellMovesHighlights
{
    public class UnityCellMoveHighlight : MonoBehaviour, ICellMoveHighlight
    {
        [SerializeField] private GameObject greenHighlight;
        [SerializeField] private GameObject orangeHighlight;
        
        public void ShowHighlight(MoveType moveType)
        {
            switch (moveType)
            {
                case MoveType.Place or MoveType.Upgrade:
                    greenHighlight.SetActive(true);
                    break;
                case MoveType.Remove or MoveType.Replace or MoveType.Capture:
                    orangeHighlight.SetActive(true);
                    break;
            }
        }

        public void HideHighlight(MoveType moveType)
        {
            switch (moveType)
            {
                case MoveType.Place or MoveType.Upgrade:
                    greenHighlight.SetActive(false);
                    break;
                case MoveType.Remove or MoveType.Replace or MoveType.Capture:
                    orangeHighlight.SetActive(false);
                    break;
            }
        }

        public void HideAllHighlights()
        {
            greenHighlight.SetActive(false);
            orangeHighlight.SetActive(false);
        }
    }
}