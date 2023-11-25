using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentAudio
{
    public class UnityKnightAudio : MonoBehaviour, IKnightAudio
    {
        [SerializeField] private AudioClip placeSound;
        [SerializeField] private AudioClip hitSound;
        [SerializeField] private AudioClip destroySound;
        private  IAudioClipPlayer _clipPlayer;
        
        private void Awake()
        {
            _clipPlayer = GetComponent<IAudioClipPlayer>();
        }

        public void PlayPlaceSound()
        {
            _clipPlayer.PlayClip(placeSound);
        }

        public void PlayHitSound()
        {
            _clipPlayer.PlayClip(hitSound);
        }

        public void PlayDestroySound()
        {
            _clipPlayer.PlayClip(destroySound);
        }
    }
}