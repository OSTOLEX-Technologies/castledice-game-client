using Moq;
using NUnit.Framework;
using Src.GameplayView;
using static Tests.Utils.ObjectCreationUtility;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentAudioTests.CastleAudioTests
{
    public class SoundPlayerCastleAudioFactoryTests
    {
        [Test]
        public void GetAudio_ShouldReturnInstantiatedPrefab()
        {
            var prefab = new GameObject().AddComponent<SoundPlayerCastleAudio>();
            var instantiatedPrefab = new GameObject().AddComponent<SoundPlayerCastleAudio>();
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(instantiator => instantiator.Instantiate(prefab)).Returns(instantiatedPrefab);
            var factory = new SoundPlayerCastleAudioFactoryBuilder
            {
                Prefab = prefab,
                Instantiator = instantiatorMock.Object
            }.Build();
            
            var audio = factory.GetAudio(GetCastle());
            
            Assert.AreSame(instantiatedPrefab, audio);
        }

        [Test]
        public void GetAudio_ShouldReturnAudio_WithSoundsFromGivenProvider()
        {
            var destroySound = GetSound();
            var hitSound = GetSound();
            var castleSoundsProviderMock = new Mock<ICastleSoundsProvider>();
            castleSoundsProviderMock.Setup(provider => provider.GetHitSound(It.IsAny<CastleGO>())).Returns(hitSound);
            castleSoundsProviderMock.Setup(provider => provider.GetDestroySound(It.IsAny<CastleGO>())).Returns(destroySound);
            var factory = new SoundPlayerCastleAudioFactoryBuilder
            {
                CastleSoundsProvider = castleSoundsProviderMock.Object
            }.Build();
            
            var audio = factory.GetAudio(GetCastle());
            var hitSoundFieldInfo = audio.GetType().GetField("_hitSound", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var destroySoundFieldInfo = audio.GetType().GetField("_destroySound", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var actualHitSound = hitSoundFieldInfo.GetValue(audio);
            var actualDestroySound = destroySoundFieldInfo.GetValue(audio);
            
            Assert.AreSame(hitSound, actualHitSound);
            Assert.AreSame(destroySound, actualDestroySound);
        }

        private class SoundPlayerCastleAudioFactoryBuilder
        {
            public ICastleSoundsProvider CastleSoundsProvider { get; set; }
            public SoundPlayerCastleAudio Prefab { get; set; }
            public IInstantiator Instantiator { get; set; }

            public SoundPlayerCastleAudioFactoryBuilder()
            {
                var destroySound = GetSound();
                var hitSound = GetSound();
                var castleSoundsProviderMock = new Mock<ICastleSoundsProvider>();
                castleSoundsProviderMock.Setup(provider => provider.GetHitSound(It.IsAny<CastleGO>())).Returns(hitSound);
                castleSoundsProviderMock.Setup(provider => provider.GetDestroySound(It.IsAny<CastleGO>())).Returns(destroySound);
                CastleSoundsProvider = castleSoundsProviderMock.Object;
                Prefab = new GameObject().AddComponent<SoundPlayerCastleAudio>();
                var instantiatedPrefab = new GameObject().AddComponent<SoundPlayerCastleAudio>();
                var instantiatorMock = new Mock<IInstantiator>();
                instantiatorMock.Setup(instantiator => instantiator.Instantiate(Prefab)).Returns(instantiatedPrefab);
                Instantiator = instantiatorMock.Object;
            }
            
            public SoundPlayerCastleAudioFactory Build()
            {
                return new SoundPlayerCastleAudioFactory(CastleSoundsProvider, Prefab, Instantiator);
            }
        }
    }
}