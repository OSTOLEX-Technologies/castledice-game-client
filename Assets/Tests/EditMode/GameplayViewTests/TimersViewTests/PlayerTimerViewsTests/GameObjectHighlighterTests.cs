using NUnit.Framework;
using Src.GameplayView.Timers.PlayerTimerViews;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.TimersViewTests.PlayerTimerViewsTests
{
    public class GameObjectHighlighterTests
    {
        private const string HighlightGameObjectFieldName = "highlight";
        
        [Test]
        public void Highlight_ShouldSetActiveHighlightGameObject_ToTrue()
        {
            var highlightGameObject = new GameObject();
            highlightGameObject.SetActive(false);
            var highlighter = new GameObject().AddComponent<GameObjectHighlighter>();
            highlighter.SetPrivateField(HighlightGameObjectFieldName, highlightGameObject);
            
            highlighter.Highlight();
            
            Assert.IsTrue(highlightGameObject.activeSelf);
        }
        
        [Test]
        public void Obscure_ShouldSetActiveHighlightGameObject_ToFalse()
        {
            var highlightGameObject = new GameObject();
            highlightGameObject.SetActive(true);
            var highlighter = new GameObject().AddComponent<GameObjectHighlighter>();
            highlighter.SetPrivateField(HighlightGameObjectFieldName, highlightGameObject);
            
            highlighter.Obscure();
            
            Assert.IsFalse(highlightGameObject.activeSelf);
        }
    }
}