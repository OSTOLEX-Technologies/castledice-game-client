using System.Collections.Generic;
using NUnit.Framework;
using Src.General.MoveConditions;
using Src.Tutorial;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.TutorialTests
{
    public class PositionMoveConditionsListConfigTests
    {
        private const string AllowedPositionsFieldName = "allowedPositions";
        private const string ConditionAllowedPositionsFieldName = "_allowedPositions";
        
        [Test]
        public void GetMoveConditions_ShouldReturnList_WithSameCountAsAllowedPositionsList()
        {
            var rnd = new System.Random();
            var allowedPositions = GetNestedPositionsList(rnd.Next(1, 10));
            var config = ScriptableObject.CreateInstance<PositionMoveConditionsListConfig>();
            config.SetPrivateField(AllowedPositionsFieldName, allowedPositions);
            
            var moveConditions = config.GetMoveConditions();
            
            Assert.AreEqual(allowedPositions.Count, moveConditions.Count);
        }

        [Test]
        public void GetMoveConditions_ShouldReturnList_WithPositionsMoveConditionsOnly()
        {
            var rnd = new System.Random();
            var allowedPositions = GetNestedPositionsList(rnd.Next(1, 10));
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
            var rnd = new System.Random();
            var allowedPositions = GetNestedPositionsList(rnd.Next(1, 10));
            var config = ScriptableObject.CreateInstance<PositionMoveConditionsListConfig>();
            config.SetPrivateField(AllowedPositionsFieldName, allowedPositions);
            
            var moveConditions = config.GetMoveConditions();
            
            for (var i = 0; i < moveConditions.Count; i++)
            {
                var condition = moveConditions[i] as PositionsMoveCondition;
                var expectedPositions = allowedPositions[i];
                var actualPositions = condition.GetPrivateField<List<Vector2Int>>(ConditionAllowedPositionsFieldName);
                Assert.AreEqual(expectedPositions, actualPositions);
            }
        }
        
        private static List<List<Vector2Int>> GetNestedPositionsList(int count)
        {
            var rnd = new System.Random();
            var allowedPositions = new List<List<Vector2Int>>();
            for (var i = 0; i < count; i++)
            {
                var nestedList = new List<Vector2Int>();
                for (var j = 0; j < rnd.Next(1, 10); j++) nestedList.Add(new Vector2Int(rnd.Next(1, 10), rnd.Next(1, 10)));
                allowedPositions.Add(nestedList);
            }
            return allowedPositions;
        }
    }
}