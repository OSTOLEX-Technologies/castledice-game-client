using castledice_game_data_logic.TurnSwitchConditions;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators
{
    public interface ITurnSwitchConditionsConfigProvider
    {
        TurnSwitchConditionsConfig GetTurnSwitchConditionsConfig(TscConfigData data);
    }
}