using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentCreation.CastlesCreation;
using Src.GameplayView.CellsContent.ContentCreation.KnightsCreation;
using Src.GameplayView.CellsContent.ContentCreation.TreesCreation;

namespace Src.GameplayView.CellsContent.ContentCreation
{
    public class ContentViewProvider : IContentViewProvider, IContentVisitor<ContentView>
    {
        private readonly ITreeViewFactory _treeViewFactory;
        private readonly IKnightViewFactory _knightViewFactory;
        private readonly ICastleViewFactory _castleViewFactory;

        public ContentViewProvider(ITreeViewFactory treeViewFactory, IKnightViewFactory knightViewFactory, ICastleViewFactory castleViewFactory)
        {
            _treeViewFactory = treeViewFactory;
            _knightViewFactory = knightViewFactory;
            _castleViewFactory = castleViewFactory;
        }

        public ContentView GetContentView(Content content)
        {
            return content.Accept(this);
        }

        public ContentView VisitTree(Tree tree)
        {
            return _treeViewFactory.GetTreeView(tree);
        }

        public ContentView VisitCastle(castledice_game_logic.GameObjects.Castle castle)
        {
            return _castleViewFactory.GetCastleView(castle);
        }

        public ContentView VisitKnight(Knight knight)
        {
            return _knightViewFactory.GetKnightView(knight);
        }
    }
}