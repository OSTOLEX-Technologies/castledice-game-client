using CastleGO = castledice_game_logic.GameObjects.Castle;
using Src.GameplayView.ContentVisuals.ContentColor;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation
{
    public class CastleVisualCreator : ICastleVisualCreator
    {
        private readonly ICastleVisualPrefabProvider _prefabProvider;
        private readonly IPlayerContentColorProvider _colorProvider;
        private readonly IInstantiator _instantiator;

        public CastleVisualCreator(ICastleVisualPrefabProvider prefabProvider, IPlayerContentColorProvider colorProvider, IInstantiator instantiator)
        {
            _prefabProvider = prefabProvider;
            _colorProvider = colorProvider;
            _instantiator = instantiator;
        }

        public CastleVisual GetCastleVisual(CastleGO castle)
        {
            var prefab = _prefabProvider.GetCastleVisualPrefab();
            var visual = _instantiator.Instantiate(prefab);
            var color = _colorProvider.GetContentColor(castle.GetOwner());
            visual.Color = color;
            return visual;
        }
    }
}