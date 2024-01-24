using System;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.PlayerObjectsColor
{
    [CreateAssetMenu(fileName = "PlayerContentColorConfig", menuName = "Configs/ObjectsColor/PlayerContentColorConfig", order = 1)]
    public class PlayerObjectsColorConfig : ScriptableObject, IPlayerObjectsColorConfig
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