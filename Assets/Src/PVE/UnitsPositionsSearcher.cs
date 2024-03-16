using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using Src.PVE.Checkers;

namespace Src.PVE
{
    public class UnitsPositionsSearcher : IUnitsPositionsSearcher
    {
        private readonly Board _board;
        private readonly IPlayerUnitChecker _unitChecker;
        
        public UnitsPositionsSearcher(Board board, IPlayerUnitChecker unitChecker)
        {
            _board = board;
            _unitChecker = unitChecker;
        }
        
        public List<Vector2Int> GetUnitsPositions(Player forPlayer)
        {
            var result = new List<Vector2Int>();
            foreach (var cell in _board)
            {
                if (cell.GetContent().Exists(content => _unitChecker.IsPlayerUnit(content, forPlayer)))
                {
                    result.Add(cell.Position);
                }
            }
            return result;
        }
    }
}