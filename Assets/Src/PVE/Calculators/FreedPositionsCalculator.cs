using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public class FreedPositionsCalculator : IFreedPositionsCalculator
    {
        private readonly ILostPositionsCalculator _lostPositionsCalculator;

        public FreedPositionsCalculator(ILostPositionsCalculator lostPositionsCalculator)
        {
            _lostPositionsCalculator = lostPositionsCalculator;
        }

        public List<Vector2Int> GetFreedPositions(AbstractMove afterMove)
        {
            throw new System.NotImplementedException();
        }
    }
}