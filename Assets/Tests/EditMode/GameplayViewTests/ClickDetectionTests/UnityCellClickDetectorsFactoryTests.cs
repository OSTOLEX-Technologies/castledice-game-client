using Moq;
using NUnit.Framework;
using Src.GameplayView.ClickDetection;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.GameplayViewTests.ClickDetectionTests
{
    public class UnityCellClickDetectorsFactoryTests
    {
        public static Vector2Int[] Positions = { (0, 0), (1, 0), (2, 0), (0, 1), (1, 1) };
        
        [Test]
        //Here position of the returned detector is checked by invoking Clicked event with Click method.
        public void GetDetector_ShouldReturnUnityCellClickDetector_WithGivenPosition([ValueSource(nameof(Positions))]Vector2Int position)
        {
            var gameObject = new GameObject();
            var factory = gameObject.AddComponent<UnityCellClickDetectorsFactory>();
            var configMock = new Mock<IUnityCellClickDetectorsConfig>();
            var detectorPrefab = new GameObject().AddComponent<UnityCellClickDetector>();
            configMock.Setup(c => c.DetectorPrefab).Returns(detectorPrefab);
            factory.Init(configMock.Object);
            Vector2Int actualPosition = default;
            
            var detector = factory.GetDetector(position);
            detector.Clicked += (sender, pos) => actualPosition = pos;
            detector.Click();
            
            Assert.AreEqual(position, actualPosition);
        }
    }
}