using UnityEngine;
using UnityEngine.Events;

namespace Src.Components
{
    //TODO: Clarify demands for this class, refactor it and test.
    /// <summary>
    /// This class invokes an event when audio from given audio source stops playing.
    /// Currently events is invoked only once.
    /// </summary>
    public class OnAudioStopEvent : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private UnityEvent onAudioStop;
        private bool _isInvoked;
        private void Update()
        {
            if (audioSource.isPlaying) return;
            if (_isInvoked) return;
            onAudioStop.Invoke();
            _isInvoked = true;
        }
    }
}
