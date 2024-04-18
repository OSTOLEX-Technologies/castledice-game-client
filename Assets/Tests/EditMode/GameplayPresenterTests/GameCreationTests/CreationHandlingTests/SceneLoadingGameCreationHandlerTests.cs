using System;
using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.Components;
using Src.GameplayPresenter.GameCreation.CreationHandling;
using Src.General.LoadingScenes;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreationHandlingTests
{
    public class SceneLoadingGameCreationHandlerTests
    {
        [Test]
        [TestCaseSource(nameof(SceneTypes))]
        public void HandleCreatedGame_ShouldLoadSceneWithTransition_WithGivenType(SceneType type)
        {
            var sceneLoaderMock = new Mock<ISceneLoader>();
            var handler = new SceneLoadingGameCreationHandler(sceneLoaderMock.Object, type);
            
            handler.HandleCreatedGame(It.IsAny<Game>(), It.IsAny<GameStartData>());
            
            sceneLoaderMock.Verify(s => s.LoadSceneWithTransition(type));       
        }

        public static IEnumerable<SceneType> SceneTypes()
        {
            var values = (SceneType[])Enum.GetValues(typeof(SceneType));
            foreach (var type in values)
            {
                yield return type;
            }
        }
    }
}