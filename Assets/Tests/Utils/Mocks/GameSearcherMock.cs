using Src.GameplayPresenter;
using static Tests.ObjectCreationUtility;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    public class GameSearcherMock : GameSearcher
    {
        public GameSearchResult SuccessResult { get; set; } = new()
        {
            Status = GameSearchResult.ResultStatus.Success,
            GameStartData = GetGameStartData()
        };

        public GameSearchResult CancelResult { get; set; } = new()
        {
            Status = GameSearchResult.ResultStatus.Canceled,
            GameStartData = null
        };

        public int SearchTimeMilliseconds { get; set; }
        public int CancelTimeMilliseconds { get; set; }

        public bool CanBeCanceled { get; set; }

        private bool _isCanceled;

        public override async Task<GameSearchResult> SearchGameAsync(string playerToken)
        {
            await Task.Delay(SearchTimeMilliseconds);
            return _isCanceled ? CancelResult : SuccessResult;
        }

        public override async Task<bool> CancelGameSearch(string playerToken)
        {
            await Task.Delay(CancelTimeMilliseconds);
            _isCanceled = CanBeCanceled;
            return _isCanceled;
        }
    }
}