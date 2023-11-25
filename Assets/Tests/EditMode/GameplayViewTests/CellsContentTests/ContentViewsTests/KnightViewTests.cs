using Moq;
using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentAudio;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentViews;
using Tests.Utils.Mocks;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsTests
{
    public class KnightViewTests
    {
        [Test, Repeat(10)]
        public void Init_ShouldSetGivenModelAsChildObjectWithZeroLocalPosition_AndSetGivenRotation()
        {
            var rotation = new Vector3(Random.value, Random.value, Random.value);
            var model = new GameObject();
            var knightView = new GameObject().AddComponent<KnightView>();
            var knight = GetKnight();
            
            knightView.Init(knight, model, rotation);
            var actualRotation = model.transform.localRotation.eulerAngles;
            
            Assert.AreSame(knightView.transform, model.transform.parent);
            Assert.AreEqual(Vector3.zero, model.transform.localPosition);
            Assert.AreEqual(rotation.x, actualRotation.x, 0.0001f);
            Assert.AreEqual(rotation.y, actualRotation.y, 0.0001f);
            Assert.AreEqual(rotation.z, actualRotation.z, 0.0001f);
        }

        //In this test "attached" means added as component to the game object.
        //The idea is that KnightView should access this component via GetComponent method.
        [Test]
        public void StartView_ShouldPlayPlaceSound_OnAttachedKnightAudio()
        {
            var knightView = new GameObject().AddComponent<KnightView>();
            var audio = knightView.gameObject.AddComponent<KnightAudioMock>();
            knightView.Init(GetKnight(), new GameObject(), Vector3.zero);
            
            knightView.StartView();
            
            Assert.IsTrue(audio.PlayPlaceSoundCalled);
        }
        
        [Test]
        public void DestroyView_ShouldPlayDestroySound_OnAttachedKnightAudio()
        {
            var knightView = new GameObject().AddComponent<KnightView>();
            var audio = knightView.gameObject.AddComponent<KnightAudioMock>();
            knightView.Init(GetKnight(), new GameObject(), Vector3.zero);
            
            knightView.DestroyView();
            
            Assert.IsTrue(audio.PlayDestroySoundCalled);
        }
    }
}