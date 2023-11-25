using Moq;
using NUnit.Framework;
using Src.GameplayPresenter;
using Src.GameplayView.PlayersColors;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.PlayersColorsTests
{
    public class DuelPlayerColorProviderTests
    {
        [Test]
        public void GetPlayerColor_ShouldReturnBlue_IfPlayerIdIsEqualToIdFromProvider([Values(1, 2, 3, 1234, 432)]int playerId)
        {
            var firstPlayer = GetPlayer(playerId);
            var playerDataProvider = new Mock<IPlayerDataProvider>();
            playerDataProvider.Setup(provider => provider.GetId()).Returns(playerId);
            var playerColorProvider = new DuelPlayerColorProvider(playerDataProvider.Object);
            
            var actualColor = playerColorProvider.GetPlayerColor(firstPlayer);
            
            Assert.AreEqual(PlayerColor.Blue, actualColor);
        }
        
        [Test]
        public void GetPlayerColor_ShouldReturnRed_IfPlayerIdIsNotEqualToIdFromProvider([Values(1, 2, 3, 1234, 432)]int playerId)
        {
            var firstPlayer = GetPlayer(playerId);
            var playerDataProvider = new Mock<IPlayerDataProvider>();
            playerDataProvider.Setup(provider => provider.GetId()).Returns(playerId + 1);
            var playerColorProvider = new DuelPlayerColorProvider(playerDataProvider.Object);
            
            var actualColor = playerColorProvider.GetPlayerColor(firstPlayer);
            
            Assert.AreEqual(PlayerColor.Red, actualColor);
        }
    }
}