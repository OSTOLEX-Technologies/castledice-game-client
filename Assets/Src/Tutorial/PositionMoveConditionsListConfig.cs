using System;
using System.Collections.Generic;
using Src.General.MoveConditions;
using Tests.EditMode.GeneralTests;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;
using Vector2IntUnity = UnityEngine.Vector2Int;

namespace Src.Tutorial
{
    [Serializable]
    public class AllowedPositions
    {
        public List<Vector2IntUnity> Positions = new List<Vector2IntUnity>();
    }
    
    [CreateAssetMenu(fileName = "PositionMoveConditionsListConfig", menuName = "Configs/PositionMoveConditionsListConfig")]
    public class PositionMoveConditionsListConfig : ScriptableObject, IMoveConditionsListConfig
    {
        [SerializeField] private List<AllowedPositions> allowedPositions;   
        
        public List<IMoveCondition> GetMoveConditions()
        {
            var moveConditions = new List<IMoveCondition>();
            foreach (var positions in allowedPositions)
            {
                moveConditions.Add(new PositionsMoveCondition(positions.Positions.ConvertToGameLogicVector2IntList()));
            }
            
            return moveConditions;
        }
    }
}