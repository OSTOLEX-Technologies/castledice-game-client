using NUnit.Framework;
using Src.GameplayView.ActionPointsGiving;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Tests.PlayMode
{
    public class UnityActionPointsPopupTests
    {
        [Test]
        public void SetAmount_ShouldSetGivenNumberToNumberTextMesh([Values(1, 2, 3, 4, 5, 6)]int number)
        {
            var popup = new GameObject().AddComponent<UnityActionPointsPopup>();
            var numberTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            var labelTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            AddObjectReferenceValueToSerializedProperty(popup, "numberTextMesh", numberTextMesh);
            AddObjectReferenceValueToSerializedProperty(popup, "labelTextMesh", labelTextMesh);
            
            popup.SetAmount(number);
            
            Assert.AreEqual(number.ToString(), numberTextMesh.text);
        }

        [TestCase("action point", 1)]
        [TestCase("action points", 2)]
        [TestCase("action points", 3)]
        [TestCase("action points", 4)]
        [TestCase("action points", 5)]
        [TestCase("action points", 6)]
        public void SetAmount_ShouldSetAppropriateLabel_AccordingToAmount(string expectedLabel, int amount)
        {
            var popup = new GameObject().AddComponent<UnityActionPointsPopup>();
            var numberTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            var labelTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            AddObjectReferenceValueToSerializedProperty(popup, "numberTextMesh", numberTextMesh);
            AddObjectReferenceValueToSerializedProperty(popup, "labelTextMesh", labelTextMesh);
            
            popup.SetAmount(amount);
            
            Assert.AreEqual(expectedLabel, labelTextMesh.text);
        }
        
        [Test]
        public void Hide_ShouldSetActiveFalse()
        {
            var popup = new GameObject().AddComponent<UnityActionPointsPopup>();
            var numberTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            var labelTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            AddObjectReferenceValueToSerializedProperty(popup, "numberTextMesh", numberTextMesh);
            AddObjectReferenceValueToSerializedProperty(popup, "labelTextMesh", labelTextMesh);
            
            popup.Hide();
            
            Assert.IsFalse(popup.gameObject.activeSelf);
        }
        
        [Test]
        public void Show_ShouldSetActiveTrue()
        {
            var popup = new GameObject().AddComponent<UnityActionPointsPopup>();
            var numberTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            var labelTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            AddObjectReferenceValueToSerializedProperty(popup, "numberTextMesh", numberTextMesh);
            AddObjectReferenceValueToSerializedProperty(popup, "labelTextMesh", labelTextMesh);
            
            popup.Hide();
            popup.Show();
            
            Assert.IsTrue(popup.gameObject.activeSelf);
        }
        
        private static void AddObjectReferenceValueToSerializedProperty<T>(UnityActionPointsPopup popup, string propertyName, T value) where T : Component
        {
            var serializedObject = new SerializedObject(popup);
            serializedObject.FindProperty(propertyName).objectReferenceValue = value;
            serializedObject.ApplyModifiedProperties();
        }
    }
}