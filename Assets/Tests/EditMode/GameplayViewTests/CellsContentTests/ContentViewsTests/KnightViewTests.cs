using System.Collections;
using NUnit.Framework;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentViews;
using Tests.Utils.Mocks;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsTests
{
    public class KnightViewTests
    {
        [Test, Repeat(10)]
        public void Init_ShouldSetGivenVisualAsChildObjectWithZeroLocalPosition_AndSetGivenRotation()
        {
            var rotation = new Vector3(Random.value, Random.value, Random.value);
            var visual = GetKnightVisual();
            visual.transform.position = Random.insideUnitSphere;
            var audio = new GameObject().AddComponent<KnightAudioForTests>();
            var knightView = new GameObject().AddComponent<KnightView>();
            var knight = GetKnight();
            
            knightView.Init(knight, visual, rotation, audio);
            var actualRotation = visual.transform.localRotation.eulerAngles;
            
            Assert.AreSame(knightView.transform, visual.transform.parent);
            Assert.AreEqual(Vector3.zero, visual.transform.localPosition);
            Assert.AreEqual(rotation.x, actualRotation.x, 0.0001f);
            Assert.AreEqual(rotation.y, actualRotation.y, 0.0001f);
            Assert.AreEqual(rotation.z, actualRotation.z, 0.0001f);
        }
        
        [Test]
        public void Init_ShouldSetKnightAudio_AsChildObjectWithZeroLocalPosition()
        {
            var audio = new GameObject().AddComponent<KnightAudioForTests>();
            audio.transform.position = Random.insideUnitSphere;
            var knightView = new GameObject().AddComponent<KnightView>();
            var knight = GetKnight();
            
            knightView.Init(knight, GetKnightVisual(), Vector3.zero, audio);
            
            Assert.AreSame(knightView.transform, audio.transform.parent);
            Assert.AreEqual(Vector3.zero, audio.transform.localPosition);
        }

        //In this test "attached" means added as component to the game object.
        //The idea is that KnightView should access this component via GetComponent method.
        [Test]
        public void StartView_ShouldPlayPlaceSound_OnGivenKnightAudio()
        {
            var knightView = new GameObject().AddComponent<KnightView>();
            var audio = new GameObject().AddComponent<KnightAudioForTests>();
            knightView.Init(GetKnight(), GetKnightVisual(), Vector3.zero, audio);
            
            knightView.StartView();
            
            Assert.IsTrue(audio.PlayPlaceSoundWasCalled);
        }
        
        [Test]
        public void DestroyView_ShouldPlayDestroySound_OnGivenKnightAudio()
        {
            var knightView = new GameObject().AddComponent<KnightView>();
            var audio = new GameObject().AddComponent<KnightAudioForTests>();
            knightView.Init(GetKnight(), GetKnightVisual(), Vector3.zero, audio);
            
            knightView.DestroyView();
            
            Assert.IsTrue(audio.PlayDestroySoundWasCalled);
        }

        [Test]
        public void DestroyView_ShouldSetVisualInactive()
        {
            var visual = GetKnightVisual();
            visual.gameObject.SetActive(true);
            var knightView = new GameObject().AddComponent<KnightView>();
            knightView.Init(GetKnight(), visual, Vector3.zero, new GameObject().AddComponent<KnightAudioForTests>());
            
            knightView.DestroyView();
            
            Assert.IsFalse(visual.gameObject.activeSelf);
        }

        [Test]
        public void ContentProperty_ShouldReturnKnight_GivenInInit()
        {
            var expectedKnight = GetKnight();
            var knightView = new GameObject().AddComponent<KnightView>();
            var audio = new GameObject().AddComponent<KnightAudioForTests>();
            knightView.Init(expectedKnight, GetKnightVisual(), Vector3.zero, audio);
            
            var actualKnight = knightView.Content;
            
            Assert.AreSame(expectedKnight, actualKnight);
        }
        
        [UnityTest]
        public IEnumerator KnightView_ShouldDestroyItself_AfterDestroySoundIsPlayed()
        {
            var knightView = new GameObject().AddComponent<KnightView>();
            var audio = new GameObject().AddComponent<KnightAudioForTests>();
            knightView.Init(GetKnight(), GetKnightVisual(), Vector3.zero, audio);
            
            audio.PlayDestroySound();
            yield return new WaitForEndOfFrame();
            
            Assert.IsTrue(knightView == null);
        }
    }
}