using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation
{
    public class CastleViewFactory : ICastleViewFactory
    {
        private readonly ICastleVisualCreator _visualCreator;
        private readonly ICastleAudioFactory _audioFactory;
        private readonly CastleView _prefab;
        private readonly IInstantiator _instantiator;

        public CastleViewFactory(ICastleVisualCreator visualCreator, ICastleAudioFactory audioFactory, CastleView prefab, IInstantiator instantiator)
        {
            _audioFactory = audioFactory;
            _prefab = prefab;
            _instantiator = instantiator;
            _visualCreator = visualCreator;
        }

        public CastleView GetCastleView(CastleGO castle)
        {
            var view = _instantiator.Instantiate(_prefab);
            var audio = _audioFactory.GetAudio(castle);
            var visual = _visualCreator.GetCastleVisual(castle);
            view.Init(castle, visual, audio);
            return view;
        }
    }
}