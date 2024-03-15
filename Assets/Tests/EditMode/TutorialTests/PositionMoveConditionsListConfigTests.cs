using System.Collections.Generic;
using NUnit.Framework;
using Src.General.MoveConditions;
using Src.Tutorial;
using Tests.EditMode.GeneralTests;
using UnityEngine;
using Random = System.Random;
using Vector2Int = castledice_game_logic.Math.Vector2Int;
using Vector2IntUnity = UnityEngine.Vector2Int;

namespace Tests.EditMode.TutorialTests
{
    public class PositionMoveConditionsListConfigTests
    {
        private const string AllowedPositionsFieldName = "allowedPositions";
        private const string ConditionAllowedPositionsFieldName = "_allowedPositions";
        
        private readonly Random _rnd = new Random();
        
        [Test]
        public void GetMoveConditions_ShouldReturnList_WithSameCountAsAllowedPositionsList()
        {
            var allowedPositions = GetAllowedPositionsList(_rnd.Next(1, 10));
            var config = ScriptableObject.CreateInstance<PositionMoveConditionsListConfig>();
            config.SetPrivateField(AllowedPositionsFieldName, allowedPositions);
            
            var moveConditions = config.GetMoveConditions();
            
            Assert.AreEqual(allowedPositions.Count, moveConditions.Count);
        }

        [Test]
        public void GetMoveConditions_ShouldReturnList_WithPositionsMoveConditionsOnly()
        {
            var allowedPositions = GetAllowedPositionsList(_rnd.Next(1, 10));
            var config = ScriptableObject.CreateInstance<PositionMoveConditionsListConfig>();
            config.SetPrivateField(AllowedPositionsFieldName, allowedPositions);
            
            var moveConditions = config.GetMoveConditions();
            
            foreach (var moveCondition in moveConditions)
            {
                Assert.IsInstanceOf<PositionsMoveCondition>(moveCondition);
            }
        }

        [Test]
        //Proper positions in this context means that each conditions should have list of allowed positions corresponding to the sublist in config.
        public void GetMoveConditions_ShouldReturnPositionsMoveConditions_WhereEachConditionHasProperPositions()
        {
            var allowedPositions = GetAllowedPositionsList(_rnd.Next(1, 10));
            var config = ScriptableObject.CreateInstance<PositionMoveConditionsListConfig>();
            config.SetPrivateField(AllowedPositionsFieldName, allowedPositions);
            
            var moveConditions = config.GetMoveConditions();
            
            for (var i = 0; i < moveConditions.Count; i++)
            {
                var condition = moveConditions[i] as PositionsMoveCondition;
                var expectedPositions = allowedPositions[i].Positions.ConvertToGameLogicVector2IntList();
                var actualPositions = condition.GetPrivateField<List<Vector2Int>>(ConditionAllowedPositionsFieldName);
                Assert.AreEqual(expectedPositions, actualPositions);
            }
        }
        
        private List<AllowedPositions> GetAllowedPositionsList(int count)
        {
            var allowedPositions = new List<AllowedPositions>();
            for (var i = 0; i < count; i++)
            {
                var positions = new AllowedPositions();
                var list = positions.Positions;
                for (var j = 0; j < _rnd.Next(1, 10); j++) list.Add(new Vector2IntUnity(_rnd.Next(1, 10), _rnd.Next(1, 10)));
                allowedPositions.Add(positions);
            }
            return allowedPositions;
        }
    }
}