using static Tests.Utils.ObjectCreationUtility;
using System;
using castledice_events_logic.ServerToClient;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.Timers;
using Src.NetworkingModule;

namespace Tests.EditMode.NetworkingModuleTests
{
    public class SwitchTimerAccepterTests
    {
        [Test]
        public void AcceptSwitchTimerDTO_ShouldPassDTOData_ToPresenter()
        {
            var playerId = new Random().Next();
            var timeLeft = GetRandomTimeSpan();
            var switchTo = new Random().Next() % 2 == 0;
            var dto = new SwitchTimerDTO(timeLeft, playerId, switchTo);
            var presenterMock = new Mock<ITimersPresenter>();
            var accepter = new SwitchTimerAccepter(presenterMock.Object);
            
            accepter.AcceptSwitchTimerDTO(dto);
            
            presenterMock.Verify(p => p.SwitchTimerForPlayer(playerId, timeLeft, switchTo), Times.Once);
        }
    }
}