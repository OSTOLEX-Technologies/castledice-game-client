using System;
using System.Collections.Generic;
using castledice_game_logic;
using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentCreation;
using Src.GameplayView.PlayersColor;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.PlayersColorTests
{
    public class DuelPlayerColorProviderTests
    {
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(15)]
        public void Constructor_ShouldThrowArgumentException_IfGivenPlayersListCountIsOtherThanTwo(int playersCount)
        {
            var playersList = new List<Player>();
            for (var i = 0; i < playersCount; i++)
            {
                playersList.Add(GetPlayer(i));
            }
            
            Assert.Throws<ArgumentException>(() => new DuelPlayerColorProvider(playersList));
        }

        [Test]
        public void GetPlayerColor_ShouldReturnBlue_IfPlayerIsFirstInTheList()
        {
            var firstPlayer = GetPlayer(1);
            var playersList = new List<Player>{firstPlayer, GetPlayer(2)};
            var playerColorProvider = new DuelPlayerColorProvider(playersList);
            
            var actualColor = playerColorProvider.GetPlayerColor(firstPlayer);
            
            Assert.AreEqual(PlayerColor.Blue, actualColor);
        }
        
        [Test]
        public void GetPlayerColor_ShouldReturnRed_IfPlayerIsSecondInTheList()
        {
            var secondPlayer = GetPlayer(2);
            var playersList = new List<Player>{GetPlayer(1), secondPlayer};
            var playerColorProvider = new DuelPlayerColorProvider(playersList);
            
            var actualColor = playerColorProvider.GetPlayerColor(secondPlayer);
            
            Assert.AreEqual(PlayerColor.Red, actualColor);
        }
        
        [Test]
        public void GetPlayerColor_ShouldThrowArgumentException_IfPlayerIsNotInTheList()
        {
            var playersList = new List<Player>{GetPlayer(1), GetPlayer(2)};
            var playerColorProvider = new DuelPlayerColorProvider(playersList);
            
            Assert.Throws<ArgumentException>(() => playerColorProvider.GetPlayerColor(GetPlayer(3)));
        }
    }
}