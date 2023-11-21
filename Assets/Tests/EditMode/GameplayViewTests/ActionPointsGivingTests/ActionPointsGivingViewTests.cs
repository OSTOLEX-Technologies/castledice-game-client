using Moq;
using NUnit.Framework;
using Src.GameplayView.ActionPointsGiving;
using Src.GameplayView.CellsContent.ContentCreation;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.ActionPointsGivingTests
{
    public class ActionPointsGivingViewTests
    {
        [Test, Combinatorial]
        public void ShowActionPointsForPlayer_ShouldPassGivenAmountAndPlayerColorFromProvider_ToGivenPopupDemonstrator([Values(1, 2, 3, 4, 5, 6)]int amount, 
            [Values(PlayerColor.Blue, PlayerColor.Red)]PlayerColor playerColor)
        {
            var player = GetPlayer();
            var popupDemonstratorMock = new Mock<IActionPointsPopupDemonstrator>();
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            playerColorProviderMock.Setup(provider => provider.GetPlayerColor(player)).Returns(playerColor);
            var view = new ActionPointsGivingView(playerColorProviderMock.Object, popupDemonstratorMock.Object);
            
            view.ShowActionPointsForPlayer(player, amount);
            
            popupDemonstratorMock.Verify(demonstrator => demonstrator.ShowActionPointsPopup(playerColor, amount));
        }
    }
}