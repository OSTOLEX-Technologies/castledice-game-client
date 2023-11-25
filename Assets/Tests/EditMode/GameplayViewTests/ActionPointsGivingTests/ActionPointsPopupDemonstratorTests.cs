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
            var popupDemonstrator = new ActionPointsPopupDemonstrator(popupProviderMock.Object, 100);
            
            popupDemonstrator.ShowActionPointsPopup(playerColor, amount);
            
            popupMock.Verify(popup => popup.SetAmount(amount), Times.Once);
            popupMock.Verify(popup => popup.Show(), Times.Once);
            popupMock.Verify(popup => popup.Hide(), Times.Never);
        }
        
        [TestCase(1000)]
        [TestCase(2000)]
        [TestCase(3000)]
        //This test checks if the popup is hidden after given time with 100 milliseconds threshold.
        public async Task Demonstrator_ShouldHidePopupAfterGivenTime(int timeMilliseconds)
        {
            var popupProviderMock = new Mock<IActionPointsPopupsProvider>();
            var popupMock = new Mock<IActionPointsPopup>();
            popupProviderMock.Setup(provider => provider.GetPopup(It.IsAny<PlayerColor>())).Returns(popupMock.Object);
            var popupDemonstrator = new ActionPointsPopupDemonstrator(popupProviderMock.Object, timeMilliseconds);
            
            popupDemonstrator.ShowActionPointsPopup(It.IsAny<PlayerColor>(), It.IsAny<int>());
            await Task.Delay(timeMilliseconds - 100);
            popupMock.Verify(popup => popup.Hide(), Times.Never); //Verifying that popup is not hidden before given time.
            await Task.Delay(200);
            popupMock.Verify(popup => popup.Hide(), Times.Once); //Verifying that popup is hidden after given time.
        }
    }
}