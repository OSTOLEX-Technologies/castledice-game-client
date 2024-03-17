using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Components.UI.ItemsGridFill
{
    public class ItemGridElementContent : GridElementContentBase
    {
        [SerializeField] private Image itemAvatar;
        [SerializeField] private TextMeshProUGUI itemNameText;

        public override void Fill(GridItemInfo content)
        {
            itemAvatar.sprite = content.itemSprite;
            itemNameText.SetText(content.descriptionTitle);
        }
    }
}
