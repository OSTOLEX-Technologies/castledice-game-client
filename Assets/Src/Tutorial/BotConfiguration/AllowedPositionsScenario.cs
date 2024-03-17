using System;
using System.Collections.Generic;
using castledice_game_logic.Math;
using Vector2IntUnity = UnityEngine.Vector2Int;

namespace Src.Tutorial.BotConfiguration
{
    [Serializable]
    public class AllowedPositionsScenario
    {
        public List<Vector2IntUnity> EnemyPositions;
        public List<Vector2IntUnity> AllowedPositions;
    }
}