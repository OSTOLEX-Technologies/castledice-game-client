using castledice_game_data_logic;
using JetBrains.Annotations;

namespace Src.GameplayPresenter
{
    public class GameSearchResult
    {
        public enum ResultStatus
        {
            Success,
            Canceled
        }
        
        public ResultStatus Status { get; set; }
        [CanBeNull] public GameStartData GameStartData { get; set; }
    }
}