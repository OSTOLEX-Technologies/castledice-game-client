using System;
using System.Collections.Generic;
using static Tests.ObjectCreationUtility;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersRotations.RotationsByOrder;
using Random = UnityEngine.Random;

namespace Tests.EditMode.GameplayViewTests.PlayersRotationsTests.RotationsByOrderTests
{
    public class PlayerOrderRotationProviderTests
    {
        public struct PlayerOrderAndPlayersCount
        {
            public int PlayerOrder { get; }
            public int PlayersCount { get; }
            
            public PlayerOrderAndPlayersCount(int playerOrder, int playersCount)
            {
                PlayerOrder = playerOrder;
                PlayersCount = playersCount;
            }
        }
        
        [Test]
        [TestCaseSource(nameof(GetOrdersAndCounts))]
        public void GetRotation_ShouldReturnRotation_AccordingToConfig(PlayerOrderAndPlayersCount data)
        {
            var playerOrder = data.PlayerOrder;
            var playersCount = data.PlayersCount;
            var playersList = new List<Player>();
            for (var i = 0; i < playersCount; i++)
            {
                playersList.Add(GetPlayer());
            }
            var player = playersList[playerOrder - 1];
            var configMock = new Mock<IPlayerOrderRotationConfig>();
            var expectedRotation = Random.insideUnitSphere;
            configMock.Setup(config => config.GetRotation(playerOrder)).Returns(expectedRotation);
            var playerRotationProvider = new PlayerOrderRotationProvider(configMock.Object, playersList);
            
            var rotation = playerRotationProvider.GetRotation(player);
            
            Assert.AreEqual(expectedRotation, rotation);
        }
        
        public static IEnumerable<PlayerOrderAndPlayersCount> GetOrdersAndCounts()
        {
            var colors = Enum.GetValues(typeof(PlayerColor));
            var orderNumber = 1;
            foreach (var color in colors)
            {
                yield return new PlayerOrderAndPlayersCount(orderNumber, colors.Length);
                orderNumber++;
            }
        }
    }
}