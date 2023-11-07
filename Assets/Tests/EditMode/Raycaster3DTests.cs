using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using NUnit.Framework;
using Src.PlayerInput;
using UnityEngine;

namespace Tests.EditMode
{
    public interface ISomeInterface
    {
        int Id { get; }
    }

    public class SomeMonoBehaviour : MonoBehaviour, ISomeInterface
    {
        public int Id { get; private set; }

        public void Init(int id)
        {
            Id = id;
        }

        protected bool Equals(SomeMonoBehaviour other)
        {
            return base.Equals(other) && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SomeMonoBehaviour)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Id);
        }
    }
    
    [TestFixture]
    public class Raycaster3DTests
    {
        public static IEnumerable<System.Func<GetRayIntersectionsTestCase>> TestCases()
        {
            yield return EmptyListCase;
            yield return ListWithOneElementCase;
            yield return ListWithTwoElementsCase;
        }
        
        [TestCaseSource(nameof(TestCases))]
        public static void GetRayIntersections_ShouldReturnListWithAllHitObjects_OfGivenType(System.Func<GetRayIntersectionsTestCase> testCaseCreationFunction)
        {
            var testCase = testCaseCreationFunction();
            var hits = testCase.Hits;
            var expectedList = testCase.ExpectedList;
            var hitsProviderMock = new Mock<IRaycastHitsProvider>();
            hitsProviderMock.Setup(h => h.GetHitsForRay(It.IsAny<Ray>())).Returns(hits);
            var raycaster = new Raycaster3D(hitsProviderMock.Object);
            
            var result = raycaster.GetRayIntersections<ISomeInterface>(new Ray());
            
            Assert.AreEqual(expectedList.Count, result.Count);
            foreach (var expectedObject in expectedList)
            {
                Assert.Contains(expectedObject, result);
            }
        }
        public class GetRayIntersectionsTestCase
        {
            public RaycastHit[] Hits { get; }
            public List<ISomeInterface> ExpectedList { get; }
            
            public GetRayIntersectionsTestCase(RaycastHit[] hits, List<ISomeInterface> expectedList)
            {
                Hits = hits;
                ExpectedList = expectedList;
            }
        }
            
        private static GetRayIntersectionsTestCase EmptyListCase()
        {
            var list = new List<ISomeInterface>();
            var hits = new[]
            {
                GetHit(),
                GetHit(),
                GetHit()
            };
            return new GetRayIntersectionsTestCase(hits, list);
        }
        
        private static GetRayIntersectionsTestCase ListWithOneElementCase()
        {
            var list = new List<ISomeInterface>();
            var hits = new[]
            {
                GetHitWithComponent(out SomeMonoBehaviour component),
                GetHit(),
                GetHit()
            };
            list.Add(component);
            return new GetRayIntersectionsTestCase(hits, list);
        }
        
        private static GetRayIntersectionsTestCase ListWithTwoElementsCase()
        {
            var list = new List<ISomeInterface>();
            var hits = new[]
            {
                GetHitWithComponent(out SomeMonoBehaviour component),
                GetHit(),
                GetHitWithComponent(out SomeMonoBehaviour component2),
            };
            list.Add(component);
            list.Add(component2);
            return new GetRayIntersectionsTestCase(hits, list);
        }

        private static RaycastHit GetHitWithComponent<T>(out T addedComponent) where T: Component
        {
            var collider = new GameObject().AddComponent<BoxCollider>();
            var raycastHit = MockHit(collider);
            addedComponent = raycastHit.collider.gameObject.AddComponent<T>();
            return raycastHit;
        }

        private static RaycastHit GetHit()
        {
            var collider = new GameObject().AddComponent<BoxCollider>();
            var raycastHit = MockHit(collider);
            return raycastHit;
        }

        private static RaycastHit MockHit(Collider collider)
        {
            RaycastHit hit = new RaycastHit();
            object obj = hit;
            Type type = obj.GetType();
            FieldInfo fieldInfo = type.GetField("m_Collider", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(obj, collider.GetInstanceID());
            return (RaycastHit)obj;
        }
    }
}