using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Components.UI
{
    [RequireComponent(typeof(Image))]
    public class AppearingImage : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float targetAlpha = 0.5f;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void AppearForSeconds(float seconds)
        {
            StartCoroutine(Show(seconds));
        }
        
        
        private IEnumerator Show(float seconds)
        {
            var time = 0f;
            var startColor = _image.color;
            var endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
            while (time < seconds)
            {
                time += Time.deltaTime;
                _image.color = Color.Lerp(startColor, endColor, time / seconds);
                yield return null;
            }
        }
    }
}