using castledice_game_data_logic;
using castledice_game_logic;

namespace Src.GameplayPresenter.GameCreation
{
    public interface IGameCreator
    {
        Game CreateGame(GameStartData gameData);
    }
}