using System.Threading.Tasks;
using casltedice_events_logic.ClientToServer;
using casltedice_events_logic.ServerToClient;
using castledice_game_data_logic.Moves;
using castledice_game_logic.Math;
using castledice_riptide_dto_adapters.Extensions;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.ClientMoves;
using Src.NetworkingModule.Moves;
using Tests.Mocks;

namespace Tests.EditMode.NetworkingModuleTests
{
    public class ServerMoveApplierTests
    {
        [TestCase(1, 2, 3, "token")]
        [TestCase(4, 5, 6, "12345")]
        [TestCase(7, 8, 9, "someToken")]
        public async Task ApplyMove_ShouldSendMessage_WithAppropriateDTO(int playerId, int x, int y, string playerToken)
        {
            var messageSender = new TestMessageSender();
            var moveData = new UpgradeMoveData(playerId, new Vector2Int(x, y));
            var applier = new ServerMoveApplier(messageSender);
            var expectedDTO = new MoveFromClientDTO(moveData, playerToken);
            
            var applyTask = applier.ApplyMoveAsync(moveData, playerToken);
            applier.AcceptApproveMoveDTO(new ApproveMoveDTO(true));
            await applyTask;
            var sentMessage = messageSender.SentMessage;
            sentMessage.GetByte();
            sentMessage.GetByte();
            var actualDTO = sentMessage.GetMoveFromClientDTO();
            
            Assert.AreEqual(expectedDTO, actualDTO);
        }

        [Test]
        public async Task ApplyMove_ShouldSendMessage_WithMakeMoveMessageId()
        {
            var messageSender = new TestMessageSender();
            var applier = new ServerMoveApplier(messageSender);
            
            var applyTask = applier.ApplyMoveAsync(new Mock<MoveData>(1, new Vector2Int(1, 1)).Object, "sometoken");
            applier.AcceptApproveMoveDTO(new ApproveMoveDTO(true));
            await applyTask;
            var sentMessage = messageSender.SentMessage;
            var actualMessageId = sentMessage.GetByte();
            
            Assert.AreEqual((byte)ClientToServerMessageType.MakeMove, actualMessageId);
        }

        [TestCase(true, MoveApplicationResult.Applied)]
        [TestCase(false, MoveApplicationResult.Rejected)]
        public async Task ApplyMove_ShouldReturnAppropriateMoveApplicationResult_IfAcceptedApproveMoveDTO(
            bool moveApproved, MoveApplicationResult expectedResult)
        {
            var messageSender = new TestMessageSender();
            var applier = new ServerMoveApplier(messageSender);
            var approveMoveDTO = new ApproveMoveDTO(moveApproved);

            var applyMoveTask = applier.ApplyMoveAsync(new Mock<MoveData>(1, new Vector2Int(1, 1)).Object, "sometoken");
            applier.AcceptApproveMoveDTO(approveMoveDTO);
            var actualResult = await applyMoveTask;
            
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}