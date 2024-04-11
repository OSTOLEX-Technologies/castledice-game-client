using System;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.PlayerInitialization;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;
using Src.NetworkingModule;
using UnityEngine;
using UnityEngine.UI;

namespace Tests.EditMode.GameplayPresenterTests.PlayerInitializationTests
{
    public class InitializeButtonHandlerTests
    {
        [Test]
        public void Handler_ShouldCallStartInitializationAsync_WhenButtonClicked()
        {
            var presenterMock = new Mock<IPlayerInitializationPresenter>();
            var button = new GameObject().AddComponent<Button>();
            var handler = new InitializeButtonHandlerBuilder
            {
                Presenter = presenterMock.Object,
                Button = button
            }.Build();
            
            button.onClick.Invoke();
            
            presenterMock.Verify(p => p.StartInitializationAsync(), Times.Once);
        }
        
        [Test]
        public void Handler_ShouldSetButtonInactive_WhenButtonClicked()
        {
            var button = new GameObject().AddComponent<Button>();
            button.gameObject.SetActive(true);
            var handler = new InitializeButtonHandlerBuilder
            {
                Button = button
            }.Build();
            
            button.onClick.Invoke();
            
            Assert.IsFalse(button.gameObject.activeSelf);
        }

        [Test]
        public void Handler_ShouldSetButtonActive_IfInitializationFailed()
        {
            var eventsEmitterMock = new Mock<IPlayerInitializationResultEventsEmitter>();
            var button = new GameObject().AddComponent<Button>();
            button.gameObject.SetActive(false);
            var handler = new InitializeButtonHandlerBuilder
            {
                EventsEmitter = eventsEmitterMock.Object,
                Button = button
            }.Build();
            
            eventsEmitterMock.Raise(e => e.InitializationFailed += null, this, EventArgs.Empty);
            
            Assert.IsTrue(button.gameObject.activeSelf);
        }
        
        [Test]
        public void Handler_ShouldSetButtonInactive_IfInitializationSucceed()
        {
            var eventsEmitterMock = new Mock<IPlayerInitializationResultEventsEmitter>();
            var button = new GameObject().AddComponent<Button>();
            button.gameObject.SetActive(true);
            var handler = new InitializeButtonHandlerBuilder
            {
                EventsEmitter = eventsEmitterMock.Object,
                Button = button
            }.Build();
            
            eventsEmitterMock.Raise(e => e.InitializationSucceed += null, this, EventArgs.Empty);
            
            Assert.IsFalse(button.gameObject.activeSelf);
        }

        private class InitializeButtonHandlerBuilder
        {
            public IPlayerInitializationPresenter Presenter { get; set; }
            public IClientWrapper ClientWrapper { get; set; }
            public IPlayerInitializationResultEventsEmitter EventsEmitter { get; set; }
            public Button Button { get; set; }

            public InitializeButtonHandlerBuilder()
            {
                var presenter = new Mock<IPlayerInitializationPresenter>();
                Presenter = presenter.Object;
                var clientWrapper = new Mock<IClientWrapper>();
                ClientWrapper = clientWrapper.Object;
                var eventsEmitter = new Mock<IPlayerInitializationResultEventsEmitter>();
                EventsEmitter = eventsEmitter.Object;
                var button = new GameObject().AddComponent<Button>();
                Button = button;
            }
            
            public InitializeButtonHandler Build()
            {
                return new InitializeButtonHandler(Presenter, ClientWrapper, EventsEmitter, Button);
            }
        }
    }
}