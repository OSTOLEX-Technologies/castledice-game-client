using castledice_game_logic.GameObjects;
using Src.GameplayView.PlayerObjectsColor;
using Src.GameplayView.PlayersRotations;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation
{
    public class KnightVisualCreator : IKnightVisualCreator
    {
        private readonly IKnightVisualPrefabProvider _prefabProvider;
        private readonly IPlayerRotationProvider _rotationProvider;
        private readonly IPlayerObjectsColorProvider _colorProvider;
        private readonly IInstantiator _instantiator;

        public KnightVisualCreator(IKnightVisualPrefabProvider prefabProvider, IPlayerObjectsColorProvider colorProvider, IInstantiator instantiator, IPlayerRotationProvider rotationProvider)
        {
            _prefabProvider = prefabProvider;
            _colorProvider = colorProvider;
            _instantiator = instantiator;
            _rotationProvider = rotationProvider;
        }

        public KnightVisual GetKnightVisual(Knight knight)
        {
            var prefab = _prefabProvider.GetKnightVisualPrefab();
            var knightVisual = _instantiator.Instantiate(prefab);
            var color = _colorProvider.GetColor(knight.GetOwner());
            knightVisual.SetColor(color);
            var rotation = _rotationProvider.GetRotation(knight.GetOwner());
            knightVisual.transform.localEulerAngles = rotation;
            return knightVisual;
        }
    }
}