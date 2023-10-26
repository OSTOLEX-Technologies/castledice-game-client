using castledice_game_data_logic;
using castledice_game_logic;

namespace Src.GameplayPresenter.GameCreation
{
    public class GameCreator : IGameCreator
    {
        private readonly IPlayersListProvider _playersListProvider;
        private readonly IBoardConfigProvider _boardConfigProvider;
        private readonly IPlaceablesConfigProvider _placeablesConfigProvider;
        private readonly IDecksListProvider _decksListProvider;
        
        public GameCreator(IPlayersListProvider playersListProvider, IBoardConfigProvider boardConfigProvider, IPlaceablesConfigProvider placeablesConfigProvider, IDecksListProvider decksListProvider)
        {
            _playersListProvider = playersListProvider;
            _boardConfigProvider = boardConfigProvider;
            _placeablesConfigProvider = placeablesConfigProvider;
            _decksListProvider = decksListProvider;
        }
        
        public Game CreateGame(GameStartData gameData)
        {
            var players = _playersListProvider.GetPlayersList(gameData.PlayersIds);
            var boardConfig = _boardConfigProvider.GetBoardConfig(gameData.CellType, gameData.CellsPresence, gameData.GeneratedContent);
            var placeablesConfig = _placeablesConfigProvider.GetPlaceablesConfig(gameData.PlaceablesConfig);
            var decks = _decksListProvider.GetDecksList(gameData.Decks);
            return new Game(players, boardConfig, placeablesConfig, decks);
        }
    }
}