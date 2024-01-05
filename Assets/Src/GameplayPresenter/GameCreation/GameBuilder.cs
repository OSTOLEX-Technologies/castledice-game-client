using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;
using JetBrains.Annotations;

namespace Src.GameplayPresenter.GameCreation
{
    public class GameBuilder : IGameBuilder
    {
        [CanBeNull] private List<Player> _playersList;
        [CanBeNull] private BoardConfig _boardConfig;
        [CanBeNull] private PlaceablesConfig _placeablesConfig;
        [CanBeNull] private TurnSwitchConditionsConfig _turnSwitchConditionsConfig;
        
        private readonly IGameConstructorWrapper _gameConstructorWrapper;

        public GameBuilder(IGameConstructorWrapper gameConstructorWrapper)
        {
            _gameConstructorWrapper = gameConstructorWrapper;
        }

        public IGameBuilder BuildPlayersList(List<Player> playersList)
        {
            _playersList = playersList;
            return this;
        }

        public IGameBuilder BuildBoardConfig(BoardConfig boardConfig)
        {
            _boardConfig = boardConfig;
            return this;
        }

        public IGameBuilder BuildPlaceablesConfig(PlaceablesConfig placeablesConfig)
        {
            _placeablesConfig = placeablesConfig;
            return this;
        }

        public IGameBuilder BuildTurnSwitchConditionsConfig(TurnSwitchConditionsConfig turnSwitchConditionsConfig)
        {
            _turnSwitchConditionsConfig = turnSwitchConditionsConfig;
            return this;
        }

        public Game Build()
        {
            if (_playersList == null)
            {
                throw new InvalidOperationException("Players list is not set");
            }
            if (_boardConfig == null)
            {
                throw new InvalidOperationException("Board config is not set");
            }
            if (_placeablesConfig == null)
            {
                throw new InvalidOperationException("Placeables config is not set");
            }
            if (_turnSwitchConditionsConfig == null)
            {
                throw new InvalidOperationException("Turn switch conditions config is not set");
            }
            return _gameConstructorWrapper.CreateGame(_playersList, _boardConfig, _placeablesConfig,
                _turnSwitchConditionsConfig);
        }

        public IGameBuilder Reset()
        {
            _playersList = null;
            _boardConfig = null;
            _placeablesConfig = null;
            _turnSwitchConditionsConfig = null;
            return this;
        }
    }
}