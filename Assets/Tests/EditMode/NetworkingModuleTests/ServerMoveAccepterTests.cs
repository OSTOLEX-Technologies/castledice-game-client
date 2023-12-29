using castledice_events_logic.ServerToClient;
using castledice_game_data_logic.Moves;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.ServerMoves;
using Src.NetworkingModule.Moves;

namespace Tests.EditMode.NetworkingModuleTests
{
    public class ServerMoveAccepterTests
    {
        [Test]
        public void AcceptMoveFromServerDTO_ShouldPassMoveDatFromDTO_ToGivenPresenter()
        {
            var moveData = new Mock<MoveData>(1, new Vector2Int(1, 1)).Object;
            var dto = new MoveFromServerDTO(moveData);
            var presenterMock = new Mock<IServerMovesPresenter>();
            var accepter = new ServerMoveAccepter(presenterMock.Object);
            
            accepter.AcceptMoveFromServerDTO(dto);
            
            presenterMock.Verify(presenter => presenter.MakeMove(moveData), Times.Once);
        }
    }
}