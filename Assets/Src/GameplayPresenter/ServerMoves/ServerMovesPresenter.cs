using castledice_game_data_logic.MoveConverters;
using castledice_game_data_logic.Moves;
using Src.GameplayPresenter.GameWrappers;

namespace Src.GameplayPresenter.ServerMoves
{
    public class ServerMovesPresenter : IServerMovesPresenter
    {
        private readonly ILocalMoveApplier _localMoveApplier;
        private readonly IDataToMoveConverter _converter;
        private readonly IPlayerProvider _playerProvider;

        public ServerMovesPresenter(ILocalMoveApplier localMoveApplier, IDataToMoveConverter converter, IPlayerProvider playerProvider)
        {
            _localMoveApplier = localMoveApplier;
            _converter = converter;
            _playerProvider = playerProvider;
        }

        public void MakeMove(MoveData moveData)
        {
            var move = _converter.ConvertToMove(moveData, _playerProvider.GetPlayer(moveData.PlayerId));
            _localMoveApplier.ApplyMove(move);
        }
    }
}