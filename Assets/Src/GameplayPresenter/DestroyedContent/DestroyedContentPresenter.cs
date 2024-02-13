using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.GameplayView.DestroyedContent;

namespace Src.GameplayPresenter.DestroyedContent
{
    public class DestroyedContentPresenter
    {
        private struct ContentToPosition
        {
            public Content Content { get; set; }
            public Vector2Int Position { get; set; }
        }
        
        private readonly Game _game;
        private readonly List<ContentToPosition> _destroyedContent = new();
        private readonly IDestroyedContentView _view;
        private readonly List<ContentToPosition> _shownDestroyedContent = new();

        public DestroyedContentPresenter(Game game, IDestroyedContentView view)
        {
            _game = game;
            _view = view;
            _game.TurnSwitched += OnTurnSwitched;
            _game.MoveApplied += OnMoveApplied;
            foreach (var cell in _game.GetBoard())
            {
                cell.ContentRemoved += OnContentRemoved;
            }
        }

        private void OnContentRemoved(object sender, Content content)
        {
            var cell = sender as Cell;
            var position = cell.Position;
            _destroyedContent.Add(new ContentToPosition{Content = content, Position = position});
        }

        private void OnTurnSwitched(object sender, Game game)
        {
            foreach (var contentToPosition in _destroyedContent)
            {
                if (CellOnPositionHasContent(contentToPosition.Position)) continue;
                _view.ShowDestroyedContent(contentToPosition.Position, contentToPosition.Content);
                _shownDestroyedContent.Add(contentToPosition);
            }
            _destroyedContent.Clear();
        }
        
        private bool CellOnPositionHasContent(Vector2Int position)
        {
            var cell = _game.GetBoard()[position];
            return cell.HasContent();
        }

        private void OnMoveApplied(object sender, AbstractMove move)
        {
            foreach (var contentToPosition in _shownDestroyedContent)
            {
                _view.RemoveDestroyedContent(contentToPosition.Position, contentToPosition.Content);
            }
            _shownDestroyedContent.Clear();
        }
    }
}