using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation
{
    public interface IGameConstructorWrapper
    {
        Game CreateGame(List<Player> playersList, BoardConfig boardConfig, PlaceablesConfig placeablesConfig,
            TurnSwitchConditionsConfig turnSwitchConditionsConfig);
    }
}