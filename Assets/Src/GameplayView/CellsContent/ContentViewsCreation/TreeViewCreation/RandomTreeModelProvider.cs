using System;
using castledice_game_logic.Math;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.TreeViewCreation
{
    public class RandomTreeModelProvider : ITreeModelProvider
    {
        private readonly IRangeRandomNumberGenerator _rnd;
        private readonly ITreeModelPrefabsListProvider _prefabsListProvider;
        private readonly IInstantiator _instantiator;

        public RandomTreeModelProvider(IRangeRandomNumberGenerator rnd, ITreeModelPrefabsListProvider prefabsListProvider, IInstantiator instantiator)
        {
            _rnd = rnd;
            _prefabsListProvider = prefabsListProvider;
            _instantiator = instantiator;
        }

        public GameObject GetTreeModel()
        {
            var prefabsList = _prefabsListProvider.GetTreeModelPrefabsList();
            if (prefabsList.Count == 0)
            {
                throw new ArgumentException("Tree models prefabs list is empty!");
            }
            var index = _rnd.GetRandom(0, prefabsList.Count);
            return _instantiator.Instantiate(prefabsList[index]);
        }
    }
}