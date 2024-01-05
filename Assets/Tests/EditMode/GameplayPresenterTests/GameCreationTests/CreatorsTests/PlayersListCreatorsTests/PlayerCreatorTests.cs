using System;
using castledice_game_logic.Time;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreatorsTests.PlayersListCreatorsTests
{
    public class PlayerCreatorTests
    {
        [Test]
        public void CreatedPlayer_ShouldHaveId_FromPlayerData([Random(1, 100, 10)]int id)
        {
            //var id = new Random().Next();
            var playerData = GetPlayerData(id);
            var playerCreator = new PlayerCreator(new Mock<IPlayerTimerCreator>().Object);
            
            var player = playerCreator.GetPlayer(playerData);
            
            Assert.AreEqual(id, player.Id);
        }

        [Test]
        public void CreatedPlayer_ShouldHaveZeroActionPoints()
        {
            var playerData = GetPlayerData();
            var playerCreator = new PlayerCreator(new Mock<IPlayerTimerCreator>().Object);
            
            var player = playerCreator.GetPlayer(playerData);
            
            Assert.AreEqual(0, player.ActionPoints.Amount);
        }
        
        [Test]
        public void CreatedPlayer_ShouldHaveAvailablePlacementsList_FromPlayerData()
        {
            var playerData = GetPlayerData();
            var playerCreator = new PlayerCreator(new Mock<IPlayerTimerCreator>().Object);
            
            var player = playerCreator.GetPlayer(playerData);
            
            Assert.AreSame(playerData.AvailablePlacements, player.Deck);
        }
        
        [Test]
        public void CreatedPlayer_ShouldHavePlayerTimer_FromPlayerTimerCreator()
        {
            var playerData = GetPlayerData();
            var expectedPlayerTimer = new Mock<IPlayerTimer>().Object;
            var playerTimerCreatorMock = new Mock<IPlayerTimerCreator>();
            playerTimerCreatorMock.Setup(creator => creator.GetPlayerTimer(playerData.TimeSpan)).Returns(expectedPlayerTimer);
            var playerCreator = new PlayerCreator(playerTimerCreatorMock.Object);
            
            var player = playerCreator.GetPlayer(playerData);
            
            Assert.AreSame(expectedPlayerTimer, player.Timer);
        }
    }
}