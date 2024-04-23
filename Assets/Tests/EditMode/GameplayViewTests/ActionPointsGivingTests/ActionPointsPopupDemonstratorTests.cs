using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.GameplayView.ActionPointsGiving;
using Src.GameplayView.PlayersColors;

namespace Tests.EditMode.GameplayViewTests.ActionPointsGivingTests
{
    public class ActionPointsPopupDemonstratorTests
    {
        [Test, Combinatorial]
        public void ShowActionPointsPopup_ShouldGetPopupFromProvider_AndShowItWithGivenAmount([Values(1, 2, 3, 4, 5, 6)]int amount, 
            [Values(PlayerColor.Blue, PlayerColor.Red)]PlayerColor playerColor)
        {
            var popupProviderMock = new Mock<IActionPointsPopupsProvider>();
            var popupMock = new Mock<IActionPointsPopup>();
            popupProviderMock.Setup(provider => provider.GetPopup(playerColor)).Returns(popupMock.Object);
            var popupDemonstrator = new ActionPointsPopupDemonstrator(popupProviderMock.Object);
            
            popupDemonstrator.ShowActionPointsPopup(playerColor, amount);
            
            popupMock.Verify(popup => popup.SetAmount(amount), Times.Once);
            popupMock.Verify(popup => popup.Show(), Times.Once);
            popupMock.Verify(popup => popup.Hide(), Times.Never);
        }
    }
}