using System.Collections.Generic;
using castledice_game_data_logic.Content.Generated;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;

namespace Tests.EditMode
{
    public class CoordinateContentSpawnerProviderTests
    {
        [Test]
        public void GetContentSpawners_ShouldReturnOneElementList_WithCoordinateContentSpawner()
        {
            var provider = new CoordinateContentSpawnerProvider();
            
            var spawners = provider.GetContentSpawnersList(new List<GeneratedContentData>(), new List<Player>());
            
            Assert.AreEqual(1, spawners.Count);
            Assert.IsInstanceOf<CoordinateContentSpawner>(spawners[0]);
        }
    }
}