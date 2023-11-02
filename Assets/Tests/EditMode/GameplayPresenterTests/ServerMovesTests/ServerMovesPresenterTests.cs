using castledice_game_data_logic.MoveConverters;
using castledice_game_data_logic.Moves;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using static Tests.ObjectCreationUtility;
using Moq;

namespace Tests.EditMode.GameplayPresenterTests.ServerMovesTests
{
    public class ServerMovesPresenterTests
    {
        public void MakeMove_ShouldConvertGivenMoveDataToMove_AndApplyItViaGivenApplier()
        {
            var player = GetPlayer(1);
            var moveData = new Mock<MoveData>(1, new Vector2Int(1, 1));
            var move = new Mock<AbstractMove>(player, new Vector2Int(1, 1));
            var converterMock = new Mock<IDataToMoveConverter>();
            converterMock.Setup(converter => converter.ConvertToMove(moveData.Object, player)).Returns(move.Object);
        }
    }
}