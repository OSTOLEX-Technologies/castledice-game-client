using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Src.GameplayView.CellMovesHighlights;

namespace Src.GameplayPresenter.CellMovesHighlights
{
    public class CellMovesHighlightPresenter
    {
        private IPlayerDataProvider _playerDataProvider;
        private ICellMovesListProvider _cellMovesListProvider;
        private Game _game;
        private ICellMovesHighlightView _view;

        public CellMovesHighlightPresenter(IPlayerDataProvider playerDataProvider, ICellMovesListProvider cellMovesListProvider, Game game, ICellMovesHighlightView view)
        {
            _playerDataProvider = playerDataProvider;
            _cellMovesListProvider = cellMovesListProvider;
            _game = game;
            _view = view;
        }

        public void HighlightCellMove()
        {
            
        }

        private void OnMoveApplied(object sender, AbstractMove move)
        {
            
        }

        private void OnTurnSwitched(object sender, Game game)
        {
            
        }
    }
}