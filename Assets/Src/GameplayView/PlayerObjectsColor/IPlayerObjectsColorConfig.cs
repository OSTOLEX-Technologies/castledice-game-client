using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.PlayerObjectsColor
{
    public interface IPlayerObjectsColorConfig
    {
        Color GetColor(PlayerColor player);
    }
}