using castledice_game_data_logic;
using castledice_game_logic;
using Src.General.Caching;

namespace Src.GameplayPresenter.GameCreation.CreationHandling
{
    public class CachingGameCreationHandlerDecorator : GameCreationHandlerDecorator
    {
        private readonly IObjectCacher _cacher;
        
        public CachingGameCreationHandlerDecorator(IGameCreationHandler handler, IObjectCacher cacher) : base(handler)
        {
            _cacher = cacher;
        }

        public override void HandleCreatedGame(Game game, GameStartData gameStartData)
        {
            _cacher.CacheObject(game);
            _cacher.CacheObject(gameStartData);
            base.HandleCreatedGame(game, gameStartData);
        }
    }
}