using Moq;
using NUnit.Framework;
using Src.GameplayView;
using Src.GameplayView.UnitsUnderlines;
using Tests.Utils.Mocks;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.UnitsUnderlinesTests
{
    public class UnderlineCreatorTests
    {
        [Test]
        public void GetUnderline_ShouldReturnInstantiatedUnderlinePrefab_FromProvider()
        {
            var underlinePrefab = new GameObject().AddComponent<UnderlineForTests>();
            var instantiatedUnderline = new GameObject().AddComponent<UnderlineForTests>();
            var prefabConfigMock = new Mock<IUnderlinePrefabProvider>();
            prefabConfigMock.Setup(config => config.GetUnderlinePrefab()).Returns(underlinePrefab);
            var instantiatorMock = new Mock<IInstantiator>();
            instantiatorMock.Setup(instantiator => instantiator.Instantiate<Underline>(underlinePrefab)).Returns(instantiatedUnderline);
            var creator = new UnderlineCreator(prefabConfigMock.Object, instantiatorMock.Object);
            
            var underline = creator.GetUnderline();
            
            Assert.AreSame(instantiatedUnderline, underline);
        }
    }
}