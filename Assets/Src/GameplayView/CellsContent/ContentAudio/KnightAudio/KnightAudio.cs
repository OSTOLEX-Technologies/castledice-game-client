using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentAudio.KnightAudio
{
    public abstract class KnightAudio : MonoBehaviour
    {
       public abstract void PlayPlaceSound();
       public abstract void PlayHitSound();
       public abstract void PlayDestroySound();
    }
}