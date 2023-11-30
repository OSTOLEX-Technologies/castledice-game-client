using castledice_game_logic.GameObjects;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation
{
    public class ColoredKnightModelProvider : IKnightModelProvider
    {
        private readonly IPlayerColorProvider _colorProvider;
        private readonly IKnightModelPrefabProvider _prefabProvider;
        private readonly IInstantiator _instantiator;
        
        public ColoredKnightModelProvider(IPlayerColorProvider colorProvider, IKnightModelPrefabProvider prefabProvider, IInstantiator instantiator)
        {
            _colorProvider = colorProvider;
            _prefabProvider = prefabProvider;
            _instantiator = instantiator;
        }
        
        public GameObject GetKnightModel(Knight knight)
        {
            var color = _colorProvider.GetPlayerColor(knight.GetOwner());
            var prefab = _prefabProvider.GetKnightModelPrefab(color);
            return _instantiator.Instantiate(prefab);
        }
    }
}