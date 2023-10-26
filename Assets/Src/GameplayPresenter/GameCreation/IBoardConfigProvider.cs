using System.Collections.Generic;
using castledice_game_data_logic.Content.Generated;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation
{
    public interface IBoardConfigProvider
    {
        BoardConfig GetBoardConfig(CellType cellType, bool[,] cellsPresence,
            List<GeneratedContentData> generatedContent);
    }
}