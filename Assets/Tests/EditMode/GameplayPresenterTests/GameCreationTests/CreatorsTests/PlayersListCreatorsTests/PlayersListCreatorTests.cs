using System;
using static Tests.Utils.ObjectCreationUtility;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using UnityEngine;
using Random = System.Random;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreatorsTests.PlayersListCreatorsTests
{
    public class PlayersListCreatorTests
    {
        [Test]
        public void GetPlayersList_ShouldCreatePlayers_ByPassingPlayerDataToPlayerCreator()
        {
            var playersCount = new Random().Next(1, 100);
            var playersData = GetPlayersDataList(playersCount);
            var expectedPlayers = GetPlayersList(playersCount);
            var playerCreatorMock = new Mock<IPlayerCreator>();
            for (int i = 0; i < playersCount; i++)
            {
                playerCreatorMock.Setup(creator => creator.GetPlayer(playersData[i])).Returns(expectedPlayers[i]);
            }
            var playersListCreator = new PlayersListCreator(playerCreatorMock.Object);
            
            var actualPlayers = playersListCreator.GetPlayersList(playersData);
            
            Assert.AreEqual(expectedPlayers, actualPlayers);
        }
    }
}