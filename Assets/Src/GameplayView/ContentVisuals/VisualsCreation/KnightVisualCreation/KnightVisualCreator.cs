using castledice_game_logic.GameObjects;
using Src.GameplayView.ContentVisuals.ContentColor;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation
{
    public class KnightVisualCreator : IKnightVisualCreator
    {
        private readonly IKnightVisualPrefabProvider _prefabProvider;
        private readonly IPlayerContentColorProvider _colorProvider;
        private readonly IInstantiator _instantiator;

        public KnightVisualCreator(IKnightVisualPrefabProvider prefabProvider, IPlayerContentColorProvider colorProvider, IInstantiator instantiator)
        {
            _prefabProvider = prefabProvider;
            _colorProvider = colorProvider;
            _instantiator = instantiator;
        }

        public KnightVisual GetKnightVisual(Knight knight)
        {
            var prefab = _prefabProvider.GetKnightVisualPrefab();
            var knightVisual = _instantiator.Instantiate(prefab);
            var color = _colorProvider.GetContentColor(knight.GetOwner());
            knightVisual.SetColor(color);
            return knightVisual;
        }
    }
}