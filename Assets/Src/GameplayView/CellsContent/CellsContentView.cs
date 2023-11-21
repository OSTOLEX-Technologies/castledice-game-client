using System;
using System.Collections.Generic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Src.GameplayView.Grid;

namespace Src.GameplayView.CellsContent
{
    public class CellsContentView : ICellsContentView
    {
        private readonly IGrid _grid;
        private readonly IContentViewProvider _contentViewProvider;
        private readonly Dictionary<Content, ContentView> _contentToView = new();

        public CellsContentView(IGrid grid, IContentViewProvider contentViewProvider)
        {
            _grid = grid;
            _contentViewProvider = contentViewProvider;
        }

        public void AddViewForContent(Vector2Int position, Content content)
        {
            if (_contentToView.ContainsKey(content))
            {
                throw new InvalidOperationException("View for content already exists");
            }

            var view = _contentViewProvider.GetContentView(content);
            _grid.GetCell(position).AddChild(view.gameObject);
            _contentToView.Add(content, view);
            view.StartView();
        }

        public void RemoveViewForContent(Content content)
        {
            if (!_contentToView.ContainsKey(content))
            {
                throw new InvalidOperationException("View for content does not exist");
            }

            var view = _contentToView[content];
            _contentToView.Remove(content);
            view.DestroyView();
        }

        public void UpdateViewForContent(Content content)
        {
            if (!_contentToView.ContainsKey(content))
            {
                throw new InvalidOperationException("View for content does not exist");
            }

            _contentToView[content].UpdateView();
        }
    }
}