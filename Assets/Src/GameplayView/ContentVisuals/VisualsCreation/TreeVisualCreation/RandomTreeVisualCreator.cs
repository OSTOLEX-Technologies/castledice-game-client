using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation
{
    public class RandomTreeVisualCreator : ITreeVisualCreator
    {
        private readonly IRangeRandomNumberGenerator _random;
        private readonly ITreeVisualPrefabsListProvider _prefabsListProvider;
        private readonly IInstantiator _instantiator;

        public RandomTreeVisualCreator(IRangeRandomNumberGenerator random, ITreeVisualPrefabsListProvider prefabsListProvider, IInstantiator instantiator)
        {
            _random = random;
            _prefabsListProvider = prefabsListProvider;
            _instantiator = instantiator;
        }

        public TreeVisual GetTreeVisual(Tree tree)
        {
            var prefabsList = _prefabsListProvider.GetTreeVisualPrefabsList();
            var prefab = prefabsList[_random.GetRandom(0, prefabsList.Count)];
            var treeVisual = _instantiator.Instantiate(prefab);
            return treeVisual;
        }
    }
}