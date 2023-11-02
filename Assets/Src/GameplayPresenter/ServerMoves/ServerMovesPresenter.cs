using castledice_game_data_logic.MoveConverters;
using castledice_game_data_logic.Moves;
using Src.GameplayPresenter.ClientMoves;
using Src.GameplayPresenter.GameWrappers;

namespace Src.GameplayPresenter.ServerMoves
{
    public class ServerMovesPresenter : IServerMovesPresenter
    {
        private readonly ILocalMoveApplier _localMoveApplier;
        private readonly IDataToMoveConverter _converter;

        public ServerMovesPresenter(ILocalMoveApplier localMoveApplier, IDataToMoveConverter converter)
        {
            _localMoveApplier = localMoveApplier;
            _converter = converter;
        }

        public void MakeMove(MoveData moveData)
        {
            throw new System.NotImplementedException();
        }
    }
}