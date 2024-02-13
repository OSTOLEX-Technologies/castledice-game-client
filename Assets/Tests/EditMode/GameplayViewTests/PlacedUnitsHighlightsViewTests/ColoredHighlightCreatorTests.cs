using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.Highlights;
using Tests.Utils.Mocks;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.PlacedUnitsHighlightsViewTests
{
    public class ColoredHighlightCreatorTests
    {
        [Test]
        public void GetHighlight_ShouldReturnInstantiatedPrefab_FromProvider()
        {
            var underlinePrefab = new GameObject().AddComponent<ColoredHighlightForTests>();
            var instantiatedUnderline = new GameObject().AddComponent<ColoredHighlightForTests>();
            var prefabConfigMock = new Mock<IColoredHighlightPrefabProvider>();
            prefabConfigMock.Setup(config => config.GetHighlightPrefab()).Returns(underlinePrefab);
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(instantiator => instantiator.Instantiate<ColoredHighlight>(underlinePrefab)).Returns(instantiatedUnderline);
            var creator = new ColoredHighlightCreator(prefabConfigMock.Object, instantiatorMock.Object);
            
            var underline = creator.GetHighlight();
            
            Assert.AreSame(instantiatedUnderline, underline);
        }
    }
}