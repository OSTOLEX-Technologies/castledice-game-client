namespace Src.GameplayView.Highlights
{
    public class ColoredHighlightCreator : IColoredHighlightCreator
    {
        private readonly IColoredHighlightPrefabProvider _coloredHighlightPrefabProvider;
        private readonly IInstantiator _instantiator;

        public ColoredHighlightCreator(IColoredHighlightPrefabProvider coloredHighlightPrefabProvider, IInstantiator instantiator)
        {
            _coloredHighlightPrefabProvider = coloredHighlightPrefabProvider;
            _instantiator = instantiator;
        }

        public ColoredHighlight GetHighlight()
        {
            var prefab = _coloredHighlightPrefabProvider.GetHighlightPrefab();
            var highlight = _instantiator.Instantiate(prefab);
            return highlight;
        }
    }
}