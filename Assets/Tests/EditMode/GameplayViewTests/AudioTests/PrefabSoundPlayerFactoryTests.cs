using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.Audio;

namespace Tests.EditMode.GameplayViewTests.AudioTests
{
    public class PrefabSoundPlayerFactoryTests
    {
        [Test]
        public void GetSoundPlayer_ShouldReturn_InstantiatedSoundPlayerPrefab()
        {
            var prefab = new Mock<UnitySoundPlayer>().Object;
            var instantiatedPrefab = new Mock<UnitySoundPlayer>().Object;
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(instantiator => instantiator.Instantiate(prefab)).Returns(instantiatedPrefab);
            var factory = new PrefabSoundPlayerFactory(prefab, instantiatorMock.Object);
            
            var soundPlayer = factory.GetSoundPlayer();
            
            Assert.AreSame(instantiatedPrefab, soundPlayer);
        }
    }
}