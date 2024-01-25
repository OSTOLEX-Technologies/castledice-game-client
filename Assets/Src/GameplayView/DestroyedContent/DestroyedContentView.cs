using System.Collections.Generic;
using castledice_game_logic.GameObjects;
using Src.GameplayView.ContentVisuals.VisualsCreation;
using Src.GameplayView.Grid;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayView.DestroyedContent
{
    public class DestroyedContentView : IDestroyedContentView
    {
        private readonly IGrid _grid;
        private readonly IContentVisualCreator _visualCreator;
        private readonly ITransparencyConfig _transparencyConfig;
        private readonly Dictionary<Content, GameObject> _shownDestroyedContentGameObjects = new();

        public DestroyedContentView(IGrid grid, IContentVisualCreator visualCreator, ITransparencyConfig transparencyConfig)
        {
            _grid = grid;
            _visualCreator = visualCreator;
            _transparencyConfig = transparencyConfig;
        }

        public void ShowDestroyedContent(Vector2Int position, Content content)
        {
            var cell = _grid.GetCell(position);
            var visual = _visualCreator.GetVisual(content);
            visual.SetTransparency(_transparencyConfig.GetTransparency());
            cell.AddChild(visual.gameObject);
            _shownDestroyedContentGameObjects.Add(content, visual.gameObject);
        }

        public void RemoveDestroyedContent(Vector2Int position, Content content)
        {
            var cell = _grid.GetCell(position);
            if (_shownDestroyedContentGameObjects.TryGetValue(content, out var visualGameObject))
            {
                cell.RemoveChild(visualGameObject);
            }     
            _shownDestroyedContentGameObjects.Remove(content);
        }
    }
}