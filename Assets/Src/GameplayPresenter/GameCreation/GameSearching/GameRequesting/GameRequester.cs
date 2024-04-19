using System.Threading.Tasks;

namespace Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting
{
    public class GameRequester : IGameRequester
    {
        private readonly IRequestGameDtoCreator _dtoCreator;
        private readonly IRequestGameDtoSender _dtoSender;
        
        public GameRequester(IRequestGameDtoCreator dtoCreator, IRequestGameDtoSender dtoSender)
        {
            _dtoCreator = dtoCreator;
            _dtoSender = dtoSender;
        }
        
        public async Task RequestGameAsync()
        {
            var dto = await _dtoCreator.CreateDtoAsync();
            _dtoSender.SendDto(dto);
        }
    }
}