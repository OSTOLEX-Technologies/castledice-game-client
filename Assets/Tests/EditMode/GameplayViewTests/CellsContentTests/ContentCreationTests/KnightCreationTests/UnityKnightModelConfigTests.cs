using System.Reflection;
using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentCreation.KnightsCreation;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentCreationTests.KnightCreationTests
{
    public class UnityKnightModelConfigTests
    {
        [Test]
        [TestCase(PlayerColor.Blue, "blueKnightModel")]
        [TestCase(PlayerColor.Red, "redKnightModel")]
        public void GetKnightModel_ShouldReturnKnightModel_WithAppropriateColor(PlayerColor color, string modelPropertyName)
        {
            var config = ScriptableObject.CreateInstance<UnityKnightModelConfig>();
            var expectedModel = new GameObject();
            config.GetType().GetField(modelPropertyName, BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(config, expectedModel);
            
            var actualModel = config.GetKnightModel(color);
            
            Assert.AreSame(expectedModel, actualModel);
        }
    }
}