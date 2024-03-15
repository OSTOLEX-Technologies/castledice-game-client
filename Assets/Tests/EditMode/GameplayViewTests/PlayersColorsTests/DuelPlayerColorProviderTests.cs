using NUnit.Framework;
using Src.GameplayView.PlayersColors;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.PlayersColorsTests
{
    public class DuelPlayerColorProviderTests
    {
        [Test]
        public void GetPlayerColor_ShouldReturnBlue_IfPlayerIsLocalPlayer()
        {
            var localPlayer = GetPlayer();
            var playerColorProvider = new DuelPlayerColorProvider(localPlayer);
            
            var actualColor = playerColorProvider.GetPlayerColor(localPlayer);
            
            Assert.AreEqual(PlayerColor.Blue, actualColor);
        }
        
        [Test]
        public void GetPlayerColor_ShouldReturnRed_IfPlayerIsNotLocalPlayer()
        {
            var localPlayer = GetPlayer();
            var playerColorProvider = new DuelPlayerColorProvider(localPlayer);
            
            var actualColor = playerColorProvider.GetPlayerColor(GetPlayer());
            
            Assert.AreEqual(PlayerColor.Red, actualColor);;
        }
    }
}