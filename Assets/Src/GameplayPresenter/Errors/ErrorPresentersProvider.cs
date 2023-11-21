using System;
using castledice_game_data_logic.Errors;

namespace Src.GameplayPresenter.Errors
{
    public class ErrorPresentersProvider : IErrorPresentersProvider
    {
        private readonly GameNotSavedErrorPresenter _gameNotSavedErrorPresenter;

        public ErrorPresentersProvider(GameNotSavedErrorPresenter gameNotSavedErrorPresenter)
        {
            _gameNotSavedErrorPresenter = gameNotSavedErrorPresenter;
        }

        public IErrorPresenter GetPresenter(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.GameNotSaved => _gameNotSavedErrorPresenter,
                _ => throw new InvalidOperationException("No presenter found for given error type: " + errorType)
            };
        }
    }
}