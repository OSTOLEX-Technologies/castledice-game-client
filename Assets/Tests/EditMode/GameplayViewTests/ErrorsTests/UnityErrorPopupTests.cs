using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.Errors;
using TMPro;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.ErrorsTests
{
    public class UnityErrorPopupTests
    {
        [Test]
        public void Show_ShouldSetActiveTrue_OnGameObject()
        {
            var gameObject = new GameObject();
            gameObject.SetActive(false);
            var popup = gameObject.AddComponent<UnityErrorPopup>();
            
            popup.Show();
            
            Assert.IsTrue(gameObject.activeSelf);
        }
        
        [Test]
        public void Hide_ShouldSetActiveFalse_OnGameObject()
        {
            var gameObject = new GameObject();
            gameObject.SetActive(true);
            var popup = gameObject.AddComponent<UnityErrorPopup>();
            
            popup.Hide();
            
            Assert.IsFalse(gameObject.activeSelf);
        }

        [Test]
        [TestCase("Test error message")]
        [TestCase("Some other error message")]
        [TestCase("Some other error message with numbers 1234567890")]
        public void ShowError_ShouldSetGivenMessage_ToMessageTextMesh(string message)
        {
            var gameObject = new GameObject();
            var textMesh = gameObject.AddComponent<TextMeshProUGUI>();
            var popup = gameObject.AddComponent<UnityErrorPopup>();
            ReflectionUtility.AddObjectReferenceValueToSerializedProperty(popup, "messageTextMesh", textMesh);
            
            popup.SetMessage(message);
            
            Assert.AreEqual(message, textMesh.text);
        }
    }
}