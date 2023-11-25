using System.Collections.Generic;
using castledice_game_logic;
using NUnit.Framework;
using Src.GameplayView.PlayersNumbers;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.PlayersNumbersTests
{
    public class PlayerNumberProviderTests
    {
        [Test]
        public void GetPlayerNumber_ShouldReturnPlayerIndexPlusOne_IfPlayerIsInPlayersList()
        {
            var rnd = new System.Random();
            var expectedPlayerNumber = rnd.Next(1, 10);
            var playersList = new List<Player>();
            for (var i = 0; i < 10; i++)
            {
                playersList.Add(GetPlayer());
            }
            var player = playersList[expectedPlayerNumber - 1];
            var playerNumberProvider = new PlayerNumberProvider(playersList);
            
            var actualPlayerNumber = playerNumberProvider.GetPlayerNumber(player);
            
            Assert.AreEqual(expectedPlayerNumber, actualPlayerNumber);
        }
        
        [Test]
        public void GetPlayerNumber_ShouldThrowArgumentException_IfPlayerIsNotInPlayersList()
        {
            var playersList = new List<Player>();
            for (var i = 0; i < 10; i++)
            {
                playersList.Add(GetPlayer());
            }
            var player = GetPlayer();
            var playerNumberProvider = new PlayerNumberProvider(playersList);
            
            Assert.Throws<System.ArgumentException>(() => playerNumberProvider.GetPlayerNumber(player));
        }
    }
}