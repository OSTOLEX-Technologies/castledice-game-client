using System.Reflection;
using NUnit.Framework;
using Src.GameplayView.CellsContent.ContentAudio;
using static Tests.ObjectCreationUtility;
using Tests.Utils.Mocks;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentAudioTests
{
    public class UnityKnightAudioTests
    {
        [Test]
        public void Awake_ShouldSetClipPlayer_AttachedToGameObject()
        {
            var gameObject = new GameObject();
            var expectedClipPlayer = gameObject.AddComponent<AudioClipPlayerForTests>();
            var unityKnightAudio = gameObject.AddComponent<UnityKnightAudio>();
            
            var fieldInfo = unityKnightAudio.GetType().GetField("_clipPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
            var actualClipPlayer = fieldInfo?.GetValue(unityKnightAudio);
            
            Assert.AreSame(expectedClipPlayer, actualClipPlayer);
        }
        
        [Test]
        public void PlayPlaceSound_ShouldPlayPlaceSound_UsingClipPlayer()
        {
            var gameObject = new GameObject();
            var clipPlayer = gameObject.AddComponent<AudioClipPlayerForTests>();
            var unityKnightAudio = gameObject.AddComponent<UnityKnightAudio>();
            var expectedClip = GetAudioClip();
            AddObjectReferenceValueToSerializedProperty(unityKnightAudio, "placeSound", expectedClip);
            
            unityKnightAudio.PlayPlaceSound();
            
            Assert.AreSame(expectedClip, clipPlayer.LastPlayedClip);
        }
        
        [Test]
        public void PlayHitSound_ShouldPlayHitSound_UsingClipPlayer()
        {
            var gameObject = new GameObject();
            var clipPlayer = gameObject.AddComponent<AudioClipPlayerForTests>();
            var unityKnightAudio = gameObject.AddComponent<UnityKnightAudio>();
            var expectedClip = GetAudioClip();
            AddObjectReferenceValueToSerializedProperty(unityKnightAudio, "hitSound", expectedClip);
            
            unityKnightAudio.PlayHitSound();
            
            Assert.AreSame(expectedClip, clipPlayer.LastPlayedClip);
        }
        
        [Test]
        public void PlayDestroySound_ShouldPlayDestroySound_UsingClipPlayer()
        {
            var gameObject = new GameObject();
            var clipPlayer = gameObject.AddComponent<AudioClipPlayerForTests>();
            var unityKnightAudio = gameObject.AddComponent<UnityKnightAudio>();
            var expectedClip = GetAudioClip();
            AddObjectReferenceValueToSerializedProperty(unityKnightAudio, "destroySound", expectedClip);
            
            unityKnightAudio.PlayDestroySound();
            
            Assert.AreSame(expectedClip, clipPlayer.LastPlayedClip);
        }
    }
}