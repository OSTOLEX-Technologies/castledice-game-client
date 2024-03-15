using System;
using System.Collections.Generic;
using castledice_game_logic.Math;

namespace Src.Tutorial.BotConfiguration
{
    [Serializable]
    public class AllowedPositionsScenario
    {
        public List<Vector2Int> EnemyPositions;
        public List<Vector2Int> AllowedPositions;
    }
}