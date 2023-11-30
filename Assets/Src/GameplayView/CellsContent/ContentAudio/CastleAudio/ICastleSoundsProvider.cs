using CastleGO = castledice_game_logic.GameObjects.Castle;
using Src.GameplayView.Audio;

namespace Src.GameplayView.CellsContent.ContentAudio.CastleAudio
{
    public interface ICastleSoundsProvider
    {
        Sound GetHitSound(CastleGO castle);
        Sound GetDestroySound(CastleGO castle);
    }
}