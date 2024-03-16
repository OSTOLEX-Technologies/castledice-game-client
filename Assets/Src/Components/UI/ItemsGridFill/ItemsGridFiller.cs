using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Components.UI.ItemsGridFill
{
    public class ItemsGridFiller : MonoBehaviour
    {
        [SerializeField] private GridItemInfo[] items;
        
        [SerializeField] private RectTransform itemGridParent;
        [SerializeField] private GameObject itemGridElementPrefab;
        
        [SerializeField] private bool bEnablePreview;
        [SerializeField] private Image previewAvatar;
        [SerializeField] private TextMeshProUGUI descriptionTitleText;
        [SerializeField] private TextMeshProUGUI descriptionText;

        private void Start()
        {
            if (items.Length == 0) return;
            
            OnElementClicked(0);
            
            for (var i  = 0; i < items.Length; i++)
            {
                var element = Instantiate(itemGridElementPrefab, itemGridParent);
                var elementContent = element.GetComponent<GridElementContentBase>();
                
                elementContent.Fill(items[i]);

                if (!bEnablePreview) continue;
                var index = i;
                elementContent.ClickButton.onClick.AddListener( delegate { OnElementClicked(index); });
            }
        }

        private void OnElementClicked(int index)
        {
            var info = items[index];
            previewAvatar.sprite = info.itemSprite;
            descriptionTitleText.SetText(info.descriptionTitle);
            descriptionText.SetText(info.description);
        }
    }
}
