﻿using castledice_game_data_logic.TurnSwitchConditions;
using castledice_game_logic.GameConfiguration;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public class TurnSwitchConditionsConfigProvider : ITurnSwitchConditionsConfigProvider
    {
        public TurnSwitchConditionsConfig GetTurnSwitchConditionsConfig(TscConfigData data)
        {
            return new TurnSwitchConditionsConfig(data.TscTypes);
        }
    }
}