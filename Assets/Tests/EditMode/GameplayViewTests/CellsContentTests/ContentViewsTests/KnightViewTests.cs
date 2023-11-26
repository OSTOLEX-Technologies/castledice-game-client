﻿using Moq;
using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentViews;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsTests
{
    public class KnightViewTests
    {
        private class TestKnightAudio : KnightAudio
        {
            public override void PlayPlaceSound() { }

            public override void PlayHitSound() { }

            public override void PlayDestroySound() { }
        }
        
        [Test, Repeat(10)]
        public void Init_ShouldSetGivenModelAsChildObjectWithZeroLocalPosition_AndSetGivenRotation()
        {
            var rotation = new Vector3(Random.value, Random.value, Random.value);
            var model = new GameObject();
            model.transform.position = Random.insideUnitSphere;
            var audio = new Mock<KnightAudio>().Object;
            var knightView = new GameObject().AddComponent<KnightView>();
            var knight = GetKnight();
            
            knightView.Init(knight, model, rotation, audio);
            var actualRotation = model.transform.localRotation.eulerAngles;
            
            Assert.AreSame(knightView.transform, model.transform.parent);
            Assert.AreEqual(Vector3.zero, model.transform.localPosition);
            Assert.AreEqual(rotation.x, actualRotation.x, 0.0001f);
            Assert.AreEqual(rotation.y, actualRotation.y, 0.0001f);
            Assert.AreEqual(rotation.z, actualRotation.z, 0.0001f);
        }
        
        [Test]
        public void Init_ShouldSetKnightSound_AsChildObjectWithZeroLocalPosition()
        {
            var model = new GameObject();
            var audio = new GameObject().AddComponent<TestKnightAudio>();
            audio.transform.position = Random.insideUnitSphere;
            var knightView = new GameObject().AddComponent<KnightView>();
            var knight = GetKnight();
            
            knightView.Init(knight, model, Vector3.zero, audio);
            
            Assert.AreSame(knightView.transform, audio.transform.parent);
            Assert.AreEqual(Vector3.zero, audio.transform.localPosition);
        }

        //In this test "attached" means added as component to the game object.
        //The idea is that KnightView should access this component via GetComponent method.
        [Test]
        public void StartView_ShouldPlayPlaceSound_OnGivenKnightAudio()
        {
            var knightView = new GameObject().AddComponent<KnightView>();
            var audioMock = new Mock<KnightAudio>();
            knightView.Init(GetKnight(), new GameObject(), Vector3.zero, audioMock.Object);
            
            knightView.StartView();
            
            audioMock.Verify(audio => audio.PlayPlaceSound(), Times.Once);
        }
        
        [Test]
        public void DestroyView_ShouldPlayDestroySound_OnAttachedKnightAudio()
        {
            var knightView = new GameObject().AddComponent<KnightView>();
            var audioMock = new Mock<KnightAudio>();
            knightView.Init(GetKnight(), new GameObject(), Vector3.zero, audioMock.Object);
            
            knightView.DestroyView();
            
            audioMock.Verify(audio => audio.PlayDestroySound(), Times.Once);
        }

        [Test]
        public void ContentProperty_ShouldReturnKnight_GivenInInit()
        {
            var expectedKnight = GetKnight();
            var knightView = new GameObject().AddComponent<KnightView>();
            var audio = new Mock<KnightAudio>().Object;
            knightView.Init(expectedKnight, new GameObject(), Vector3.zero, audio);
            
            var actualKnight = knightView.Content;
            
            Assert.AreSame(expectedKnight, actualKnight);
        }
    }
}