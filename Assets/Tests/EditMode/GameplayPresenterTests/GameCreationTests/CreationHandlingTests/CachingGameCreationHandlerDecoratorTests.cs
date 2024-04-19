using castledice_game_data_logic;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.CreationHandling;
using Src.General.Caching;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreationHandlingTests
{
    public class CachingGameCreationHandlerDecoratorTests
    {
        [Test]
        public void HandleCreatedGame_ShouldCacheGame()
        {
            var cacherMock = new Mock<IObjectCacher>();
            var game = GetGame();
            var decorator = new DecoratorBuilder
            {
                Cacher = cacherMock.Object
            }.Build();
            
            decorator.HandleCreatedGame(game, It.IsAny<GameStartData>());
            
            cacherMock.Verify(c => c.CacheObject(game));   
        }

        [Test]
        public void HandleCreateGame_ShouldCacheGameStartData()
        {
            var cacherMock = new Mock<IObjectCacher>();
            var gameStartData = GetGameStartData();
            var decorator = new DecoratorBuilder
            {
                Cacher = cacherMock.Object
            }.Build();
            
            decorator.HandleCreatedGame(It.IsAny<Game>(), gameStartData);
            
            cacherMock.Verify(c => c.CacheObject(gameStartData));
        }

        [Test]
        public void HandleCreateGame_ShouldCallBaseHandleCreateGame_AfterCaching()
        {
            var cacherMock = new Mock<IObjectCacher>();
            var baseHandlerMock = new Mock<IGameCreationHandler>();
            var calledLast = false;
            cacherMock.Setup(c => c.CacheObject(It.IsAny<object>())).Callback(() => calledLast = false);
            baseHandlerMock.Setup(h => h.HandleCreatedGame(
                It.IsAny<Game>(),
                It.IsAny<GameStartData>())).Callback(() => calledLast = true);
            var decorator = new DecoratorBuilder
            {
                Cacher = cacherMock.Object,
                Handler = baseHandlerMock.Object
            }.Build();
            
            decorator.HandleCreatedGame(It.IsAny<Game>(), It.IsAny<GameStartData>());
            
            Assert.True(calledLast);
        }

        [Test]
        public void HandleCreateGame_ShouldCallBaseHandleCreateGame_Once()
        {
            var baseHandlerMock = new Mock<IGameCreationHandler>();
            var decorator = new DecoratorBuilder
            {
                Handler = baseHandlerMock.Object
            }.Build();
            
            decorator.HandleCreatedGame(It.IsAny<Game>(), It.IsAny<GameStartData>());
            
            baseHandlerMock.Verify(h => h.HandleCreatedGame(
                It.IsAny<Game>(), It.IsAny<GameStartData>()), Times.Once);
        }

        [Test]
        public void HandleCreateGame_ShouldPassGame_ToBaseHandleCreateGame()
        {
            var baseHandlerMock = new Mock<IGameCreationHandler>();
            var game = GetGame();
            var decorator = new DecoratorBuilder
            {
                Handler = baseHandlerMock.Object
            }.Build();
            
            decorator.HandleCreatedGame(game, It.IsAny<GameStartData>());
            
            baseHandlerMock.Verify(h => 
                h.HandleCreatedGame(game, It.IsAny<GameStartData>()));
        }

        [Test]
        public void HandleCreateGame_ShouldPassGameStartData_ToBaseHandleCreateGame()
        {
            var baseHandlerMock = new Mock<IGameCreationHandler>();
            var gameStartData = GetGameStartData();
            var decorator = new DecoratorBuilder
            {
                Handler = baseHandlerMock.Object
            }.Build();
            
            decorator.HandleCreatedGame(It.IsAny<Game>(), gameStartData);
            
            baseHandlerMock.Verify(h => 
                h.HandleCreatedGame(It.IsAny<Game>(), gameStartData));
        }

        private class DecoratorBuilder
        {
            public IGameCreationHandler Handler { get; set; }
            public IObjectCacher Cacher { get; set; }

            public DecoratorBuilder()
            {
                Handler = new Mock<IGameCreationHandler>().Object;
                Cacher = new Mock<IObjectCacher>().Object;
            }

            public CachingGameCreationHandlerDecorator Build()
            {
                return new CachingGameCreationHandlerDecorator(Handler, Cacher);
            }
        }
    }
}