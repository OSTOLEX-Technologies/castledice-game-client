using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatableByHorizontalDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Rigidbody _toRotate;
        [SerializeField] private Vector3 _defaultAngularVelocity;
        [SerializeField] private float _dragMultiplier;
        [SerializeField] private float _speedNormalizationTime = 0.2f;
        private float _elapsedTime = 0f;

        private void Start()
        {
            var localAV = _toRotate.transform.TransformVector(_defaultAngularVelocity);
            _toRotate.angularVelocity = localAV;
        }

        public void OnDrag(PointerEventData eventData)
        {
            StopAllCoroutines();
            _elapsedTime = 0;
            float xSpeed = eventData.delta.x * Time.deltaTime * _dragMultiplier;
            Vector3 speed = new Vector3(0, 0, xSpeed);
            _toRotate.angularVelocity = _toRotate.transform.TransformVector(speed);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            StopAllCoroutines();
            _elapsedTime = 0;
            StartCoroutine(OnSpeedStabilize());
        }

        private IEnumerator OnSpeedStabilize()
        {
            Vector3 currentSpeed = _toRotate.angularVelocity;
            Vector3 neededSpeed = _toRotate.transform.TransformVector(_defaultAngularVelocity);
            while (_elapsedTime <= _speedNormalizationTime)
            {
                float percentage = _elapsedTime / _speedNormalizationTime;
                _toRotate.angularVelocity = Vector3.Lerp(currentSpeed, neededSpeed, percentage);

                _elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            _elapsedTime = 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StopAllCoroutines();
            _elapsedTime = 0;
            _toRotate.angularVelocity = _toRotate.transform.TransformVector(Vector3.zero);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopAllCoroutines();
            _elapsedTime = 0;
            StartCoroutine(OnSpeedStabilize());
        }
    }
