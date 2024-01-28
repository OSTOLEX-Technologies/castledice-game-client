using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.GameplayView.NewUnitsHighlights;

namespace Src.GameplayPresenter.NewUnitsHighlights
{
    public class NewUnitsHighlightsPresenter
    {
        private readonly Game _game;
        private readonly List<Vector2Int> _newUnitsPositions = new();
        private readonly INewUnitsHighlightsView _view;

        public NewUnitsHighlightsPresenter(Game game, INewUnitsHighlightsView view)
        {
            _game = game;
            _view = view;
            _game.TurnSwitched += OnTurnSwitched;
            _game.MoveApplied += OnMoveApplied;
            foreach (var cell in _game.GetBoard())
            {
                cell.ContentAdded += OnContentAdded;
            }
        }

        private void OnContentAdded(object sender, Content content)
        {
            if (content is IPlayerOwned)
            {
                _newUnitsPositions.Add(((Cell)sender).Position);
            }
        }

        private void OnTurnSwitched(object sender, Game game)
        {
            foreach (var pos in _newUnitsPositions)
            {
                _view.ShowHighlight(pos, game.GetPreviousPlayer());
            }
            _newUnitsPositions.Clear();
        }
        
        private void OnMoveApplied(object sender, AbstractMove move)
        {
            _view.HideHighlights();
        }
        
        ~NewUnitsHighlightsPresenter()
        {
            _game.TurnSwitched -= OnTurnSwitched;
            _game.MoveApplied -= OnMoveApplied;
            foreach (var cell in _game.GetBoard())
            {
                cell.ContentAdded -= OnContentAdded;
            }
        }
    }
}