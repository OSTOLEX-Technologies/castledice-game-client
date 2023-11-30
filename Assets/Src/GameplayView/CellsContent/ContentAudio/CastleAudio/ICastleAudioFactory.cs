using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.GameplayView.CellsContent.ContentAudio.CastleAudio
{
    public interface ICastleAudioFactory
    {
        CastleAudio GetAudio(CastleGO castle);
    }
}