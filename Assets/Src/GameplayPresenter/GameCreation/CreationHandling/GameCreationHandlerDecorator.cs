using castledice_game_data_logic;
using castledice_game_logic;

namespace Src.GameplayPresenter.GameCreation.CreationHandling
{
    public abstract class GameCreationHandlerDecorator : IGameCreationHandler
    {
        protected readonly IGameCreationHandler Handler;

        protected GameCreationHandlerDecorator(IGameCreationHandler handler)
        {
            Handler = handler;
        }
        
        public virtual void HandleCreatedGame(Game game, GameStartData gameStartData)
        {
            Handler.HandleCreatedGame(game, gameStartData);
        }
    }
}