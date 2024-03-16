using System;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayView.ClientMoves;

namespace Src.Tutorial
{
    public class TutorialMovesPresenter
    {
        private readonly IMovesView _view;
        private readonly IPossibleMovesListProvider _possibleMovesListProvider;
        private readonly ILocalMoveApplier _localMoveApplier;
        private readonly IMoveConditionsSequence _moveConditionsSequence;
        private readonly int _playerId;

        public event EventHandler<AbstractMove> RightMovePicked;
        public event EventHandler<AbstractMove> WrongMovePicked;

        public TutorialMovesPresenter(IMovesView view, IPossibleMovesListProvider possibleMovesListProvider,
            ILocalMoveApplier localMoveApplier, IMoveConditionsSequence moveConditionsSequence, int playerId)
        {
            _view = view;
            _possibleMovesListProvider = possibleMovesListProvider;
            _localMoveApplier = localMoveApplier;
            _moveConditionsSequence = moveConditionsSequence;
            _playerId = playerId;

            _view.PositionClicked += OnPositionsClicked;
            _view.MovePicked += OnMovePicked;
        }

        private void OnPositionsClicked(object sender, Vector2Int position)
        {
            var moves = _possibleMovesListProvider.GetPossibleMoves(position, _playerId);
            _view.ShowMovesList(moves);
        }

        private void OnMovePicked(object sender, AbstractMove move)
        {
            var currentCondition = _moveConditionsSequence.GetCurrentCondition();
            if (currentCondition.IsSatisfiedBy(move))
            {
                _localMoveApplier.ApplyMove(move);
                _moveConditionsSequence.MoveToNextCondition();
                RightMovePicked?.Invoke(this, move);
            }
            else
            {
                WrongMovePicked?.Invoke(this, move);
            }
        }
    }
}