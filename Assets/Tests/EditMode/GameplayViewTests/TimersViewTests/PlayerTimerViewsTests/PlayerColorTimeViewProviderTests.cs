using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.Timers.PlayerTimerViews;

namespace Tests.EditMode.GameplayViewTests.TimersViewTests.PlayerTimerViewsTests
{
    public class PlayerColorTimeViewProviderTests
    {
        [Test]
        [TestCaseSource(nameof(GetAllPlayerColors))]
        public void GetTimeView_ShouldReturnTimeView_AccordingToPlayerColor(PlayerColor color)
        {
            var player = GetPlayer();
            var redPlayerTimeView = new Mock<TimeView>().Object;
            var bluePlayerTimeView = new Mock<TimeView>().Object;
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            playerColorProviderMock.Setup(provider => provider.GetPlayerColor(player))
                .Returns(color);
            var playerColorTimeViewProvider = new PlayerColorTimeViewProvider(redPlayerTimeView, bluePlayerTimeView, playerColorProviderMock.Object);
            
            var timeView = playerColorTimeViewProvider.GetTimeView(player);
            
            switch (color)
            {
                case PlayerColor.Red:
                    Assert.AreSame(redPlayerTimeView, timeView);
                    break;
                case PlayerColor.Blue:
                    Assert.AreSame(bluePlayerTimeView, timeView);
                    break;
                default:
                    Assert.Fail();
                    break;
            }
        }
        
        public static IEnumerable<PlayerColor> GetAllPlayerColors()
        {
            var values = System.Enum.GetValues(typeof(PlayerColor));
            foreach (var value in values)
            {
                yield return (PlayerColor) value;
            }
        }
    }
}