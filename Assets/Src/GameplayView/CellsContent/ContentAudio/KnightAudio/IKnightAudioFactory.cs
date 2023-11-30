using castledice_game_logic.GameObjects;

namespace Src.GameplayView.CellsContent.ContentAudio.KnightAudio
{
    public interface IKnightAudioFactory
    {
        KnightAudio GetAudio(Knight knight);
    }
}