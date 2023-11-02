using casltedice_events_logic.ServerToClient;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.ActionPointsGiving;
using Src.NetworkingModule;

namespace Tests.EditMode.NetworkManagerTests
{
    public class GiveActionPointsAccepterTests
    {
        [TestCase(1, 6)]
        [TestCase(2, 3)]
        [TestCase(3, 4)]
        public void AcceptGiveActionPointsDTO_ShouldPassIdAndAmountFromDTO_ToGivenPresenter(int playerId, int amount)
        {
            var presenterMock = new Mock<IActionPointsGivingPresenter>();
            var dto = new GiveActionPointsDTO(playerId, amount);
            var accepter = new GiveActionPointsAccepter(presenterMock.Object);
            
            accepter.AcceptGiveActionPointsDTO(dto);
            
            presenterMock.Verify(p => p.GiveActionPoints(playerId, amount), Times.Once);
        }
    }
}