using NUnit.Framework;
using Src.GameplayView.Audio;
using UnityEngine;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.AudioTests
{
    public class SoundTests
    {
        [Test]
        public void Properties_ShouldReturnValues_GivenInConstructor()
        {
            var expectedClip = GetAudioClip();
            var expectedVolume = Random.value;
            var sound = new Sound(expectedClip, expectedVolume);

            Assert.AreSame(expectedClip, sound.Clip);
            Assert.AreEqual(expectedVolume, sound.Volume, 0.0001f);
        }
    }
}