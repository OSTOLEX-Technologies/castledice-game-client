using NUnit.Framework;
using Src.Auth.AuthTokenSaver.PlayerPrefsStringSaver;
using UnityEngine;

namespace Tests.EditMode.AuthTests.TokenSaving
{
    [TestFixture]
    public class PlayerPrefsStringSaverTests
    {
        private const string SamplePrefName = "SAMPLE_PREF";
        private const string SamplePrefValue = "SAMPLE_PREF_VALUE";

        [Test]
        public void SaveStringValue_ShouldSaveCorrectly()
        {
            var saver = new StringSaver();

            saver.SaveStringValue(SamplePrefName, SamplePrefValue);

            var storedValue = PlayerPrefs.GetString(SamplePrefName);
            PlayerPrefs.DeleteKey(SamplePrefName);

            Assert.IsTrue(storedValue == SamplePrefValue);
        }

        [Test]
        public void TryGetStringValue_ShouldReturnValid()
        {
            var saver = new StringSaver();

            saver.SaveStringValue(SamplePrefName, SamplePrefValue);

            saver.TryGetStringValue(SamplePrefName, out var storedValue);
            PlayerPrefs.DeleteKey(SamplePrefName);

            Assert.IsTrue(storedValue == SamplePrefValue);
        }

        [Test]
        public void TryGetStringValue_ShouldFail()
        {
            var saver = new StringSaver();
            PlayerPrefs.DeleteKey(SamplePrefName);
            var exists = saver.TryGetStringValue(SamplePrefName, out var storedValue);

            Assert.IsFalse(exists);
        }
    }
}