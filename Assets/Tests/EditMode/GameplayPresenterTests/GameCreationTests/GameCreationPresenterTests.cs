using System;
using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.CreationHandling;
using Src.GameplayPresenter.GameCreation.Creators.GameCreator;
using Src.GameplayPresenter.GameCreation.GameSearching;
using Tests.Utils;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests
{
    public class GameCreationPresenterTests
    {
        [Test]
        public void Presenter_ShouldShowMatchmakingScreen_IfPlayChosen()
        {
            var viewMock = new Mock<IGameCreationView>();
            var presenter = new PresenterBuilder()
            {
                View = viewMock.Object
            }.Build();
            
            viewMock.Raise(x => x.PlayChosen += null);
            
            viewMock.Verify(x => x.ShowMatchmakingScreen());
        }

        [Test]
        public void Presenter_ShouldSearchGameOnce_IfPlayChosen()
        {
            var viewMock = new Mock<IGameCreationView>();
            var searcherMock = new Mock<IGameSearcher>();
            var presenter = new PresenterBuilder()
            {
                View = viewMock.Object,
                GameSearcher = searcherMock.Object,
            }.Build();
            
            viewMock.Raise(x => x.PlayChosen += null);
            
            searcherMock.Verify(x => x.Search(), Times.Once);
        }

        [Test]
        public void Presenter_ShowCancellationScreen_IfCancelChosen()
        {
            var viewMock = new Mock<IGameCreationView>();
            var presenter = new PresenterBuilder()
            {
                View = viewMock.Object
            }.Build();
            
            viewMock.Raise(x => x.CancelChosen += null);
            
            viewMock.Verify(x => x.ShowCancellationScreen());
        }
        
        [Test]
        public void Presenter_ShouldCancelGameSearchOnce_IfCancelChosen()
        {
            var viewMock = new Mock<IGameCreationView>();
            var searcherMock = new Mock<IGameSearcher>();
            var presenter = new PresenterBuilder()
            {
                View = viewMock.Object,
                GameSearcher = searcherMock.Object,
            }.Build();
            
            viewMock.Raise(x => x.CancelChosen += null);
            
            searcherMock.Verify(x => x.Cancel(), Times.Once);
        }
        
        [Test]
        public void Presenter_ShouldPassGameStartDataOnce_ToGameCreator_IfGameFound()
        {
            var gameCreatorMock = new Mock<IGameCreator>();
            var searcherMock = new Mock<IGameSearcher>();
            var startData = ObjectCreationUtility.GetGameStartData();
            var presenter = new PresenterBuilder()
            {
                GameCreator = gameCreatorMock.Object,
                GameSearcher = searcherMock.Object,
            }.Build();

            searcherMock.Raise(x => x.GameFound += null, startData);

            gameCreatorMock.Verify(x => x.CreateGame(startData), Times.Once);
        }
        
        [Test]
        public void Presenter_ShouldPassGameStartDataOnce_ToGameCreationHandler_IfGameFound()
        {
            var gameCreationHandlerMock = new Mock<IGameCreationHandler>();
            var searcherMock = new Mock<IGameSearcher>();
            var startData = ObjectCreationUtility.GetGameStartData();
            var presenter = new PresenterBuilder()
            {
                GameCreationHandler = gameCreationHandlerMock.Object,
                GameSearcher = searcherMock.Object,
            }.Build();

            searcherMock.Raise(x => x.GameFound += null, startData);

            gameCreationHandlerMock.Verify(
                x => x.HandleCreatedGame(
                It.IsAny<Game>(), startData), 
                Times.Once);
        }
        
        [Test]
        public void Presenter_ShouldPassCreatedGame_FromCreator_ToGameCreationHandler_IfGameFound()
        {
            var game = ObjectCreationUtility.GetGame();
            var gameCreatorMock = new Mock<IGameCreator>();
            gameCreatorMock.Setup(
                x => x.CreateGame(
                    It.IsAny<GameStartData>())).Returns(game);
            var gameCreationHandlerMock = new Mock<IGameCreationHandler>();
            var searcherMock = new Mock<IGameSearcher>();
            var presenter = new PresenterBuilder()
            {
                GameCreationHandler = gameCreationHandlerMock.Object,
                GameSearcher = searcherMock.Object,
                GameCreator = gameCreatorMock.Object,
            }.Build();

            searcherMock.Raise(
                x => x.GameFound += null, 
                It.IsAny<GameStartData>());

            gameCreationHandlerMock.Verify(
                x => x.HandleCreatedGame(
                    game, It.IsAny<GameStartData>()), 
                Times.Once);
        }
        
        [Test]
        public void Presenter_ShouldHideMatchmakingScreen_IfCancellationApproved()
        {
            var viewMock = new Mock<IGameCreationView>();
            var searcherMock = new Mock<IGameSearcher>();
            var presenter = new PresenterBuilder()
            {
                View = viewMock.Object,
                GameSearcher = searcherMock.Object,
            }.Build();
            
            searcherMock.Raise(x => x.CancellationApproved += null);
            
            viewMock.Verify(x => x.HideMatchmakingScreen());
        }
        
        [Test]
        public void Presenter_ShouldHideCancellationScreen_IfCancellationApproved()
        {
            var viewMock = new Mock<IGameCreationView>();
            var searcherMock = new Mock<IGameSearcher>();
            var presenter = new PresenterBuilder()
            {
                View = viewMock.Object,
                GameSearcher = searcherMock.Object,
            }.Build();
            
            searcherMock.Raise(x => x.CancellationApproved += null);
            
            viewMock.Verify(x => x.HideCancellationScreen());
        }
        
        [Test]
        public void Presenter_ShouldShowFailOnce_IfSearchFailed()
        {
            var viewMock = new Mock<IGameCreationView>();
            var searcherMock = new Mock<IGameSearcher>();
            var presenter = new PresenterBuilder()
            {
                View = viewMock.Object,
                GameSearcher = searcherMock.Object,
            }.Build();
            
            searcherMock.Raise(x => x.SearchFailed += null, It.IsAny<GameSearchFailReason>());
            
            viewMock.Verify(x => x.ShowFail(
                It.IsAny<GameSearchFailReason>()));
        }

        [Test]
        [TestCaseSource(nameof(GetReasons))]
        public void Presenter_ShouldPassFailReason_ToView_IfSearchFailed(GameSearchFailReason reason)
        {
            var viewMock = new Mock<IGameCreationView>();
            var searcherMock = new Mock<IGameSearcher>();
            var presenter = new PresenterBuilder()
            {
                View = viewMock.Object,
                GameSearcher = searcherMock.Object,
            }.Build();
            
            searcherMock.Raise(x => x.SearchFailed += null, reason);
            
            viewMock.Verify(x => x.ShowFail(
                reason), Times.Once);
        }
        
        private class PresenterBuilder
        {
            public IGameCreationView View;
            public IGameSearcher GameSearcher;
            public IGameCreator GameCreator;
            public IGameCreationHandler GameCreationHandler;

            public PresenterBuilder()
            {
                View = new Mock<IGameCreationView>().Object;
                GameSearcher = new Mock<IGameSearcher>().Object;
                GameCreator = new Mock<IGameCreator>().Object;
                GameCreationHandler = new Mock<IGameCreationHandler>().Object;
            }

            public GameCreationPresenter Build()
            {
                return new GameCreationPresenter(
                    View,
                    GameSearcher,
                    GameCreator,
                    GameCreationHandler);
            }
        }

        public static IEnumerable<GameSearchFailReason> GetReasons()
        {
            var reasons = Enum.GetValues(typeof(GameSearchFailReason));
            foreach (var reason in reasons)
            {
                yield return (GameSearchFailReason) reason;
            }
        }
    }
}