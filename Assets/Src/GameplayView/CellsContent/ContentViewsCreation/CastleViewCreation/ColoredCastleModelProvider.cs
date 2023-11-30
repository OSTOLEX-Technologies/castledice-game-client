using Src.GameplayView.PlayersColors;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation
{
    public class ColoredCastleModelProvider : ICastleModelProvider
    {
        private readonly IPlayerColorProvider _colorProvider;
        private readonly ICastleModelPrefabProvider _prefabProvider;
        private readonly IInstantiator _instantiator;
        
        public ColoredCastleModelProvider(IPlayerColorProvider colorProvider, ICastleModelPrefabProvider prefabProvider, IInstantiator instantiator)
        {
            _colorProvider = colorProvider;
            _prefabProvider = prefabProvider;
            _instantiator = instantiator;
        }

        public GameObject GetCastleModel(CastleGO castle)
        {
            var color = _colorProvider.GetPlayerColor(castle.GetOwner());
            var prefab = _prefabProvider.GetCastleModelPrefab(color);
            return _instantiator.Instantiate(prefab);
        }
    }
}