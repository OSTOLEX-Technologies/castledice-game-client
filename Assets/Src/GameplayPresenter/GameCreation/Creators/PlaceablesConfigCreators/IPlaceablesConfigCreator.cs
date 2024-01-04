using castledice_game_data_logic.ConfigsData;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators
{
    public interface IPlaceablesConfigCreator
    {
        PlaceablesConfig GetPlaceablesConfig(PlaceablesConfigData configData);
    }
}