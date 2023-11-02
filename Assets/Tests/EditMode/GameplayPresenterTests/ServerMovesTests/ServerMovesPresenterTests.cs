using castledice_game_data_logic.MoveConverters;
using castledice_game_data_logic.Moves;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using static Tests.ObjectCreationUtility;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayPresenter.ServerMoves;

namespace Tests.EditMode.GameplayPresenterTests.ServerMovesTests
{
    public class ServerMovesPresenterTests
    {
        [TestCase(1)]
        [TestCase(53)]
        [TestCase(2784)]
        public void MakeMove_ShouldConvertGivenMoveDataToMove_AndApplyItViaGivenApplier(int playerId)
        {
            var player = GetPlayer(playerId);
            var moveData = new Mock<MoveData>(playerId, new Vector2Int(1, 1)).Object;
            var move = new Mock<AbstractMove>(player, new Vector2Int(1, 1)).Object;
            var converterMock = new Mock<IDataToMoveConverter>();
            converterMock.Setup(converter => converter.ConvertToMove(moveData, player)).Returns(move);
            var playerProviderMock = new Mock<IPlayerProvider>();
            playerProviderMock.Setup(provider => provider.GetPlayer(moveData.PlayerId)).Returns(player);
            var applierMock = new Mock<ILocalMoveApplier>();
            var presenter = new ServerMovesPresenter(applierMock.Object, converterMock.Object, playerProviderMock.Object);
            
            presenter.MakeMove(moveData);
            
            applierMock.Verify(applier => applier.ApplyMove(move), Times.Once);
        }
    }
}