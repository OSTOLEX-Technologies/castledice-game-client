using castledice_game_data_logic;
using castledice_game_logic;
using Src.GameplayPresenter.GameCreation.Creators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.DecksListCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;

namespace Src.GameplayPresenter.GameCreation
{
    public class GameCreator : IGameCreator
    {
        private readonly IPlayersListCreator _playersListCreator;
        private readonly IBoardConfigProvider _boardConfigProvider;
        private readonly IPlaceablesConfigCreator _placeablesConfigCreator;
        private readonly ITurnSwitchConditionsConfigProvider _turnSwitchConditionsConfigProvider;
        private readonly IDecksListCreator _decksListCreator;
        
        public GameCreator(IPlayersListCreator playersListCreator, IBoardConfigProvider boardConfigProvider, IPlaceablesConfigCreator placeablesConfigCreator, ITurnSwitchConditionsConfigProvider turnSwitchConditionsConfigProvider, IDecksListCreator decksListCreator)
        {
            _playersListCreator = playersListCreator;
            _boardConfigProvider = boardConfigProvider;
            _placeablesConfigCreator = placeablesConfigCreator;
            _turnSwitchConditionsConfigProvider = turnSwitchConditionsConfigProvider;
            _decksListCreator = decksListCreator;
        }
        
        public Game CreateGame(GameStartData gameData)
        {
            var players = _playersListCreator.GetPlayersList(gameData.PlayersIds);
            var boardConfig = _boardConfigProvider.GetBoardConfig(gameData.BoardData, players);
            var placeablesConfig = _placeablesConfigCreator.GetPlaceablesConfig(gameData.PlaceablesConfigData);
            var decks = _decksListCreator.GetDecksList(gameData.Decks);
            var turnSwitchConditionsConfig = _turnSwitchConditionsConfigProvider.GetTurnSwitchConditionsConfig(gameData.TscConfigData);
            var game = new Game(players, boardConfig, placeablesConfig, decks, turnSwitchConditionsConfig);
            return game;
        }
    }
}