using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation
{
    public interface IGameBuilder
    {
        IGameBuilder BuildPlayersList(List<Player> playersList);
        IGameBuilder BuildBoardConfig(BoardConfig boardConfig);
        IGameBuilder BuildPlaceablesConfig(PlaceablesConfig placeablesConfig);
        IGameBuilder BuildTurnSwitchConditionsConfig(TurnSwitchConditionsConfig turnSwitchConditionsConfig);
        Game Build();
        IGameBuilder Reset();
    }
}