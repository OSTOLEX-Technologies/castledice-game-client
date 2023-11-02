﻿using System.Threading.Tasks;
using castledice_game_data_logic.MoveConverters;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.GameplayView.ClientMoves;

namespace Src.GameplayPresenter.ClientMoves
{
    public class ClientMovesPresenter
    {
        private readonly IPlayerDataProvider _playerDataProvider;
        private readonly IServerMoveApplier _serverMoveApplier;
        private readonly IPossibleMovesListProvider _possibleMovesListProvider;
        private readonly ILocalMoveApplier _localMoveApplier;
        private readonly IMoveToDataConverter _moveToDataConverter;
        private readonly IClientMovesView _view;

        public ClientMovesPresenter(IPlayerDataProvider playerDataProvider, IServerMoveApplier serverMoveApplier, IPossibleMovesListProvider possibleMovesListProvider, ILocalMoveApplier localMoveApplier, IMoveToDataConverter moveToDataConverter, IClientMovesView view)
        {
            _playerDataProvider = playerDataProvider;
            _serverMoveApplier = serverMoveApplier;
            _possibleMovesListProvider = possibleMovesListProvider;
            _localMoveApplier = localMoveApplier;
            _view = view;
            _view.MovePicked += OnMovePicked;
            _view.PositionClicked += OnPositionClicked;
        }

        public virtual async Task MakeMove(AbstractMove move)
        {
            // var applicationResult = await _serverMoveApplier.ApplyMoveAsync(move);
            // if (applicationResult == MoveApplicationResult.Applied)
            // {
            //     _localMoveApplier.ApplyMove(move);
            // }
        }

        public virtual void ShowMovesForPosition(Vector2Int position)
        {
            var playerId = _playerDataProvider.GetId();
            var moves = _possibleMovesListProvider.GetPossibleMoves(position, playerId);
            _view.ShowMovesList(moves);
        }

        private async void OnMovePicked(object sender, AbstractMove move)
        {
            await MakeMove(move);
        }
        
        private void OnPositionClicked(object sender, Vector2Int position)
        {
            ShowMovesForPosition(position);
        }
    }
}