using System;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.ContentVisuals.ContentColor
{
    [CreateAssetMenu(fileName = "PlayerContentColorConfig", menuName = "Configs/ContentColor/PlayerContentColorConfig", order = 1)]
    public class PlayerContentColorConfig : ScriptableObject, IPlayerContentColorConfig
    {
        [SerializeField] private Color redPlayerColor;
        [SerializeField] private Color bluePlayerColor;
        
        public Color GetColor(PlayerColor player)
        {
            return player switch
            {
                PlayerColor.Blue => bluePlayerColor,
                PlayerColor.Red => redPlayerColor,
                _ => throw new NotImplementedException()
            };
        }
    }
}