using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentAudio.CastleAudio
{
    public abstract class CastleAudio : MonoBehaviour
    {
       public abstract void PlayHitSound();
       public abstract void PlayDestroySound();
    }
}