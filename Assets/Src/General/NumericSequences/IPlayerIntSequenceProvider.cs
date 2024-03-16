using castledice_game_logic;

namespace Src.General.NumericSequences
{
    public interface IPlayerIntSequenceProvider
    {
        IIntSequence GetSequence(Player forPlayer);
    }
}