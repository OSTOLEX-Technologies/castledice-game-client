using System.Collections;
using Moq;
using NUnit.Framework;
using Src.GameplayView.Audio;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using UnityEngine;
using UnityEngine.TestTools;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentAudioTests.KnightAudioTests
{
    public class SoundPlayerKnightAudioTests
    {
        [Test]
        public void PlayPlaceSound_ShouldPlayGivenPlaceSound_ViaPlayer()
        {
            var expectedPlaceSound = GetSound();
            var soundPlayerMock = new Mock<SoundPlayer>();
            var soundPlayerKnightAudio = new GameObject().AddComponent<SoundPlayerKnightAudio>();
            soundPlayerKnightAudio.Init(expectedPlaceSound, GetSound(), GetSound());
            SetPrivateFieldValue(soundPlayerMock.Object, soundPlayerKnightAudio, "soundPlayer");
            
            soundPlayerKnightAudio.PlayPlaceSound();
            
            soundPlayerMock.Verify(soundPlayer => soundPlayer.PlaySound(expectedPlaceSound), Times.Once);
        }
        
        [Test]
        public void PlayHitSound_ShouldPlayGivenHitSound_ViaPlayer()
        {
            var expectedHitSound = GetSound();
            var soundPlayerMock = new Mock<SoundPlayer>();
            var soundPlayerKnightAudio = new GameObject().AddComponent<SoundPlayerKnightAudio>();
            soundPlayerKnightAudio.Init(GetSound(), expectedHitSound, GetSound());
            SetPrivateFieldValue(soundPlayerMock.Object, soundPlayerKnightAudio, "soundPlayer");
            
            soundPlayerKnightAudio.PlayHitSound();
            
            soundPlayerMock.Verify(soundPlayer => soundPlayer.PlaySound(expectedHitSound), Times.Once);
        }
        
        [Test]
        public void PlayDestroySound_ShouldPlayGivenDestroySound_ViaPlayer()
        {
            var expectedDestroySound = GetSound();
            var soundPlayerMock = new Mock<SoundPlayer>();
            var soundPlayerKnightAudio = new GameObject().AddComponent<SoundPlayerKnightAudio>();
            soundPlayerKnightAudio.Init(GetSound(), GetSound(), expectedDestroySound);
            SetPrivateFieldValue(soundPlayerMock.Object, soundPlayerKnightAudio, "soundPlayer");
            
            soundPlayerKnightAudio.PlayDestroySound();
            
            soundPlayerMock.Verify(soundPlayer => soundPlayer.PlaySound(expectedDestroySound), Times.Once);
        }
        
        [UnityTest]
        public IEnumerator PlayDestroySound_ShouldInvokeDestroySoundPlayedEvent_AfterAudioClipIsPlayed()
        {
            var soundPlayerMock = new Mock<SoundPlayer>();
            var destroySound = GetSound();
            var audio = new GameObject().AddComponent<SoundPlayerKnightAudio>();
            SetPrivateFieldValue(soundPlayerMock.Object, audio, "soundPlayer");
            audio.Init(GetSound(), GetSound(), destroySound);
            var eventInvoked = false;
            audio.DestroySoundPlayed += () => eventInvoked = true;
            
            audio.PlayDestroySound();
            yield return new WaitForSeconds(destroySound.Clip.length * 2);
            
            Assert.IsTrue(eventInvoked);
        }
    }
}