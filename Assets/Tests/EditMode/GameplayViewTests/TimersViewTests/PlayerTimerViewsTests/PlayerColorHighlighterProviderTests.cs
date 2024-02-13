using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.Timers.PlayerTimerViews;

namespace Tests.EditMode.GameplayViewTests.TimersViewTests.PlayerTimerViewsTests
{
    public class PlayerColorHighlighterProviderTests
    {
        [Test]
        [TestCaseSource(nameof(GetAllPlayerColors))]
        public void GetHighlighter_ShouldReturnHighlighter_AccordingToPlayerColor(PlayerColor color)
        {
            var player = GetPlayer();
            var redPlayerHighlighter = new Mock<Highlighter>().Object;
            var bluePlayerHighlighter = new Mock<Highlighter>().Object;
            var playerColorProviderMock = new Mock<IPlayerColorProvider>();
            playerColorProviderMock.Setup(provider => provider.GetPlayerColor(player))
                .Returns(color);
            var playerColorHighlighterProvider = new PlayerColorHighlighterProvider(redPlayerHighlighter, bluePlayerHighlighter, playerColorProviderMock.Object);
            
            var highlighter = playerColorHighlighterProvider.GetHighlighter(player);

            switch (color)
            {
                case PlayerColor.Red:
                    Assert.AreSame(redPlayerHighlighter, highlighter);
                    break;
                case PlayerColor.Blue:
                    Assert.AreSame(bluePlayerHighlighter, highlighter);
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