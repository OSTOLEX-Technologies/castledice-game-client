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
        private readonly IBoardConfigCreator _boardConfigCreator;
        private readonly IPlaceablesConfigCreator _placeablesConfigCreator;
        private readonly ITurnSwitchConditionsConfigCreator _turnSwitchConditionsConfigCreator;
        private readonly IDecksListCreator _decksListCreator;
        
        public GameCreator(IPlayersListCreator playersListCreator, IBoardConfigCreator boardConfigCreator, IPlaceablesConfigCreator placeablesConfigCreator, ITurnSwitchConditionsConfigCreator turnSwitchConditionsConfigCreator, IDecksListCreator decksListCreator)
        {
            _playersListCreator = playersListCreator;
            _boardConfigCreator = boardConfigCreator;
            _placeablesConfigCreator = placeablesConfigCreator;
            _turnSwitchConditionsConfigCreator = turnSwitchConditionsConfigCreator;
            _decksListCreator = decksListCreator;
        }
        
        public Game CreateGame(GameStartData gameData)
        {
            var players = _playersListCreator.GetPlayersList(gameData.PlayersIds);
            var boardConfig = _boardConfigCreator.GetBoardConfig(gameData.BoardData, players);
            var placeablesConfig = _placeablesConfigCreator.GetPlaceablesConfig(gameData.PlaceablesConfigData);
            var decks = _decksListCreator.GetDecksList(gameData.Decks);
            var turnSwitchConditionsConfig = _turnSwitchConditionsConfigCreator.GetTurnSwitchConditionsConfig(gameData.TscConfigData);
            var game = new Game(players, boardConfig, placeablesConfig, decks, turnSwitchConditionsConfig);
            return game;
        }
    }
}