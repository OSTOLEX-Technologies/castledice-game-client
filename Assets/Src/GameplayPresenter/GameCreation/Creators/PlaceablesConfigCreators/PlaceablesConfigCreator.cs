using castledice_game_data_logic.ConfigsData;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects.Configs;

namespace Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators
{
    public class PlaceablesConfigCreator : IPlaceablesConfigCreator
    {
        public PlaceablesConfig GetPlaceablesConfig(PlaceablesConfigData configData)
        {
            return new PlaceablesConfig(new KnightConfig(configData.KnightConfig.PlacementCost, configData.KnightConfig.Health));
        }
    }
}