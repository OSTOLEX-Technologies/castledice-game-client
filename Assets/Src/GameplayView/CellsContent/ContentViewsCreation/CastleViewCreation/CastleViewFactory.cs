using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using Src.GameplayView.CellsContent.ContentViews;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation
{
    public class CastleViewFactory : ICastleViewFactory
    {
        private readonly ICastleModelProvider _modelProvider;
        private readonly ICastleAudioFactory _audioFactory;
        private readonly CastleView _prefab;
        private readonly IInstantiator _instantiator;

        public CastleViewFactory(ICastleModelProvider modelProvider, ICastleAudioFactory audioFactory, CastleView prefab, IInstantiator instantiator)
        {
            _modelProvider = modelProvider;
            _audioFactory = audioFactory;
            _prefab = prefab;
            _instantiator = instantiator;
        }

        public CastleView GetCastleView(CastleGO castle)
        {
            var view = _instantiator.Instantiate(_prefab);
            var model = _modelProvider.GetCastleModel(castle);
            var audio = _audioFactory.GetAudio(castle);
            view.Init(castle, model, audio);
            return view;
        }
    }
}