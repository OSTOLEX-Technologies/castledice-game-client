using System.Collections.Generic;
using castledice_game_data_logic.Content.Placeable;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public class BoardConfigProvider : IBoardConfigProvider
    {
        private readonly IContentSpawnersListProvider _spawnersProvider;
        private readonly ICellsGeneratorProvider _cellsGeneratorProvider;

        public BoardConfigProvider(IContentSpawnersListProvider spawnersProvider, ICellsGeneratorProvider cellsGeneratorProvider)
        {
            _spawnersProvider = spawnersProvider;
            _cellsGeneratorProvider = cellsGeneratorProvider;
        }

        public BoardConfig GetBoardConfig(BoardData boardData, List<Player> players)
        {
            var spawners = _spawnersProvider.GetContentSpawnersList(boardData.GeneratedContent, players);
            var cellsGenerator = _cellsGeneratorProvider.GetCellsGenerator(boardData.CellsPresence);
            return new BoardConfig(spawners, cellsGenerator, boardData.CellType);
        }
    }
}