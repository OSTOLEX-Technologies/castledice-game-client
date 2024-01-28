using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using Src.GameplayView.Grid;
using Src.GameplayView.Highlights;
using Src.GameplayView.PlayerObjectsColor;

namespace Src.GameplayView.NewUnitsHighlights
{
    public class NewUnitsHighlightsView : INewUnitsHighlightsView
    {
        private readonly IPlayerObjectsColorProvider _colorProvider;
        private readonly Dictionary<Vector2Int, ColoredHighlight> _highlights = new();

        public NewUnitsHighlightsView(IGrid grid, IColoredHighlightCreator highlightCreator, IPlayerObjectsColorProvider colorProvider)
        {
            _colorProvider = colorProvider;
            foreach (var cell in grid)
            {
                var highlight = highlightCreator.GetHighlight();
                var position = cell.Position;
                cell.AddChild(highlight.gameObject);
                _highlights.Add(position, highlight);
                highlight.Hide();
            }
        }

        public void ShowHighlight(Vector2Int position, Player player)
        {
            if (!_highlights.ContainsKey(position))
            {
                throw new InvalidOperationException("No highlight on given position: " + position);
            }

            var color = _colorProvider.GetColor(player);
            var highlight = _highlights[position];
            highlight.Show();
            highlight.SetColor(color);
        }

        public void HideHighlights()
        {
            foreach (var highlight in _highlights.Values)
            {
                highlight.Hide();
            }
        }
    }
}