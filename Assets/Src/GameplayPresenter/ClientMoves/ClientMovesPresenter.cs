using System.Threading.Tasks;
using castledice_game_data_logic.MoveConverters;
using castledice_game_logic.MovesLogic;
using Src.Auth.TokenProviders;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayView.ClientMoves;
using Src.HttpUtils;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayPresenter.ClientMoves
{
    public class ClientMovesPresenter
    {
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly IServerMoveApplier _serverMoveApplier;
        private readonly IPossibleMovesListProvider _possibleMovesListProvider;
        private readonly ILocalMoveApplier _localMoveApplier;
        private readonly IMoveToDataConverter _moveToDataConverter;
        private readonly IMovesView _view;

        public ClientMovesPresenter(
            IAccessTokenProvider accessTokenProvider,
            IServerMoveApplier serverMoveApplier, 
            IPossibleMovesListProvider possibleMovesListProvider, 
            ILocalMoveApplier localMoveApplier, 
            IMoveToDataConverter moveToDataConverter, 
            IMovesView view)
        {
            _accessTokenProvider = accessTokenProvider;
            _serverMoveApplier = serverMoveApplier;
            _possibleMovesListProvider = possibleMovesListProvider;
            _localMoveApplier = localMoveApplier;
            _moveToDataConverter = moveToDataConverter;
            _view = view;
            _view.MovePicked += OnMovePicked;
            _view.PositionClicked += OnPositionClicked;
        }

        public virtual async Task MakeMove(AbstractMove move)
        {
            var moveData = _moveToDataConverter.ConvertToData(move);
            var applicationResult = await _serverMoveApplier.ApplyMoveAsync(moveData, await _accessTokenProvider.GetAccessTokenAsync());
            if (applicationResult == MoveApplicationResult.Applied)
            {
                _localMoveApplier.ApplyMove(move);
            }
        }

        public virtual async void ShowMovesForPosition(Vector2Int position)
        {
            var localPlayerId = await new PlayerIdProvider().GetLocalPlayerId();
            var moves = _possibleMovesListProvider.GetPossibleMoves(position, localPlayerId);
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
    
    public class PVEMovesPresenter
    {
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly IServerMoveApplier _serverMoveApplier;
        private readonly IPossibleMovesListProvider _possibleMovesListProvider;
        private readonly ILocalMoveApplier _localMoveApplier;
        private readonly IMoveToDataConverter _moveToDataConverter;
        private readonly IMovesView _view;

        public PVEMovesPresenter(
            IAccessTokenProvider accessTokenProvider,
            IServerMoveApplier serverMoveApplier, 
            IPossibleMovesListProvider possibleMovesListProvider, 
            ILocalMoveApplier localMoveApplier, 
            IMoveToDataConverter moveToDataConverter, 
            IMovesView view)
        {
            _accessTokenProvider = accessTokenProvider;
            _serverMoveApplier = serverMoveApplier;
            _possibleMovesListProvider = possibleMovesListProvider;
            _localMoveApplier = localMoveApplier;
            _moveToDataConverter = moveToDataConverter;
            _view = view;
            _view.MovePicked += OnMovePicked;
            _view.PositionClicked += OnPositionClicked;
        }

        public virtual async Task MakeMove(AbstractMove move)
        {
            var moveData = _moveToDataConverter.ConvertToData(move);
            var applicationResult = await _serverMoveApplier.ApplyMoveAsync(moveData, await _accessTokenProvider.GetAccessTokenAsync());
            if (applicationResult == MoveApplicationResult.Applied)
            {
                _localMoveApplier.ApplyMove(move);
            }
        }

        public virtual async void ShowMovesForPosition(Vector2Int position)
        {
            var localPlayerId = 1;
            var moves = _possibleMovesListProvider.GetPossibleMoves(position, localPlayerId);
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