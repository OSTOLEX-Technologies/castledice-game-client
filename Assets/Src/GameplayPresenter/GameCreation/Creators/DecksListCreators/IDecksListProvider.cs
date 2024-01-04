using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_logic.GameObjects;

namespace Src.GameplayPresenter.GameCreation.Creators.DecksListCreators
{
    public interface IDecksListProvider
    {
        IDecksList GetDecksList(List<PlayerDeckData> decksData);
    }
}