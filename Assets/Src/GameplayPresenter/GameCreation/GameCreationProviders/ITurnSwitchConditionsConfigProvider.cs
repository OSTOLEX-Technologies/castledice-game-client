using castledice_game_data_logic.TurnSwitchConditions;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public interface ITurnSwitchConditionsConfigProvider
    {
        TurnSwitchConditionsConfig GetTurnSwitchConditionsConfig(TscConfigData data);
    }
}