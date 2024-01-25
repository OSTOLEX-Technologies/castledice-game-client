using castledice_game_logic;
using UnityEngine;

namespace Src.GameplayView.PlayerObjectsColor
{
    public interface IPlayerObjectsColorProvider
    {
        Color GetColor(Player player);
    }
}