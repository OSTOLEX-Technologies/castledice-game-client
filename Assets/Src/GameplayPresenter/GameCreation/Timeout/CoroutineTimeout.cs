using System;
using System.Collections;
using UnityEngine;

namespace Src.GameplayPresenter.GameCreation.Timeout
{
    public class CoroutineTimeout : MonoBehaviour, ITimeout
    {
        [SerializeField] private float seconds;

        public void StartCountdown()
        {
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            yield return new WaitForSecondsRealtime(seconds);
            TimeOut?.Invoke();
        }

        public event Action TimeOut;
    }
}