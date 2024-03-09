using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public class LostPositionsCalculator : ILostPositionsCalculator
    {
        private readonly IUnitsPositionsSearcher _unitsPositionsSearcher;
        private readonly IUnconnectedValuesCutter<SimpleCellState> _unconnectedValuesCutter;
        private readonly IBasePositionsCalculator _basePositionsCalculator;

        public LostPositionsCalculator(IUnitsPositionsSearcher unitsPositionsSearcher, IUnconnectedValuesCutter<SimpleCellState> unconnectedValuesCutter, IBasePositionsCalculator basePositionsCalculator)
        {
            _unitsPositionsSearcher = unitsPositionsSearcher;
            _unconnectedValuesCutter = unconnectedValuesCutter;
            _basePositionsCalculator = basePositionsCalculator;
        }

        public List<Vector2Int> GetLostPositions(Player forPlayer, AbstractMove afterMove)
        {
            throw new System.NotImplementedException();
        }
    }
}