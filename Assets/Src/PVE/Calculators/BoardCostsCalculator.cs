using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public class BoardCostsCalculator : IBoardCostsCalculator
    {
        private readonly Board _board;
        private readonly IFreedPositionsCalculator _freedPositionsCalculator;
        private readonly int _defaultCost;
        private readonly int _minimalPlaceCost;

        public BoardCostsCalculator(Board board, IFreedPositionsCalculator freedPositionsCalculator, int defaultCost, int minimalPlaceCost)
        {
            _board = board;
            _freedPositionsCalculator = freedPositionsCalculator;
            _defaultCost = defaultCost;
            _minimalPlaceCost = minimalPlaceCost;
        }

        public int[,] GetCosts(Player forPlayer)
        {
            var costs = new int[_board.GetLength(0), _board.GetLength(1)];
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    var cell = _board[i, j];
                    var cost = GetCostForCell(cell, forPlayer);
                    costs[i, j] = cost;
                }
            }
            return costs;
        }

        private int GetCostForCell(Cell cell, Player costFor)
        {
            if (!cell.HasContent())
            {
                return _minimalPlaceCost;
            }
            if (cell.HasContent(c => c is IPlayerOwned owned && owned.GetOwner() == costFor))
            {
                return int.MaxValue;
            }
            if (cell.HasContent(c => c is IRemovable))
            {
                var removable = cell.GetContent().Find(c => c is IRemovable) as IRemovable;
                if (removable.CanBeRemoved())
                {
                    return removable.GetRemoveCost();
                }

                return int.MaxValue;
            }
            if (cell.HasContent(c => c is IReplaceable))
            {
                var replaceable = cell.GetContent().Find(c => c is IReplaceable) as IReplaceable;
                return replaceable.GetReplaceCost();
            }
            if (cell.HasContent(c => c is ICapturable))
            {
                var capturable = cell.GetContent().Find(c => c is ICapturable) as ICapturable;
                return capturable.GetCaptureHitCost(costFor);
            }
            return _defaultCost;
        }

        public int[,] GetCostsAfterMove(Player forPlayer, AbstractMove afterMove)
        {
            var costs = GetCosts(forPlayer);
            var movePosition = afterMove.Position;
            if (afterMove is PlaceMove placeMove)
            {
                var placement = placeMove.ContentToPlace;
                var cost = GetPlaceableCost(placement, forPlayer);
                costs[movePosition.X, movePosition.Y] = cost;
            }
            else if (afterMove is ReplaceMove replaceMove)
            {
                var replacement = replaceMove.Replacement;
                var cost = GetPlaceableCost(replacement, forPlayer);
                costs[movePosition.X, movePosition.Y] = cost;
            }
            var freedPositions = _freedPositionsCalculator.GetFreedPositions(afterMove);
            foreach (var freedPosition in freedPositions)
            {
                costs[freedPosition.X, freedPosition.Y] = _minimalPlaceCost;
            }
            return costs;
        }

        private int GetPlaceableCost(IPlaceable placeable, Player forPlayer)
        {
            if (placeable is IPlayerOwned owned && owned.GetOwner() == forPlayer)
            {
                return int.MaxValue;
            }
            if (placeable is IReplaceable replaceable)
            {
                return replaceable.GetReplaceCost();
            }
            if (placeable is IRemovable removable)
            {
                return removable.CanBeRemoved() ? removable.GetRemoveCost() : int.MaxValue; 
            }
            if (placeable is ICapturable capturable)
            {
                return capturable.GetCaptureHitCost(forPlayer);
            }
            return _defaultCost;
        }
    }
}