using castledice_game_logic.GameObjects;
using UnityEngine;
using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentAudioTests.KnightAudioTests
{
    public class SoundPlayerKnightAudioFactoryTests
    {
        [Test]
        public void GetAudio_ShouldReturnSoundPlayerKnightAudio_WithAppropriateSounds()
        {
            var knight = GetKnight();
            var expectedPlaceSound = GetSound();
            var expectedHitSound = GetSound();
            var expectedDestroySound = GetSound();
            var soundsProviderMock = new Mock<IKnightSoundsProvider>();
            soundsProviderMock.Setup(provider => provider.GetPlaceSound(knight)).Returns(expectedPlaceSound);
            soundsProviderMock.Setup(provider => provider.GetHitSound(knight)).Returns(expectedHitSound);
            soundsProviderMock.Setup(provider => provider.GetDestroySound(knight)).Returns(expectedDestroySound);
            var factory = new SoundPlayerKnightAudioFactoryBuilder
            {
                SoundsProvider = soundsProviderMock.Object
            }.Build();
            
            var audio = factory.GetAudio(knight);
            var placeSoundFieldInfo = audio.GetType().GetField("_placeSound", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var hitSoundFieldInfo = audio.GetType().GetField("_hitSound", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var destroySoundFieldInfo = audio.GetType().GetField("_destroySound", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            Assert.AreSame(expectedPlaceSound, placeSoundFieldInfo.GetValue(audio));
            Assert.AreSame(expectedHitSound, hitSoundFieldInfo.GetValue(audio));
            Assert.AreSame(expectedDestroySound, destroySoundFieldInfo.GetValue(audio));
        }

        [Test]
        public void GetAudio_ShouldReturnSoundPlayerKnightAudio_InstantiatedFromPrefab()
        {
            var prefab = new GameObject().AddComponent<SoundPlayerKnightAudio>();
            var instantiatedPrefab = new GameObject().AddComponent<SoundPlayerKnightAudio>();
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(instantiator => instantiator.Instantiate(prefab)).Returns(instantiatedPrefab);
            var factory = new SoundPlayerKnightAudioFactoryBuilder
            {
                Prefab = prefab,
                Instantiator = instantiatorMock.Object
            }.Build();
            
            var audio = factory.GetAudio(GetKnight());
            
            Assert.AreSame(instantiatedPrefab, audio);
        }

        private class SoundPlayerKnightAudioFactoryBuilder
        {
            public IKnightSoundsProvider SoundsProvider { get; set; }
            public SoundPlayerKnightAudio Prefab { get; set; }
            public IInstantiator Instantiator { get; set; }

            public SoundPlayerKnightAudioFactoryBuilder()
            {
                var soundsProviderMock = new Mock<IKnightSoundsProvider>();
                var placeSound = GetSound();
                var hitSound = GetSound();
                var destroySound = GetSound();
                soundsProviderMock.Setup(provider => provider.GetPlaceSound(It.IsAny<Knight>())).Returns(placeSound);
                soundsProviderMock.Setup(provider => provider.GetHitSound(It.IsAny<Knight>())).Returns(hitSound);
                soundsProviderMock.Setup(provider => provider.GetDestroySound(It.IsAny<Knight>())).Returns(destroySound);
                SoundsProvider = soundsProviderMock.Object;
                Prefab = new GameObject().AddComponent<SoundPlayerKnightAudio>();
                var instantiatorMock = new Mock<IInstantiator>();
                var instantiatedPrefab = new GameObject().AddComponent<SoundPlayerKnightAudio>();
                instantiatorMock.Setup(instantiator => instantiator.Instantiate(Prefab)).Returns(instantiatedPrefab);
                Instantiator = instantiatorMock.Object;
            }
            
            public SoundPlayerKnightAudioFactory Build()
            {
                return new SoundPlayerKnightAudioFactory(SoundsProvider, Prefab, Instantiator);
            }
        }
    }
}