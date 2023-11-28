using NUnit.Framework;
using static Tests.ObjectCreationUtility;

using Src.GameplayView.Audio;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentAudioTests.KnightAudioTests
{
    public class KnightSoundsConfigTests
    {
        [Test]
        public void GetPlaceSound_ShouldReturnSound_CreatedAccordingToConfig()
        {
            var soundConfig = new SoundConfig
            {
                clip = GetAudioClip(),
                volume = Random.value
            };
            var config = ScriptableObject.CreateInstance<KnightSoundsConfig>();
            SetPrivateFieldValue(soundConfig, config, "placeSound");
            
            var sound = config.GetPlaceSound(GetKnight());
            
            Assert.AreSame(soundConfig.clip, sound.Clip);
            Assert.AreEqual(soundConfig.volume, sound.Volume);
        }
        
        [Test]
        public void GetHitSound_ShouldReturnSound_CreatedAccordingToConfig()
        {
            var soundConfig = new SoundConfig
            {
                clip = GetAudioClip(),
                volume = Random.value
            };
            var config = ScriptableObject.CreateInstance<KnightSoundsConfig>();
            SetPrivateFieldValue(soundConfig, config, "hitSound");
            
            var sound = config.GetHitSound(GetKnight());
            
            Assert.AreSame(soundConfig.clip, sound.Clip);
            Assert.AreEqual(soundConfig.volume, sound.Volume);
        }
        
        [Test]
        public void GetDestroySound_ShouldReturnSound_CreatedAccordingToConfig()
        {
            var soundConfig = new SoundConfig
            {
                clip = GetAudioClip(),
                volume = Random.value
            };
            var config = ScriptableObject.CreateInstance<KnightSoundsConfig>();
            SetPrivateFieldValue(soundConfig, config, "destroySound");
            
            var sound = config.GetDestroySound(GetKnight());
            
            Assert.AreSame(soundConfig.clip, sound.Clip);
            Assert.AreEqual(soundConfig.volume, sound.Volume);
        }
    }
}