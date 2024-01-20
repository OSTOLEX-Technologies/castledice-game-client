using castledice_game_logic;
using UnityEngine;

namespace Src.GameplayView.ContentVisuals.ContentColor
{
    public interface IPlayerContentColorProvider
    {
        Color GetContentColor(Player player);
    }
}