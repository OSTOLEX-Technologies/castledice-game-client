using castledice_game_data_logic;

namespace Src.Tutorial
{
    public interface ITutorialGameStartDataProvider
    {
        GameStartData GetGameStartData(int playerId, int enemyId);
    }
}