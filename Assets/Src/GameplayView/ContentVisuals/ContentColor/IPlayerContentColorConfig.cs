using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.ContentVisuals.ContentColor
{
    public interface IPlayerContentColorConfig
    {
        Color GetColor(PlayerColor player);
    }
}