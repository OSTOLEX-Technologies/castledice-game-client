using castledice_game_logic;
using UnityEngine;

namespace Src.GameplayView.CellsContent
{
    public interface IPlayerRotationProvider
    {
        Vector3 GetRotationForPlayer(Player player);
    }
}