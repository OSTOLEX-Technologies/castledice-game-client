using castledice_game_logic.GameObjects;
using Src.GameplayView.Audio;

namespace Src.GameplayView.CellsContent.ContentAudio.KnightAudio
{
    public interface IKnightSoundsProvider
    {
        Sound GetPlaceSound(Knight knight);
        Sound GetHitSound(Knight knight);
        Sound GetDestroySound(Knight knight);
    }
}