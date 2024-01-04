using System.Collections.Generic;
using castledice_game_data_logic.ConfigsData;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;

namespace Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators
{
    public class BoardConfigProvider : IBoardConfigProvider
    {
        private readonly IContentSpawnersListProvider _spawnersProvider;
        private readonly ICellsGeneratorCreator _cellsGeneratorCreator;

        public BoardConfigProvider(IContentSpawnersListProvider spawnersProvider, ICellsGeneratorCreator cellsGeneratorCreator)
        {
            _spawnersProvider = spawnersProvider;
            _cellsGeneratorCreator = cellsGeneratorCreator;
        }

        public BoardConfig GetBoardConfig(BoardData boardData, List<Player> players)
        {
            var spawners = _spawnersProvider.GetContentSpawnersList(boardData.GeneratedContent, players);
            var cellsGenerator = _cellsGeneratorCreator.GetCellsGenerator(boardData.CellsPresence);
            return new BoardConfig(spawners, cellsGenerator, boardData.CellType);
        }
    }
}