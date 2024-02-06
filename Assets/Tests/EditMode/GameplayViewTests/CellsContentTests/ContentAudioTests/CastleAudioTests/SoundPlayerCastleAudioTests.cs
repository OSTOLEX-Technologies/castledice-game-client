﻿using System.Collections;
using Moq;
using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.Audio;
using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentAudioTests.CastleAudioTests
{
    public class SoundPlayerCastleAudioTests
    {
        [Test]
        public void PlayHitSound_ShouldPlayerGivenHitSound_OnSerializedSoundPlayer()
        {
            var soundPlayerMock = new Mock<SoundPlayer>();
            var hitSound = GetSound();
            var destroySound = GetSound();
            var audio = new GameObject().AddComponent<SoundPlayerCastleAudio>();
            SetPrivateFieldValue(soundPlayerMock.Object, audio, "soundPlayer");
            audio.Init(hitSound, destroySound);
            
            audio.PlayHitSound();
            
            soundPlayerMock.Verify(player => player.PlaySound(hitSound), Times.Once);
        }
        
        [Test]
        public void PlayDestroySound_ShouldPlayerGivenDestroySound_OnSerializedSoundPlayer()
        {
            var soundPlayerMock = new Mock<SoundPlayer>();
            var hitSound = GetSound();
            var destroySound = GetSound();
            var audio = new GameObject().AddComponent<SoundPlayerCastleAudio>();
            SetPrivateFieldValue(soundPlayerMock.Object, audio, "soundPlayer");
            audio.Init(hitSound, destroySound);
            
            audio.PlayDestroySound();
            
            soundPlayerMock.Verify(player => player.PlaySound(destroySound), Times.Once);
        }
        
        [UnityTest]
        public IEnumerator PlayDestroySound_ShouldInvokeDestroySoundPlayedEvent_AfterAudioClipIsPlayed()
        {
            var soundPlayerMock = new Mock<SoundPlayer>();
            var hitSound = GetSound();
            var destroySound = GetSound();
            var audio = new GameObject().AddComponent<SoundPlayerCastleAudio>();
            SetPrivateFieldValue(soundPlayerMock.Object, audio, "soundPlayer");
            audio.Init(hitSound, destroySound);
            var eventInvoked = false;
            audio.DestroySoundPlayed += () => eventInvoked = true;
            
            audio.PlayDestroySound();
            yield return new WaitForSeconds(destroySound.Clip.length * 2);
            
            Assert.IsTrue(eventInvoked);
        }
    }
}