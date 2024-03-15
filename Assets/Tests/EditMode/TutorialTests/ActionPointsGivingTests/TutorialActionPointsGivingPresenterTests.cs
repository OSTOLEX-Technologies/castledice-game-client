using System;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.ActionPointsGiving;
using Src.Tutorial.ActionPointsGiving;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.TutorialTests.ActionPointsGivingTests
{
    public class TutorialActionPointsGivingPresenterTests
    {
        [Test]
        public void Presenter_ShouldGiveActionPointsToCurrentPlayer_AfterTurnSwitched()
        {
            var rnd = new Random();
            var currentPlayerId = rnd.Next();
            var currentPlayer = GetPlayer(currentPlayerId);
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetCurrentPlayer()).Returns(currentPlayer);
            var presenter = new TutorialActionPointsGivingPresenterBuilder
            {
                Game = gameMock.Object
            }.Build();
            
            gameMock.Raise(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            
            gameMock.Verify(g => g.GiveActionPointsToPlayer(currentPlayerId, It.IsAny<int>()));
        }

        [Test]
        public void Presenter_ShouldGiveActionPointsOnlyOnce_EachTimeTurnSwitched()
        {
            var gameMock = GetGameMock();
            var presenter = new TutorialActionPointsGivingPresenterBuilder
            {
                Game = gameMock.Object
            }.Build();
            
            gameMock.Raise(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            
            gameMock.Verify(g => g.GiveActionPointsToPlayer(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Presenter_ShouldGiveGeneratedAmountOfActionPoints_AfterTurnSwitched()
        {
            var rnd = new Random();
            var expectedAmount = rnd.Next();
            var actionPointsGeneratorMock = new Mock<IActionPointsGenerator>();
            actionPointsGeneratorMock.Setup(a => a.GetActionPoints(It.IsAny<Player>())).Returns(expectedAmount);
            var gameMock = GetGameMock();
            var presenter = new TutorialActionPointsGivingPresenterBuilder
            {
                Game = gameMock.Object,
                ActionPointsGenerator = actionPointsGeneratorMock.Object
            }.Build();
            
            gameMock.Raise(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);

            gameMock.Verify(g => g.GiveActionPointsToPlayer(It.IsAny<int>(), expectedAmount));
        }

        [Test]
        public void Presenter_ShouldCallActionPointsGenerator_ForCurrentPlayer_AfterTurnSwitched()
        {
            var currentPlayer = GetPlayer();
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetCurrentPlayer()).Returns(currentPlayer);
            var actionPointsGeneratorMock = new Mock<IActionPointsGenerator>();
            var presenter = new TutorialActionPointsGivingPresenterBuilder
            {
                Game = gameMock.Object,
                ActionPointsGenerator = actionPointsGeneratorMock.Object
            }.Build();
            
            gameMock.Raise(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);

            actionPointsGeneratorMock.Verify(a => a.GetActionPoints(currentPlayer));
        }

        [Test]
        public void Presenter_ShouldGenerateActionPointsOnlyOnce_EachTimeTurnSwitched()
        {
            var actionPointsGeneratorMock = new Mock<IActionPointsGenerator>();
            var gameMock = GetGameMock();
            var presenter = new TutorialActionPointsGivingPresenterBuilder
            {
                Game = gameMock.Object,
                ActionPointsGenerator = actionPointsGeneratorMock.Object
            }.Build();
            
            gameMock.Raise(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            
            actionPointsGeneratorMock.Verify(a => a.GetActionPoints(It.IsAny<Player>()), Times.Once);
        }

        [Test]
        public void Presenter_ShouldShowActionPointsForPlayerOnlyOnce_EachTimeTurnSwitched()
        {
            var viewMock = new Mock<IActionPointsGivingView>();
            var gameMock = GetGameMock();
            var presenter = new TutorialActionPointsGivingPresenterBuilder
            {
                Game = gameMock.Object,
                View = viewMock.Object
            }.Build();
            
            gameMock.Raise(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            
            viewMock.Verify(v => v.ShowActionPointsForPlayer(It.IsAny<Player>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Presenter_ShouldShowActionPointsForCurrentPlayer_AfterTurnSwitched()
        {
            var currentPlayer = GetPlayer();
            var viewMock = new Mock<IActionPointsGivingView>();
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetCurrentPlayer()).Returns(currentPlayer);
            var presenter = new TutorialActionPointsGivingPresenterBuilder
            {
                Game = gameMock.Object,
                View = viewMock.Object
            }.Build();
            
            gameMock.Raise(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);

            viewMock.Verify(v => v.ShowActionPointsForPlayer(currentPlayer, It.IsAny<int>()));
        }

        [Test]
        public void Presenter_ShouldShowGeneratedAmountOfActionPoints_AfterTurnSwitched()
        {
            var rnd = new Random();
            var expectedAmount = rnd.Next();
            var viewMock = new Mock<IActionPointsGivingView>();
            var actionPointsGeneratorMock = new Mock<IActionPointsGenerator>();
            actionPointsGeneratorMock.Setup(a => a.GetActionPoints(It.IsAny<Player>())).Returns(expectedAmount);
            var gameMock = GetGameMock();
            var presenter = new TutorialActionPointsGivingPresenterBuilder
            {
                Game = gameMock.Object,
                View = viewMock.Object,
                ActionPointsGenerator = actionPointsGeneratorMock.Object
            }.Build();
            
            gameMock.Raise(g => g.TurnSwitched += null, gameMock.Object, gameMock.Object);
            
            viewMock.Verify(v => v.ShowActionPointsForPlayer(It.IsAny<Player>(), expectedAmount));
        }

        private class TutorialActionPointsGivingPresenterBuilder
        {
            public IActionPointsGivingView View { get; set; }
            public IActionPointsGenerator ActionPointsGenerator { get; set; }
            public Game Game { get; set; }

            public TutorialActionPointsGivingPresenterBuilder()
            {
                var viewMock = new Mock<IActionPointsGivingView>();
                View = viewMock.Object;
                var actionPointsGeneratorMock = new Mock<IActionPointsGenerator>();
                ActionPointsGenerator = actionPointsGeneratorMock.Object;
                var game = GetGame();
            }

            public TutorialActionPointsGivingPresenter Build()
            {
                return new TutorialActionPointsGivingPresenter(View, ActionPointsGenerator, Game);
            }
        }
    }
}