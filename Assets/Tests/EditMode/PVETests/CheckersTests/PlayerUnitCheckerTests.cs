using NUnit.Framework;
using Src.PVE.Checkers;
using static Tests.Utils.ContentMocksCreationUtility;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.CheckersTests
{
    public class PlayerUnitCheckerTests
    {
        [Test]
        public void IsPlayerUnit_ShouldReturnFalse_IfContentIsNotPlayerOwned()
        {
            var content = GetContent();
            var player = GetPlayer();
            var playerUnitChecker = new PlayerUnitChecker();
            
            var result = playerUnitChecker.IsPlayerUnit(content, player);
            
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPlayerUnit_ShouldReturnFalse_IfContentIsNotReplaceable()
        {
            var playerOwnedContent = GetPlayerOwnedContent();
            var player = GetPlayer();
            var playerUnitChecker = new PlayerUnitChecker();
            
            var result = playerUnitChecker.IsPlayerUnit(playerOwnedContent, player);
            
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPlayerUnit_ShouldReturnFalse_IfContentReplaceableButNotPlayerOwned()
        {
            var replaceableContent = GetReplaceableContent(1);
            var player = GetPlayer();
            var playerUnitChecker = new PlayerUnitChecker();
            
            var result = playerUnitChecker.IsPlayerUnit(replaceableContent, player);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void IsPlayerUnit_ShouldReturnTrue_IfContentIsPlayerOwnedAndReplaceable()
        {
            var player = GetPlayer();
            var playerOwnedReplaceableContent = GetReplaceablePlayerOwnedContent(player, 1);
            var playerUnitChecker = new PlayerUnitChecker();
            
            var result = playerUnitChecker.IsPlayerUnit(playerOwnedReplaceableContent, player);
            
            Assert.IsTrue(result);
        }
    }
}