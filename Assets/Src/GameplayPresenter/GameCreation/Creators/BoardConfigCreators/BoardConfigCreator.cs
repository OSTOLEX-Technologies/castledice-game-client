using System.Collections.Generic;
using castledice_game_data_logic.ConfigsData;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;

namespace Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators
{
    public class BoardConfigCreator : IBoardConfigCreator
    {
        private readonly IContentSpawnersListCreator _spawnersCreator;
        private readonly ICellsGeneratorCreator _cellsGeneratorCreator;

        public BoardConfigCreator(IContentSpawnersListCreator spawnersCreator, ICellsGeneratorCreator cellsGeneratorCreator)
        {
            _spawnersCreator = spawnersCreator;
            _cellsGeneratorCreator = cellsGeneratorCreator;
        }

        public BoardConfig GetBoardConfig(BoardData boardData, List<Player> players)
        {
            var spawners = _spawnersCreator.GetContentSpawnersList(boardData.GeneratedContent, players);
            var cellsGenerator = _cellsGeneratorCreator.GetCellsGenerator(boardData.CellsPresence);
            return new BoardConfig(spawners, cellsGenerator, boardData.CellType);
        }
    }
}