using castledice_game_logic.GameObjects;
using Moq;
using NUnit.Framework;
using Src.PVE.Checkers;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.CheckersTests
{
    public class PlayerBaseCheckerTests
    {
        [Test]
        public void IsPlayerBase_ShouldReturnFalse_IfContentIsNotCastle()
        {
            var content = new Mock<Content>();
            var checker = new PlayerBaseChecker();
            
            var isPlayerBase = checker.IsPlayerBase(content.Object, GetPlayer());
            
            Assert.False(isPlayerBase);
        }

        [Test]
        public void IsPlayerBase_ShouldReturnFalse_IfContentIsCastleOwnedByOtherPlayer()
        {
            var owner = GetPlayer();
            var content = GetCastle(owner);
            var checker = new PlayerBaseChecker();
            
            var isPlayerBase = checker.IsPlayerBase(content, GetPlayer());
            
            Assert.False(isPlayerBase);
        }
        
        [Test]
        public void IsPlayerBase_ShouldReturnTrue_IfContentIsCastleOwnedByPlayer()
        {
            var owner = GetPlayer();
            var content = GetCastle(owner);
            var checker = new PlayerBaseChecker();
            
            var isPlayerBase = checker.IsPlayerBase(content, owner);
            
            Assert.True(isPlayerBase);
        }
    }
}