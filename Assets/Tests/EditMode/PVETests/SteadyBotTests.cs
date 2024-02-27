using System;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Src.PVE;
using Src.TimeManagement;

namespace Tests.EditMode.PVETests
{
    public class SteadyBotTests
    {
        private const string DelayMethodName = "Delay";
        private static TimeSpan[] _delayCases = {TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)};
        
        
        [Test]
        [TestCaseSource(nameof(_delayCases))]
        public async Task Delay_ShouldDelayForTime_GivenInConstructor(TimeSpan delay)
        {
            var delayerMock = new Mock<IAsyncDelayer>();
            delayerMock.Setup(d => d.Delay(It.IsAny<TimeSpan>())).Returns(Task.CompletedTask);
            var steadyBot = new SteadyBotBuilder {Delay = delay, Delayer = delayerMock.Object}.Build();
            
            await steadyBot.CallAsyncProtectedMethod(DelayMethodName);
            
            delayerMock.Verify(d => d.Delay(delay), Times.Once());
        }
    }
    
    public class SteadyBotBuilder : BotBuilder
    {
        public IAsyncDelayer Delayer = new AsyncDelayer();
        public TimeSpan Delay = TimeSpan.FromMilliseconds(100);
        
        public new SteadyBot Build()
        {
            return new SteadyBot(LocalMoveApplier, BestMoveSearcher, Game, BotPlayer, Delay, Delayer);
        }
        
        public new Mock<SteadyBot> BuildMock()
        {
            return new Mock<SteadyBot>(LocalMoveApplier, BestMoveSearcher, Game, BotPlayer, Delay, Delayer);
        }
    }
}