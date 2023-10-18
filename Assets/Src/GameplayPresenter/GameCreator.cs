using castledice_game_data_logic;
using castledice_game_logic;

namespace Src.GameplayPresenter
{
    public abstract class GameCreator
    {
        public abstract Game CreateGame(GameStartData gameData);
    }
}