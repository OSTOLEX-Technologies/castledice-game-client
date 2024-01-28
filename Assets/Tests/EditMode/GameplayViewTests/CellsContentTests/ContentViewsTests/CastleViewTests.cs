using System.Collections;
using Moq;
using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using Src.GameplayView.CellsContent.ContentViews;
using Tests.Utils.Mocks;
using UnityEditor;
using static Tests.ObjectCreationUtility;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsTests
{
    public class CastleViewTests
    {
        [Test]
        public void Init_ShouldSetGivenVisualAsChildObjectWithZeroLocalPosition()
        {
            var visual = GetCastleVisual();
            visual.transform.position = Random.insideUnitSphere;
            var audio = new GameObject().AddComponent<CastleAudioForTests>();
            var castleView = new GameObject().AddComponent<CastleView>();
            var castle = GetCastle();
            
            castleView.Init(castle, visual, audio);
            
            Assert.AreSame(castleView.transform, visual.transform.parent);
            Assert.AreEqual(Vector3.zero, visual.transform.localPosition);
        }
        
        [Test]
        public void Init_ShouldSetGivenAudioAsChildObjectWithZeroLocalPosition()
        {
            var audio = new GameObject().AddComponent<CastleAudioForTests>();
            audio.transform.position = Random.insideUnitSphere;
            var castleView = new GameObject().AddComponent<CastleView>();
            var castle = GetCastle();
            
            castleView.Init(castle, GetCastleVisual(), audio);
            
            Assert.AreSame(castleView.transform, audio.transform.parent);
            Assert.AreEqual(Vector3.zero, audio.transform.localPosition);
        }

        [Test]
        public void DestroyView_ShouldCallPlayDestroySound_OnGivenCastleAudio()
        {
            var castleView = new GameObject().AddComponent<CastleView>();
            var audio = castleView.gameObject.AddComponent<CastleAudioForTests>();
            castleView.Init(GetCastle(), GetCastleVisual(), audio);
            
            castleView.DestroyView();
            
            Assert.IsTrue(audio.PlayDestroySoundCalled);
        }

        [Test]
        public void DestroyView_ShouldSetVisualInactive()
        {
            var visual = GetCastleVisual();
            visual.gameObject.SetActive(true);
            var castleView = new GameObject().AddComponent<CastleView>();
            castleView.Init(GetCastle(), visual, new GameObject().AddComponent<CastleAudioForTests>());
            
            castleView.DestroyView();
            
            Assert.IsFalse(visual.gameObject.activeSelf);
        }
        
        [Test]
        public void ContentProperty_ShouldReturnCastle_GivenInInit()
        {
            var castleView = new GameObject().AddComponent<CastleView>();
            var audio = castleView.gameObject.AddComponent<CastleAudioForTests>();
            var castle = GetCastle();
            castleView.Init(castle, GetCastleVisual(), audio);
            
            Assert.AreSame(castle, castleView.Content);
        }

        [Test]
        public void HitSound_ShouldBePlayed_IfCastleGotHit()
        {
            var castleView = new GameObject().AddComponent<CastleView>();
            var audio = castleView.gameObject.AddComponent<CastleAudioForTests>();
            var castle = GetCastle();
            castleView.Init(castle, GetCastleVisual(), audio);
            
            castle.CaptureHit(GetPlayer(actionPointsCount: 6));
            
            Assert.IsTrue(audio.PlayHitSoundCalled);
        }

        [UnityTest]
        public IEnumerator CastleView_ShouldDestroyItself_AfterDestroySoundPlayedEventIsInvoked()
        {
            var castleView = new GameObject().AddComponent<CastleView>();
            var audio = new GameObject().AddComponent<CastleAudioForTests>();
            castleView.Init(GetCastle(), GetCastleVisual(), audio);
            
            audio.PlayDestroySound();
            yield return null;
            
            Assert.IsTrue(castleView == null);
        }
    }
}