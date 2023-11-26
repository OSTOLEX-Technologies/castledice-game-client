using UnityEngine;

namespace Src.GameplayView.Audio
{
    public abstract class SoundPlayer : MonoBehaviour
    {
        public abstract void PlaySound(Sound sound);
    }
}