using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using Src.GameplayView.Grid;
using Src.GameplayView.PlayerObjectsColor;

namespace Src.GameplayView.UnitsUnderlines
{
    public class UnitsUnderlinesView : IUnitsUnderlinesView
    {
        private readonly IPlayerObjectsColorProvider _colorProvider;
        private readonly Dictionary<Vector2Int, Underline> _underlines = new();

        public UnitsUnderlinesView(IGrid grid, IUnderlineCreator underlineCreator, IPlayerObjectsColorProvider colorProvider)
        {
            _colorProvider = colorProvider;
            foreach (var cell in grid)
            {
                var underline = underlineCreator.GetUnderline();
                var position = cell.Position;
                cell.AddChild(underline.gameObject);
                underline.Hide();
                _underlines.Add(position, underline);
            }
        }

        public void ShowUnderline(Vector2Int position, Player player)
        {
            if (!_underlines.TryGetValue(position, out var underline)) return;
            underline.Show();
            underline.SetColor(_colorProvider.GetColor(player));
        }

        public void HideUnderline(Vector2Int position)
        {
            if (!_underlines.TryGetValue(position, out var underline)) return;
            underline.Hide();
        }
    }
}