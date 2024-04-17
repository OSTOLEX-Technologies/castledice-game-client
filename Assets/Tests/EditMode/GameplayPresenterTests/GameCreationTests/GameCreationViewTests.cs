using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.GameSearching;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests
{
    public class GameCreationViewTests
    {
        [Test]
        public void ShowMatchmakingScreen_ShouldSetMatchmakingScreen_Active()
        {
            var matchMakingScreen = new GameObject();
            matchMakingScreen.SetActive(false);
            var view = new ViewBuilder{MatchmakingScreen = matchMakingScreen}.Build();
            
            view.ShowMatchmakingScreen();
            
            Assert.IsTrue(matchMakingScreen.activeSelf);
        }
        
        [Test]
        public void HideMatchmakingScreen_ShouldSetMatchmakingScreen_Inactive()
        {
            var matchMakingScreen = new GameObject();
            matchMakingScreen.SetActive(true);
            var view = new ViewBuilder{MatchmakingScreen = matchMakingScreen}.Build();
            
            view.HideMatchmakingScreen();
            
            Assert.IsFalse(matchMakingScreen.activeSelf);
        }
        
        [Test]
        public void ShowCancellationScreen_ShouldSetCancellationScreen_Active()
        {
            var cancellationScreen = new GameObject();
            cancellationScreen.SetActive(false);
            var view = new ViewBuilder{CancellationScreen = cancellationScreen}.Build();
            
            view.ShowCancellationScreen();
            
            Assert.IsTrue(cancellationScreen.activeSelf);
        }
        
        [Test]
        public void HideCancellationScreen_ShouldSetCancellationScreen_Inactive()
        {
            var cancellationScreen = new GameObject();
            cancellationScreen.SetActive(true);
            var view = new ViewBuilder{CancellationScreen = cancellationScreen}.Build();
            
            view.HideCancellationScreen();
            
            Assert.IsFalse(cancellationScreen.activeSelf);
        }

        [Test]
        public void ShowFail_ShouldSetFailMessage_Active()
        {
            var failMessage = new GameObject();
            failMessage.SetActive(false);
            var view = new ViewBuilder{FailMessage = failMessage}.Build();
            
            view.ShowFail(It.IsAny<SearchFailReason>());
            
            Assert.IsTrue(failMessage.activeSelf);
        }

        [Test]
        [TestCaseSource(nameof(FailReasons))]
        public void ShowFail_ShouldSetFailMessageText_AccordingToConfig_BasedOnFailReason(SearchFailReason reason)
        {
            var failMessageTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
            var failMessagesConfig = new Mock<IFailMessagesConfig>();
            var expectedMessage = new Random().Next().ToString();
            failMessagesConfig.Setup(x => x.GetFailMessage(reason)).Returns(expectedMessage);
            var view = new ViewBuilder
            {
                FailMessageTextMesh = failMessageTextMesh, 
                FailMessagesConfig = failMessagesConfig.Object
            }.Build();
            
            view.ShowFail(reason);
            
            Assert.AreEqual(expectedMessage, failMessageTextMesh.text);
        }
        
        [Test]
        public void PlayChosen_ShouldBeInvoked_WhenPlayButtonClicked()
        {
            var playButton = new GameObject().AddComponent<Button>();
            var view = new ViewBuilder{PlayButton = playButton}.Build();
            var playChosenInvoked = false;
            view.PlayChosen += () => playChosenInvoked = true;
            
            playButton.onClick.Invoke();
            
            Assert.IsTrue(playChosenInvoked);
        }
        
        [Test]
        public void CancelChosen_ShouldBeInvoked_WhenCancelButtonClicked()
        {
            var cancelButton = new GameObject().AddComponent<Button>();
            var view = new ViewBuilder{CancelButton = cancelButton}.Build();
            var cancelChosenInvoked = false;
            view.CancelChosen += () => cancelChosenInvoked = true;
            
            cancelButton.onClick.Invoke();
            
            Assert.IsTrue(cancelChosenInvoked);
        }
        
        public static IEnumerable<SearchFailReason> FailReasons()
        {
            var values = System.Enum.GetValues(typeof(SearchFailReason));
            foreach (var value in values)
            {
                yield return (SearchFailReason)value;
            }
        }
        
        private class ViewBuilder
        {
            public Button PlayButton { get; set; }
            public Button CancelButton { get; set; }
            public GameObject MatchmakingScreen { get; set; }
            public GameObject CancellationScreen { get; set; }
            public GameObject FailMessage { get; set; }
            public TextMeshProUGUI FailMessageTextMesh { get; set; }
            public IFailMessagesConfig FailMessagesConfig { get; set; }

            public ViewBuilder()
            {
                PlayButton = new GameObject().AddComponent<Button>();
                CancelButton = new GameObject().AddComponent<Button>();
                MatchmakingScreen = new GameObject();
                CancellationScreen = new GameObject();
                FailMessage = new GameObject();
                FailMessageTextMesh = new GameObject().AddComponent<TextMeshProUGUI>();
                FailMessagesConfig = new Mock<IFailMessagesConfig>().Object;
            }
            
            public GameCreationView Build()
            {
                return new GameCreationView(PlayButton, 
                    CancelButton, 
                    MatchmakingScreen,
                    CancellationScreen, 
                    FailMessage, 
                    FailMessageTextMesh, 
                    FailMessagesConfig);
            }
        }
    }
}