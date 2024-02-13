using CastleGO = castledice_game_logic.GameObjects.Castle;
using Src.GameplayView.PlayerObjectsColor;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation
{
    public class CastleVisualCreator : ICastleVisualCreator
    {
        private readonly ICastleVisualPrefabProvider _prefabProvider;
        private readonly IPlayerObjectsColorProvider _colorProvider;
        private readonly IInstantiator _instantiator;

        public CastleVisualCreator(ICastleVisualPrefabProvider prefabProvider, IPlayerObjectsColorProvider colorProvider, IInstantiator instantiator)
        {
            _prefabProvider = prefabProvider;
            _colorProvider = colorProvider;
            _instantiator = instantiator;
        }

        public CastleVisual GetCastleVisual(CastleGO castle)
        {
            var prefab = _prefabProvider.GetCastleVisualPrefab();
            var visual = _instantiator.Instantiate(prefab);
            var color = _colorProvider.GetColor(castle.GetOwner());
            visual.SetColor(color);
            return visual;
        }
    }
}