using System.Collections.Generic;
using castledice_game_logic;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreatorsTests.PlayersListCreatorsTests
{
    public class PlayersListProvidersTests
    {
        public static List<int>[] IdLists =
        {
            new () { 1, 2, 3, 4, 5 },
            new () { 3, 11, 234 }
        };
        
        [Test]
        public void GetPlayersList_ShouldReturnListOfPlayers_WithAppropriateIds([ValueSource(nameof(IdLists))]List<int> ids)
        {
            var playersListProvider = new PlayersListProvider();
            
            var playersList = playersListProvider.GetPlayersList(ids);
            
            Assert.That(playersList, Has.Count.EqualTo(ids.Count));
            foreach (var id in ids)
            {
                Assert.That(playersList, Has.Exactly(1).Matches<Player>(p => p.Id == id));
            }
        }
    }
}