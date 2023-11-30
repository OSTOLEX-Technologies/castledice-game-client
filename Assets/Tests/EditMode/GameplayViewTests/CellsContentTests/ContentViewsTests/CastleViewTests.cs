using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentViews;
using Tests.Utils.Mocks;
using static Tests.ObjectCreationUtility;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsTests
{
    public class CastleViewTests
    {
        [Test]
        public void Init_ShouldSetGivenModelAsChildObjectWithZeroLocalPosition()
        {
            var model = new GameObject();
            model.transform.position = Random.insideUnitSphere;
            var audio = new GameObject().AddComponent<CastleAudioForTests>();
            var castleView = new GameObject().AddComponent<CastleView>();
            var castle = GetCastle();
            
            castleView.Init(castle, model, audio);
            
            Assert.AreSame(castleView.transform, model.transform.parent);
            Assert.AreEqual(Vector3.zero, model.transform.localPosition);
        }
        
        [Test]
        public void Init_ShouldSetGivenAudioAsChildObjectWithZeroLocalPosition()
        {
            var audio = new GameObject().AddComponent<CastleAudioForTests>();
            audio.transform.position = Random.insideUnitSphere;
            var model = new GameObject();
            var castleView = new GameObject().AddComponent<CastleView>();
            var castle = GetCastle();
            
            castleView.Init(castle, model, audio);
            
            Assert.AreSame(castleView.transform, audio.transform.parent);
            Assert.AreEqual(Vector3.zero, audio.transform.localPosition);
        }

        [Test]
        public void DestroyView_ShouldCallPlayDestroySound_OnGivenCastleAudio()
        {
            var castleView = new GameObject().AddComponent<CastleView>();
            var audio = castleView.gameObject.AddComponent<CastleAudioForTests>();
            castleView.Init(GetCastle(), new GameObject(), audio);
            
            castleView.DestroyView();
            
            Assert.IsTrue(audio.PlayDestroySoundCalled);
        }

        [Test]
        public void DestroyView_ShouldSetModelInactive()
        {
            var model = new GameObject();
            model.SetActive(true);
            var castleView = new GameObject().AddComponent<CastleView>();
            castleView.Init(GetCastle(), model, new GameObject().AddComponent<CastleAudioForTests>());
            
            castleView.DestroyView();
            
            Assert.IsFalse(model.activeSelf);
        }
        
        [Test]
        public void ContentProperty_ShouldReturnCastle_GivenInInit()
        {
            var castleView = new GameObject().AddComponent<CastleView>();
            var audio = castleView.gameObject.AddComponent<CastleAudioForTests>();
            var castle = GetCastle();
            castleView.Init(castle, new GameObject(), audio);
            
            Assert.AreSame(castle, castleView.Content);
        }
    }
}