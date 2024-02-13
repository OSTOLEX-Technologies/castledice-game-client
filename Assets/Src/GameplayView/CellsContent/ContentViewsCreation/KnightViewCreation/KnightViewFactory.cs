using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation;
using Src.GameplayView.PlayersRotations;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation
{
    public class KnightViewFactory : IKnightViewFactory
    {
        private readonly IKnightVisualCreator _visualCreator;
        private readonly IKnightAudioFactory _audioFactory;
        private readonly KnightView _knightViewPrefab;
        private readonly IInstantiator _instantiator;

        public KnightViewFactory(IKnightVisualCreator visualCreator, IKnightAudioFactory audioFactory,
            KnightView knightViewPrefab, IInstantiator instantiator)
        {
            _audioFactory = audioFactory;
            _knightViewPrefab = knightViewPrefab;
            _instantiator = instantiator;
            _visualCreator = visualCreator;
        }

        public KnightView GetKnightView(Knight knight)
        {
            var view = _instantiator.Instantiate(_knightViewPrefab);
            var audio = _audioFactory.GetAudio(knight);
            var visual = _visualCreator.GetKnightVisual(knight);
            view.Init(knight, visual, audio);
            return view;
        }
    }
}