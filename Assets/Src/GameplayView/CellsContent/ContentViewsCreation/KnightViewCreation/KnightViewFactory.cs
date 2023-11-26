using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.PlayersColors;
using Src.GameplayView.PlayersRotations;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation
{
    public class KnightViewFactory : IKnightViewFactory
    {
        private readonly IPlayerRotationProvider _rotationProvider;
        private readonly IPlayerColorProvider _colorProvider;
        private readonly IKnightModelProvider _modelProvider;
        private readonly KnightView _knightViewPrefab;
        private readonly IInstantiator _instantiator;

        public KnightViewFactory(IPlayerRotationProvider rotationProvider, IPlayerColorProvider colorProvider, IKnightModelProvider modelProvider, KnightView knightViewPrefab, IInstantiator instantiator)
        {
            _rotationProvider = rotationProvider;
            _colorProvider = colorProvider;
            _modelProvider = modelProvider;
            _knightViewPrefab = knightViewPrefab;
            _instantiator = instantiator;
        }

        public KnightView GetKnightView(Knight knight)
        {
            var view = _instantiator.Instantiate(_knightViewPrefab);
            var playerColor = _colorProvider.GetPlayerColor(knight.GetOwner());
            var model = _modelProvider.GetKnightModel(playerColor);
            var rotation = _rotationProvider.GetRotation(playerColor);
            view.Init(knight, model, rotation, null);
            return view;
        }
    }
}