using castledice_game_data_logic;
using castledice_game_logic;

namespace Src.GameplayPresenter.GameCreation.CreationHandling
{
    public interface IGameCreationHandler
    {
        public void HandleCreatedGame(Game game, GameStartData gameStartData);
    }
}