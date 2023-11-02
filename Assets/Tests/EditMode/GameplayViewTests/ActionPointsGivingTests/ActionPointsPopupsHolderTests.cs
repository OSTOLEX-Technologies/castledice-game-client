using Moq;
using NUnit.Framework;
using Src.GameplayView.ActionPointsGiving;
using Src.GameplayView.CellsContent.ContentCreation;

namespace Tests.EditMode.GameplayViewTests.ActionPointsGivingTests
{
    public class ActionPointsPopupsHolderTests
    {
        [Test]
        public void GetPopup_ShouldReturnGivenBluePopup_IfGivenBluePlayerColor()
        {
            var bluePopup = new Mock<IActionPointsPopup>().Object;
            var redPopup = new Mock<IActionPointsPopup>().Object;
            var holder = new ActionPointsPopupsHolder(bluePopup, redPopup);
            
            var popup = holder.GetPopup(PlayerColor.Blue);
            
            Assert.AreSame(bluePopup, popup);
        }
        
        [Test]
        public void GetPopup_ShouldReturnGivenRedPopup_IfGivenRedPlayerColor()
        {
            var bluePopup = new Mock<IActionPointsPopup>().Object;
            var redPopup = new Mock<IActionPointsPopup>().Object;
            var holder = new ActionPointsPopupsHolder(bluePopup, redPopup);
            
            var popup = holder.GetPopup(PlayerColor.Red);
            
            Assert.AreSame(redPopup, popup);
        }
    }
}