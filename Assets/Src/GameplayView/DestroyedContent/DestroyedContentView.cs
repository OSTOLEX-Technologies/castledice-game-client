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
        private readonly Dictionary<Content, GameObject> _placedDestroyedContent = new();

        public DestroyedContentView(IGrid grid, IContentVisualCreator visualCreator)
        {
            _grid = grid;
            _visualCreator = visualCreator;
        }

        public void ShowDestroyedContent(Vector2Int position, Content content)
        {
            var cell = _grid.GetCell(position);
            var visual = _visualCreator.GetVisual(content);
            cell.AddChild(visual.gameObject);
            _placedDestroyedContent.Add(content, visual.gameObject);
        }

        public void RemoveDestroyedContent(Vector2Int position, Content content)
        {
            var cell = _grid.GetCell(position);
            if (_placedDestroyedContent.TryGetValue(content, out var visualGameObject))
            {
                cell.RemoveChild(visualGameObject);
            }     
            _placedDestroyedContent.Remove(content);
        }
    }
}