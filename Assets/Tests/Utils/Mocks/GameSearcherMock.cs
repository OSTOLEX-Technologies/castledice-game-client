using Src.GameplayPresenter;
using static Tests.ObjectCreationUtility;
using System.Threading.Tasks;
using Src.GameplayPresenter.GameCreation;

namespace Tests.Mocks
{
    public class GameSearcherMock : IGameSearcher
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

        public async Task<GameSearchResult> SearchGameAsync(string playerToken)
        {
            await Task.Delay(SearchTimeMilliseconds);
            return _isCanceled ? CancelResult : SuccessResult;
        }

        public async Task<bool> CancelGameSearchAsync(string playerToken)
        {
            await Task.Delay(CancelTimeMilliseconds);
            _isCanceled = CanBeCanceled;
            return _isCanceled;
        }
    }
}