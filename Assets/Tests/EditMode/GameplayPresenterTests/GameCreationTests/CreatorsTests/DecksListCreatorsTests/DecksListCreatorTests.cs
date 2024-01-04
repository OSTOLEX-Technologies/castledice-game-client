using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_logic.GameObjects;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.Creators.DecksListCreators;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreatorsTests.DecksListCreatorsTests
{
    public class DecksListCreatorTests
    {
        public static List<PlayerDeckData>[] DeckDataLists =
        {
            new()
            {
                new PlayerDeckData(1, new List<PlacementType> { PlacementType.Knight , PlacementType.HeavyKnight }),
                new PlayerDeckData(2, new List<PlacementType> { PlacementType.HeavyKnight }),
            },
            new()
            {
                new PlayerDeckData(1, new List<PlacementType> { PlacementType.Knight , PlacementType.HeavyKnight }),
                new PlayerDeckData(2, new List<PlacementType> { PlacementType.HeavyKnight }),
                new PlayerDeckData(3, new List<PlacementType> { PlacementType.Knight , PlacementType.HeavyKnight }),
                new PlayerDeckData(4, new List<PlacementType> { PlacementType.HeavyKnight }),
            }
        };
        
        [Test]
        public void GetDecksList_ShouldReturnDecksList_WithAppropriateDeckForEachPlayerId([ValueSource(nameof(DeckDataLists))]List<PlayerDeckData> decksData)
        {
            var decksListCreator = new DecksListCreator();
            
            var decksList = decksListCreator.GetDecksList(decksData);
            
            foreach (var data in decksData)
            {
                var deck = decksList.GetDeck(data.PlayerId);
                foreach (var placement in data.AvailablePlacements)
                {
                    Assert.Contains(placement, deck);
                }
            }
        }
        
        [Test]
        public void GetDecksList_ShouldThrowArgumentException_IfThereAreDuplicatePlayerIds()
        {
            var decksData = new List<PlayerDeckData>
            {
                new(1, new List<PlacementType> { PlacementType.Knight , PlacementType.HeavyKnight }),
                new(1, new List<PlacementType> { PlacementType.HeavyKnight }),
            };
            var decksListCreator = new DecksListCreator();
            
            Assert.Throws<System.ArgumentException>(() => decksListCreator.GetDecksList(decksData));
        }
    }
}