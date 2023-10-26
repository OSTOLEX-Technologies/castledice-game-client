using castledice_game_data_logic.Content.Placeable;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation
{
    public interface IPlaceablesConfigProvider
    {
        PlaceablesConfig GetPlaceablesConfig(PlaceablesConfigData configData);
    }
}