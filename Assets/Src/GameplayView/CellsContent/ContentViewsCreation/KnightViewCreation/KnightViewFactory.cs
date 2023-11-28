using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.PlayersRotations;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation
{
    public class KnightViewFactory : IKnightViewFactory
    {
        private readonly IPlayerRotationProvider _rotationProvider;
        private readonly IKnightModelProvider _modelProvider;
        private readonly IKnightAudioFactory _audioFactory;
        private readonly KnightView _knightViewPrefab;
        private readonly IInstantiator _instantiator;

        public KnightViewFactory(IPlayerRotationProvider rotationProvider, IKnightModelProvider modelProvider, IKnightAudioFactory audioFactory,
            KnightView knightViewPrefab, IInstantiator instantiator)
        {
            _rotationProvider = rotationProvider;
            _modelProvider = modelProvider;
            _audioFactory = audioFactory;
            _knightViewPrefab = knightViewPrefab;
            _instantiator = instantiator;
        }

        public KnightView GetKnightView(Knight knight)
        {
            var view = _instantiator.Instantiate(_knightViewPrefab);
            var model = _modelProvider.GetKnightModel(knight);
            var rotation = _rotationProvider.GetRotation(knight.GetOwner());
            var audio = _audioFactory.GetAudio(knight);
            view.Init(knight, model, rotation, audio);
            return view;
        }
    }
}