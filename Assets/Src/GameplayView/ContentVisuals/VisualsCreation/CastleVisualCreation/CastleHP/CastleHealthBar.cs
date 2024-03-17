using System.Collections.Generic;
using UnityEngine;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation.CastleHP
{
    public class CastleHealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject healthBarElementPrefab;
        [SerializeField] private Vector2 elementSpacing;
        [SerializeField] private Sprite damagedSprite;
        
        private readonly List<CastleHealthBarElement> _healthBarElements = new();
        private int _nextHealthElementToBreak = 0;
        private bool HasHeathLeft => _nextHealthElementToBreak < _healthBarElements.Count;

        public void Init(int healthInitialAmount)
        {
            for (var i = 0; i < healthInitialAmount; i++)
            {
                var element = Instantiate(healthBarElementPrefab, gameObject.transform);
                _healthBarElements.Add(element.transform.GetComponent<CastleHealthBarElement>());
                _healthBarElements[i].transform.localPosition = elementSpacing * i;
            }
        }
        
        public void ApplyHit(object sender, int damage)
        {
            if (_healthBarElements.Count == 0) return;

            var damageLeft = damage;
            while(HasHeathLeft && damageLeft > 0)
            {
                _healthBarElements[_nextHealthElementToBreak++].ApplyHit(damagedSprite);
                damageLeft--;
            }
        }
    }
}