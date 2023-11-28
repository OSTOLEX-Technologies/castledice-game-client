using castledice_game_logic.GameObjects;
using Src.GameplayView.Audio;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentAudio.KnightAudio
{
    [CreateAssetMenu(fileName = "KnightSoundsConfig", menuName = "Configs/Content/Knight/KnightSoundsConfig")]
    public class KnightSoundsConfig : ScriptableObject, IKnightSoundsProvider
    {
        [SerializeField] private SoundConfig placeSound;
        [SerializeField] private SoundConfig hitSound;
        [SerializeField] private SoundConfig destroySound;
        
        public Sound GetPlaceSound(Knight knight)
        {
            return new Sound(placeSound.clip, placeSound.volume);
        }

        public Sound GetHitSound(Knight knight)
        {
            return new Sound(hitSound.clip, hitSound.volume);
        }

        public Sound GetDestroySound(Knight knight)
        {
            return new Sound(destroySound.clip, destroySound.volume);
        }
    }
}