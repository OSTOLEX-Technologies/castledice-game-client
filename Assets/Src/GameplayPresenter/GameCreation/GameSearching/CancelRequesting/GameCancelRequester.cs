using System.Threading.Tasks;

namespace Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting
{
    public class GameCancelRequester : IGameCancelRequester
    {
        private readonly ICancelGameDtoCreator _dtoCreator;
        private readonly ICancelGameDtoSender _dtoSender;
        
        public GameCancelRequester(ICancelGameDtoCreator dtoCreator, ICancelGameDtoSender dtoSender)
        {
            _dtoCreator = dtoCreator;
            _dtoSender = dtoSender;
        }
        
        public async Task RequestCancelAsync()
        {
            var dto = await _dtoCreator.CreateDtoAsync();
            _dtoSender.SendDto(dto);
        }
    }
}