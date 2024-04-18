using NUnit.Framework;
using Src.GameplayPresenter.PlayerInitialization;
using UnityEngine;

namespace Tests.EditMode.GameplayPresenterTests.PlayerInitializationTests
{
    public class PlayerInitializationViewTests
    {
        [Test]
        public void ShowProcessMessage_ShouldSetProcessMessageActive()
        {
            var processMessage = new GameObject();
            processMessage.SetActive(false);
            var view = new PlayerInitializationView(processMessage, new GameObject());
            
            view.ShowProcessMessage();
            
            Assert.IsTrue(processMessage.activeSelf);
        }
        
        [Test]
        public void HideProcessMessage_ShouldSetProcessMessageInactive()
        {
            var processMessage = new GameObject();
            processMessage.SetActive(true);
            var view = new PlayerInitializationView(processMessage, new GameObject());
            
            view.HideProcessMessage();
            
            Assert.IsFalse(processMessage.activeSelf);
        }
        
        [Test]
        public void ShowFailureMessage_ShouldSetFailureMessageActive()
        {
            var failureMessage = new GameObject();
            failureMessage.SetActive(false);
            var view = new PlayerInitializationView(new GameObject(), failureMessage);
            
            view.ShowFailureMessage();
            
            Assert.IsTrue(failureMessage.activeSelf);
        }
    }
}