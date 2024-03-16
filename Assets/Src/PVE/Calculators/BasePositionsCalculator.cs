using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.General.MoveConditions;
using Src.PVE.Checkers;

namespace Src.PVE.Calculators
{
    public class BasePositionsCalculator : IBasePositionsCalculator
    {
        private readonly Board _board;
        private readonly IPlayerBaseChecker _baseChecker;
        private readonly IMoveCondition _baseCaptureChecker;

        public BasePositionsCalculator(Board board, IPlayerBaseChecker baseChecker, IMoveCondition baseCaptureChecker)
        {
            _board = board;
            _baseChecker = baseChecker;
            _baseCaptureChecker = baseCaptureChecker;
        }

        public List<Vector2Int> GetBasePositions(Player forPlayer)
        {
            var result = new List<Vector2Int>();
            foreach (var cell in _board)
            {
                if (cell.GetContent().Exists(content => _baseChecker.IsPlayerBase(content, forPlayer)))
                {
                    result.Add(cell.Position);
                }
            }
            return result;
        }

        public List<Vector2Int> GetBasePositionsAfterMove(Player forPlayer, AbstractMove afterMove)
        {
            var result = GetBasePositions(forPlayer);
            if (_baseCaptureChecker.IsSatisfiedBy(afterMove))
            {
                if (afterMove.Player == forPlayer)
                {
                    result.Add(afterMove.Position);
                }
                else
                {
                    result.Remove(afterMove.Position);
                }
            }
            return result;
        }
    }
}