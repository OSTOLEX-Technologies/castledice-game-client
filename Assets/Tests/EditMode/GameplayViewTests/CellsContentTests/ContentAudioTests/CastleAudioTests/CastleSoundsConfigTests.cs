using NUnit.Framework;
using Src.GameplayView.Audio;
using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using UnityEngine;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentAudioTests.CastleAudioTests
{
    public class CastleSoundsConfigTests
    {
        [Test]
        public void GetHitSound_ShouldReturnSound_CreatedAccordingToConfig()
        {
            var volume = Random.value;
            var audioClip = GetAudioClip();
            var hitSoundConfig = new SoundConfig
            {
                clip = audioClip,
                volume = volume
            };
            var castle = GetCastle();
            var castleSoundsConfig = ScriptableObject.CreateInstance<CastleSoundsConfig>();
            SetPrivateFieldValue(hitSoundConfig, castleSoundsConfig, "hitSoundConfig");
            
            var result = castleSoundsConfig.GetHitSound(castle);
            
            Assert.AreSame(audioClip, result.Clip);
            Assert.AreEqual(volume, result.Volume);
        }
        
        [Test]
        public void GetDestroySound_ShouldReturnSound_CreatedAccordingToConfig()
        {
            var volume = Random.value;
            var audioClip = GetAudioClip();
            var destroySoundConfig = new SoundConfig
            {
                clip = audioClip,
                volume = volume
            };
            var castle = GetCastle();
            var castleSoundsConfig = ScriptableObject.CreateInstance<CastleSoundsConfig>();
            SetPrivateFieldValue(destroySoundConfig, castleSoundsConfig, "destroySoundConfig");
            
            var result = castleSoundsConfig.GetDestroySound(castle);
            
            Assert.AreSame(audioClip, result.Clip);
            Assert.AreEqual(volume, result.Volume);
        }
    }
}