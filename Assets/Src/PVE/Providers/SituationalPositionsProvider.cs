using System.Collections.Generic;
using castledice_game_logic.Math;
using Src.General;
using Src.PVE.GameSituations;

namespace Src.PVE.Providers
{
    public class SituationalPositionsProvider : IPositionsProvider
    {
        private readonly Dictionary<IGameSituation, List<Vector2Int>> _situationToPositions;

        public SituationalPositionsProvider(Dictionary<IGameSituation, List<Vector2Int>> situationToPositions)
        {
            _situationToPositions = situationToPositions;
        }

        public List<Vector2Int> GetPositions()
        {
            foreach (var situationToPosition in _situationToPositions)
            {
                if (situationToPosition.Key.IsSituation())
                {
                    return situationToPosition.Value;
                }
            }
            return new List<Vector2Int>();
        }
    }
}