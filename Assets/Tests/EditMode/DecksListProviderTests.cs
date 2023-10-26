﻿using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_logic.GameObjects;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;

namespace Tests.EditMode
{
    public class DecksListProviderTests
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
            var decksListProvider = new DecksListProvider();
            
            var decksList = decksListProvider.GetDecksList(decksData);
            
            Assert.That(decksList, Has.Count.EqualTo(decksData.Count));
            foreach (var data in decksData)
            {
                var deck = decksList.GetDeck(data.PlayerId);
                foreach (var placement in data.AvailablePlacements)
                {
                    Assert.Contains(placement, deck);
                }
            }
        }
    }
}