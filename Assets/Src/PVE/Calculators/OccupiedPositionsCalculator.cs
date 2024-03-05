using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public class OccupiedPositionsCalculator : IOccupiedPositionsCalculator
    {
        private readonly Board _board;
        private readonly ILostPositionsCalculator _lostPositionsCalculator;

        public OccupiedPositionsCalculator(Board board, ILostPositionsCalculator lostPositionsCalculator)
        {
            _board = board;
            _lostPositionsCalculator = lostPositionsCalculator;
        }
        
        public List<Vector2Int> GetOccupiedPositionsAfterMove(Player forPlayer, AbstractMove afterMove)
        {
            var occupiedPositions = GetOccupiedPositions(forPlayer);
            var lostPositions = _lostPositionsCalculator.GetLostPositions(forPlayer, afterMove);
            foreach (var lostPosition in lostPositions)
            {
                occupiedPositions.Remove(lostPosition);
            }
            return occupiedPositions;
        }

        public List<Vector2Int> GetOccupiedPositions(Player forPlayer)
        {
            var occupiedPositions = new List<Vector2Int>();
            foreach (var cell in _board)
            {
                if (CellHasPlayerOwnedContent(cell, forPlayer))
                {
                    occupiedPositions.Add(cell.Position);
                }
            }
            return occupiedPositions;
        }

        private static bool CellHasPlayerOwnedContent(Cell cell, Player player)
        {
            return cell.HasContent(c => c is IPlayerOwned owned && owned.GetOwner() == player);
        }
    }
}