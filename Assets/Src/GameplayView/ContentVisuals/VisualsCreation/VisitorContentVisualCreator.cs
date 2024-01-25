using CastleGO = castledice_game_logic.GameObjects.Castle;

using castledice_game_logic.GameObjects;
using Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation;
using Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation;
using Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation;

namespace Src.GameplayView.ContentVisuals.VisualsCreation
{
    public class VisitorContentVisualCreator : IContentVisualCreator, IContentVisitor<ContentVisual>
    {
        private readonly IKnightVisualCreator _knightVisualCreator;
        private readonly ITreeVisualCreator _treeVisualCreator;
        private readonly ICastleVisualCreator _castleVisualCreator;

        public VisitorContentVisualCreator(IKnightVisualCreator knightVisualCreator, ITreeVisualCreator treeVisualCreator, ICastleVisualCreator castleVisualCreator)
        {
            _knightVisualCreator = knightVisualCreator;
            _treeVisualCreator = treeVisualCreator;
            _castleVisualCreator = castleVisualCreator;
        }

        public ContentVisual GetVisual(Content content)
        {
            return content.Accept(this);
        }

        public ContentVisual VisitTree(Tree tree)
        {
            return _treeVisualCreator.GetTreeVisual(tree);
        }

        public ContentVisual VisitCastle(CastleGO castle)
        {
            return _castleVisualCreator.GetCastleVisual(castle);
        }

        public ContentVisual VisitKnight(Knight knight)
        {
            return _knightVisualCreator.GetKnightVisual(knight);
        }
    }
}