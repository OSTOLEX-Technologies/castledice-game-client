using castledice_game_data_logic.ConfigsData;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public interface IPlaceablesConfigProvider
    {
        PlaceablesConfig GetPlaceablesConfig(PlaceablesConfigData configData);
    }
}