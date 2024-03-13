using System.Collections.Generic;
using Src.General.MoveConditions;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.Tutorial
{
    [CreateAssetMenu(fileName = "PositionMoveConditionsListConfig", menuName = "Configs/PositionMoveConditionsListConfig")]
    public class PositionMoveConditionsListConfig : ScriptableObject, IMoveConditionsListConfig
    {
        [Tooltip("Each sublist will be converted into a separate PositionsMoveCondition, which determines the allowed positions for a move.")]
        [SerializeField] private List<List<Vector2Int>> allowedPositions;   
        
        public List<IMoveCondition> GetMoveConditions()
        {
            var moveConditions = new List<IMoveCondition>();
            foreach (var positions in allowedPositions)
            {
                moveConditions.Add(new PositionsMoveCondition(positions));
            }
            
            return moveConditions;
        }
    }
}