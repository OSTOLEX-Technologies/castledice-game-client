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
        public void AcceptMessage_ShouldInvokeInitializationSucceed_IfMessageHasDto_WithSuccessTrue()
        {
            var handler = new PlayerInitializationResultMessageAccepter();
            var message = Message.Create();
            message.AddPlayerInitializationResultDTO(new PlayerInitializationResultDTO(true));
            var initializationSucceedInvoked = false;
            handler.InitializationSucceed += (sender, args) => initializationSucceedInvoked = true;
            
            handler.AcceptMessage(message);
            
            Assert.IsTrue(initializationSucceedInvoked);
        }
        
        [Test]
        public void AcceptMessage_ShouldInvokeInitializationFailed_IfAcceptedMessageHasDto_WithSuccessFalse()
        {
            var handler = new PlayerInitializationResultMessageAccepter();
            var message = Message.Create();
            message.AddPlayerInitializationResultDTO(new PlayerInitializationResultDTO(false));
            var initializationFailedInvoked = false;
            handler.InitializationFailed += (sender, args) => initializationFailedInvoked = true;
            
            handler.AcceptMessage(message);
            
            Assert.IsTrue(initializationFailedInvoked);
        }
    }
}