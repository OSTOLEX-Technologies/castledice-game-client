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
            var castleView = new GameObject().AddComponent<CastleView>();
            var castle = GetCastle();
            
            castleView.Init(castle, model);
            
            Assert.AreSame(castleView.transform, model.transform.parent);
            Assert.AreEqual(Vector3.zero, model.transform.localPosition);
        }

        [Test]
        public void DestroyView_ShouldCallPlayDestroySound_OnAttachedCastleAudio()
        {
            var castleView = new GameObject().AddComponent<CastleView>();
            var audio = castleView.gameObject.AddComponent<CastleAudioForTests>();
            castleView.Init(GetCastle(), new GameObject());
            
            castleView.DestroyView();
            
            Assert.IsTrue(audio.PlayDestroySoundCalled);
        }
        
        [Test]
        public void ContentProperty_ShouldReturnCastle_GivenInInit()
        {
            var castleView = new GameObject().AddComponent<CastleView>();
            var castle = GetCastle();
            castleView.Init(castle, new GameObject());
            
            Assert.AreSame(castle, castleView.Content);
        }
    }
}