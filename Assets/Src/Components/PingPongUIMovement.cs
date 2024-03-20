using System;
using UnityEngine;

public class PingPongUIMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private RectTransform _from;
    [SerializeField] private RectTransform _to;
    [SerializeField] private float _movementDuration;
    private float _elapsedTime;
    private bool _forward;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _movementDuration)
        {
            _elapsedTime = 0;
            _forward = !_forward;
        }

        var t = _elapsedTime / _movementDuration;
        if (!_forward)
        {
            t = 1 - t;
        }

        var position = Vector3.Lerp(_from.position, _to.position, _movementCurve.Evaluate(t));
        transform.position = position;
    }
}
