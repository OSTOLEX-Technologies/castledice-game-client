﻿using castledice_game_data_logic;
using castledice_game_logic;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;

namespace Src.GameplayPresenter.GameCreation
{
    public class GameCreator : IGameCreator
    {
        private readonly IPlayersListCreator _playersListCreator;
        private readonly IBoardConfigCreator _boardConfigCreator;
        private readonly IPlaceablesConfigCreator _placeablesConfigCreator;
        private readonly ITurnSwitchConditionsConfigCreator _turnSwitchConditionsConfigCreator;
        
        public GameCreator(IPlayersListCreator playersListCreator, IBoardConfigCreator boardConfigCreator, IPlaceablesConfigCreator placeablesConfigCreator, ITurnSwitchConditionsConfigCreator turnSwitchConditionsConfigCreator)
        {
            _playersListCreator = playersListCreator;
            _boardConfigCreator = boardConfigCreator;
            _placeablesConfigCreator = placeablesConfigCreator;
            _turnSwitchConditionsConfigCreator = turnSwitchConditionsConfigCreator;
        }
        
        public Game CreateGame(GameStartData gameData)
        {
            var boardConfig = _boardConfigCreator.GetBoardConfig(gameData.BoardData, null);
            var placeablesConfig = _placeablesConfigCreator.GetPlaceablesConfig(gameData.PlaceablesConfigData);
            var turnSwitchConditionsConfig = _turnSwitchConditionsConfigCreator.GetTurnSwitchConditionsConfig(gameData.TscConfigData);
            var game = new Game(null, boardConfig, placeablesConfig, turnSwitchConditionsConfig);
            return game;
        }
    }
}