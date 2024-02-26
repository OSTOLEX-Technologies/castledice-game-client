using NUnit.Framework;
using Src.GameplayView.ClickDetection;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.GameplayViewTests.ClickDetectionTests
{
    public class UnityCellClickDetectorTests
    {
        public static Vector2Int[] Positions =
        {
            (0, 0), (1, 0), (2, 0),
            (0, 1), (1, 1), (2, 1),
            (0, 2), (1, 2), (2, 2)
        };
        
        [Test]
        public void Click_ShouldInvokeClickedEventWithPosition_GivenInInit([ValueSource(nameof(Positions))]Vector2Int position)
        {
            var gameObject = new GameObject();
            var detector = gameObject.AddComponent<UnityCellClickDetector>();
            detector.Init(position);
            Vector2Int eventPosition = default;
            var clicked = false;
            detector.Clicked += (sender, args) =>
            {
                clicked = true;
                eventPosition = args;
            };
            
            detector.Click();
            
            Assert.IsTrue(clicked);
        }
    }
}