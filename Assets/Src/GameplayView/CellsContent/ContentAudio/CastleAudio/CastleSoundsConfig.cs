using CastleGO = castledice_game_logic.GameObjects.Castle;
using Src.GameplayView.Audio;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentAudio.CastleAudio
{
    [CreateAssetMenu(fileName = "CastleSoundsConfig", menuName = "Configs/Content/Castle/CastleSoundsConfig")]

    public class CastleSoundsConfig : ScriptableObject, ICastleSoundsProvider
    {
        [SerializeField] private SoundConfig hitSoundConfig;
        [SerializeField] private SoundConfig destroySoundConfig;
        
        public Sound GetHitSound(CastleGO castle)
        {
            return new Sound(hitSoundConfig.clip, hitSoundConfig.volume);
        }

        public Sound GetDestroySound(CastleGO castle)
        {
            return new Sound(destroySoundConfig.clip, destroySoundConfig.volume);
        }
    }
}