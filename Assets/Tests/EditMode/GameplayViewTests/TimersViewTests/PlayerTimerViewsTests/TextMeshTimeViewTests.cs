using System;
using NUnit.Framework;
using Src.GameplayView.Timers.PlayerTimerViews;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace Tests.EditMode.GameplayViewTests.TimersViewTests.PlayerTimerViewsTests
{
    public class TextMeshTimeViewTests
    {
        private const string TextMeshFieldName = "textMesh";
        
        [Test]
        //In this test by appropriate format we mean MM:SS
        public void SetTime_ShouldSetTextMeshText_FromGivenTimeSpanInAppropriateFormat()
        {
            var rnd = new Random();
            var minutes = rnd.Next(0, 60);
            var seconds = rnd.Next(0, 60);
            var expectedText = $"{minutes:D2}:{seconds:D2}";
            var timeSpan = new TimeSpan(0, minutes, seconds);
            var gameObject = new GameObject();
            var textMeshTimeView = gameObject.AddComponent<TextMeshTimeView>();
            textMeshTimeView.SetPrivateField(TextMeshFieldName, new GameObject().AddComponent<TextMeshProUGUI>());
            
            textMeshTimeView.SetTime(timeSpan);
            var textMesh = textMeshTimeView.GetPrivateField<TextMeshProUGUI>(TextMeshFieldName);
            var actualText = textMesh.text;
            
            Assert.AreEqual(expectedText, actualText);
        }
    }
}