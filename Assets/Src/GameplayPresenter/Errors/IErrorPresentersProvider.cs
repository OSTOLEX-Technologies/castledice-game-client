using castledice_game_data_logic.Errors;

namespace Src.GameplayPresenter.Errors
{
    public interface IErrorPresentersProvider
    {
        IErrorPresenter GetPresenter(ErrorType errorType);
    }
}