using System.Collections.Generic;
using castledice_game_data_logic.Content.Placeable;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public class BoardConfigProvider : IBoardConfigProvider
    {
        private IContentSpawnersListProvider _spawnersProvider;
        private ICellsGeneratorProvider _cellsGeneratorProvider;

        public BoardConfigProvider(IContentSpawnersListProvider spawnersProvider, ICellsGeneratorProvider cellsGeneratorProvider)
        {
            _spawnersProvider = spawnersProvider;
            _cellsGeneratorProvider = cellsGeneratorProvider;
        }

        public BoardConfig GetBoardConfig(BoardData boardData, List<Player> players)
        {
            throw new System.NotImplementedException();
        }
    }
}