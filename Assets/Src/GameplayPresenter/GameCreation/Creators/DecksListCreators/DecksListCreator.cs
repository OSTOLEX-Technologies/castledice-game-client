using System;
using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Decks;

namespace Src.GameplayPresenter.GameCreation.Creators.DecksListCreators
{
    public class DecksListCreator : IDecksListCreator
    {
        public IDecksList GetDecksList(List<PlayerDeckData> decksData)
        {
            var playerIdToDeck = new Dictionary<int, List<PlacementType>>();
            foreach (var data in decksData)
            {
                if (playerIdToDeck.ContainsKey(data.PlayerId))
                {
                    throw new ArgumentException("Deck for player with id " + data.PlayerId + " already exists");
                }
                playerIdToDeck.Add(data.PlayerId, data.AvailablePlacements);
            }

            return new IndividualDecksList(playerIdToDeck);
        }
    }
}