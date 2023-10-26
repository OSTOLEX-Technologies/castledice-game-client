using System.Collections.Generic;
using castledice_game_data_logic.Content.Placeable;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public interface IBoardConfigProvider
    {
        BoardConfig GetBoardConfig(BoardData boardData, List<Player> players);
    }
}