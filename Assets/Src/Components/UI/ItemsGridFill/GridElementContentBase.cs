using UnityEngine;
using UnityEngine.UI;

namespace Src.Components.UI.ItemsGridFill
{
    public abstract class GridElementContentBase : MonoBehaviour
    {
        [SerializeField] private Button itemClickButton;

        public Button ClickButton => itemClickButton;

        public abstract void Fill(GridItemInfo content);
    }
}
