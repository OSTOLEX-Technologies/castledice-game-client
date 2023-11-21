using System.Collections.Generic;
using castledice_game_data_logic.Content;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public class CoordinateContentSpawnerProvider : IContentSpawnersListProvider
    {
        private readonly IContentToCoordinateProvider _contentToCoordinateProvider;

        public CoordinateContentSpawnerProvider(IContentToCoordinateProvider contentToCoordinateProvider)
        {
            _contentToCoordinateProvider = contentToCoordinateProvider;
        }

        public List<IContentSpawner> GetContentSpawnersList(List<ContentData> contentData, List<Player> players)
        {
            var spawners = new List<IContentSpawner>();
            var contentToCoordinates = new List<ContentToCoordinate>();
            foreach (var data in contentData)
            {
                var contentToCoordinate = _contentToCoordinateProvider.GetContentToCoordinate(data, players);
                contentToCoordinates.Add(contentToCoordinate);
            }
            spawners.Add(new CoordinateContentSpawner(contentToCoordinates));
            return spawners;
        }
    }
}