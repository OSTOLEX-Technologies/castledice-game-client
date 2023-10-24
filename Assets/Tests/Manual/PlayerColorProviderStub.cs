using castledice_game_logic;
using Src.GameplayView.CellsContent.ContentCreation;

namespace Tests.Manual
{
    public class PlayerColorProviderStub : IPlayerColorProvider
    {
        public PlayerColor GetPlayerColor(Player player)
        {
            if (player.Id == 1)
            {
                return PlayerColor.Blue;
            }

            return PlayerColor.Red;
        }
    }
}