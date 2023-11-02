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

namespace Tests.EditMode.NetworkManagerTests
{
    public class ServerMoveApplierTests
    {
        [TestCase(1, 2, 3, "token")]
        [TestCase(4, 5, 6, "12345")]
        [TestCase(7, 8, 9, "someToken")]
        public async Task ApplyMove_ShouldSendMessage_WithAppropriateDTO(int playerId, int x, int y, string playerToken)
        {
            var messageSender = new TestMessageSender();
            var moveData = new Mock<MoveData>(playerId, new Vector2Int(x, y)).Object;
            var applier = new ServerMoveApplier(messageSender);
            var expectedDTO = new MoveFromClientDTO(moveData, playerToken);
            
            await applier.ApplyMoveAsync(moveData, playerToken);
            var sentMessage = messageSender.SentMessage;
            sentMessage.GetByte();
            sentMessage.GetByte();
            var actualDTO = sentMessage.GetMoveFromClientDTO();
            
            Assert.AreEqual(expectedDTO, actualDTO);
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