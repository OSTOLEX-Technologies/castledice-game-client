using UnityEngine;

namespace Src.GameplayView.Audio
{
    public abstract class UnitySoundPlayer : MonoBehaviour, ISoundPlayer
    {
        public abstract void PlaySound(Sound sound);
    }
}