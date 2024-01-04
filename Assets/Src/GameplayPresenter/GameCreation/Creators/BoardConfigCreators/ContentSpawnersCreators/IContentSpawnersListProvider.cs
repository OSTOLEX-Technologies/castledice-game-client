using System.Collections.Generic;
using castledice_game_data_logic.Content;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;

namespace Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators
{
    public interface IContentSpawnersListProvider
    {
        List<IContentSpawner> GetContentSpawnersList(List<ContentData> contentData, List<Player> players);
    }
}