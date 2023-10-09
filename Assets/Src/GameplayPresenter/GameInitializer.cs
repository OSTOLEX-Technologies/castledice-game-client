using castledice_game_data_logic;
using castledice_game_logic;

namespace Src.GameplayPresenter
{
    public abstract class GameInitializer
    {
        public abstract Game InitializeGame(GameStartData gameData);
    }
}