using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Src.Components.UI
{
    [RequireComponent(typeof(Image))]
    public class AppearingImage : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float startAlpha = 0f;
        [SerializeField] [Range(0, 1)] private float targetAlpha = 0.5f;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            Reset();
        }

        public void Reset()
        {
            var color = _image.color;
            var defaultColor = new Color(color.r, color.g, color.b, startAlpha);
            _image.color = defaultColor;
        }

        public void AppearInSeconds(float seconds, float delaySeconds)
        {
            Reset();
            StartCoroutine(Show(seconds, delaySeconds));
        }

        private IEnumerator Show(float seconds, float delaySeconds)
        {
            var time = 0f;
            var startColor = _image.color;
            var endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
            while (time < seconds/3)
            {
                time += Time.deltaTime;
                _image.color = Color.Lerp(startColor, endColor, time*3 / seconds);
                yield return null;
            }
            yield return new WaitForSeconds(delaySeconds);
            time = 0f;
            while (time < seconds/3)
            {
                time += Time.deltaTime;
                _image.color = Color.Lerp(endColor, startColor, time*3 / seconds);
                yield return null;
            }
        }
    }
}