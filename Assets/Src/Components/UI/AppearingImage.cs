using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Components.UI
{
    [RequireComponent(typeof(Image))]
    public class AppearingImage : MonoBehaviour
    {
        [SerializeField] private float transitionTime = 0.5f;
        [SerializeField] [Range(0, 1)] private float targetAlpha = 0.5f;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Appear()
        {
            StartCoroutine(ShowShadow());
        }
        
        private IEnumerator ShowShadow()
        {
            var time = 0f;
            var startColor = _image.color;
            var endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
            while (time < transitionTime)
            {
                time += Time.deltaTime;
                _image.color = Color.Lerp(startColor, endColor, time / transitionTime);
                yield return null;
            }
        }
        
        public void Disappear()
        {
            StartCoroutine(HideShadow());
        }
    
        private IEnumerator HideShadow()
        {
            var time = 0f;
            var startColor = _image.color;
            var endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
            while (time < transitionTime)
            {
                time += Time.deltaTime;
                _image.color = Color.Lerp(startColor, endColor, time / transitionTime);
                yield return null;
            }
        }
    }
    

}