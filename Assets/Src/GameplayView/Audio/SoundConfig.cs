using System;
using UnityEngine;

namespace Src.GameplayView.Audio
{
    /// <summary>
    /// This class should be used in other config classes to store sound data and create Sound objects based on it.
    /// </summary>
    [Serializable]
    public class SoundConfig
    {
        public AudioClip clip;
        public float volume;
    }
}