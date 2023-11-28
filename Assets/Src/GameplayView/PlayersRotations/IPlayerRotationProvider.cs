using castledice_game_logic;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.PlayersRotations
{
    /// <summary>
    /// This class is needed to determine correct rotation for player's units.
    /// </summary>
    public interface IPlayerRotationProvider
    {
        Vector3 GetRotation(Player player);
    }
}