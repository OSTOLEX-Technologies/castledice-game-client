using castledice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using NUnit.Framework;
using Riptide;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;

namespace Tests.EditMode.GameplayPresenterTests.PlayerInitializationTests
{
    public class PlayerInitializationResultMessageHandlerTests
    {
        
        [Test]
        public void AcceptDto_ShouldInvokeInitializationSucceed_IfIsSuccessfulIsTrue()
        {
            var handler = new PlayerInitializationResultDtoAccepter();
            var dto = new PlayerInitializationResultDTO(true);
            var initializationSucceedInvoked = false;
            handler.InitializationSucceed += (sender, args) => initializationSucceedInvoked = true;
            
            handler.AcceptDto(dto);
            
            Assert.IsTrue(initializationSucceedInvoked);
        }
        
        [Test]
        public void AcceptDto_ShouldInvokeInitializationFailed_IfIsSuccessfulIsFalse()
        {
            var handler = new PlayerInitializationResultDtoAccepter();
            var dto = new PlayerInitializationResultDTO(false);
            var initializationFailedInvoked = false;
            handler.InitializationFailed += (sender, args) => initializationFailedInvoked = true;
            
            handler.AcceptDto(dto);
            
            Assert.IsTrue(initializationFailedInvoked);
        }
    }
}