using casltedice_events_logic.ServerToClient;
using Src.GameplayPresenter.Errors;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule.Errors
{
    public class ServerErrorsRouter : IServerErrorDTOAccepter
    {
        private readonly IErrorPresentersProvider _presentersProvider;
        
        public ServerErrorsRouter(IErrorPresentersProvider presentersProvider)
        {
            _presentersProvider = presentersProvider;
        }

        public void AcceptServerErrorDTO(ServerErrorDTO dto)
        {
            var data = dto.ErrorData;
            var presenter = _presentersProvider.GetPresenter(data.ErrorType);
            presenter.ShowError(data.Message);
        }
    }
}