using UnityEngine;
using UnityEngine.UI;

namespace Src.Components.UI.ItemsGridFill
{
    public class CharacterGridElementContent : GridElementContentBase
    {
        [SerializeField] private Image itemAvatar;

        public override void Fill(GridItemInfo content)
        {
            itemAvatar.sprite = content.itemSprite;
        }
    }
}
