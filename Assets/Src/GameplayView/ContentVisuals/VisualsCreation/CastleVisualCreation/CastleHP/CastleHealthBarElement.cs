using UnityEngine;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation.CastleHP
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CastleHealthBarElement : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        }

        public void ApplyHit(Sprite damagedSprite)
        {
            _spriteRenderer.sprite = damagedSprite;
        }
    }
}
