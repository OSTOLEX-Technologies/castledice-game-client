using castledice_game_logic.GameObjects;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentAudio.KnightAudio
{
    public interface IKnightAudioFactory
    {
        KnightAudio GetAudio(Knight knight, GameObject gameObject);
    }
}