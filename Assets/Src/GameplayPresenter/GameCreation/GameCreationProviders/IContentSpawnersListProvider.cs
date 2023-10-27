using System.Collections.Generic;
using castledice_game_data_logic.Content.Generated;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public interface IContentSpawnersListProvider
    {
        List<IContentSpawner> GetContentSpawnersList(List<GeneratedContentData> contentData, List<Player> players);
    }
}