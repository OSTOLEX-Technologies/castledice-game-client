using System;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic;

namespace Src.OLDPVE.MoveSearchers.TraitsEvaluators
{
    public class BoardCellsCostCalculator : IBoardCellsCostCalculator
    {
        private readonly Board _board;
        private readonly IBoardCellsStateCalculator _boardCellsStateCalculator;
        private readonly int _enemyCapturableContentCost;
        private readonly int _minimumPlaceCost;
        private readonly int _notRemovableObstaclesCost;

        private readonly int _ownContentCost;
        private readonly int _undefinedCellCost;

        public BoardCellsCostCalculator(
            Board board,
            IBoardCellsStateCalculator boardCellsStateCalculator,
            int minimumPlaceCost = 1,
            int ownContentCost = 1000,
            int enemyCapturableContentCost = 1000,
            int notRemovableObstaclesCost = 1000,
            int undefinedCellCost = 1000)
        {
            _board = board;
            _boardCellsStateCalculator = boardCellsStateCalculator;
            _minimumPlaceCost = minimumPlaceCost;
            _ownContentCost = ownContentCost;
            _enemyCapturableContentCost = enemyCapturableContentCost;
            _notRemovableObstaclesCost = notRemovableObstaclesCost;
            _undefinedCellCost = undefinedCellCost;
        }


        public int[,] GetCellsCostsAfterMove(AbstractMove move)
        {
            var player = move.Player;
            var cellsCosts = GetCellsCosts(player);
            var boardStateAfterMove = _boardCellsStateCalculator.GetBoardStateAfterPlayerMove(move);
            for (var i = 0; i < _board.GetLength(0); i++)
            for (var j = 0; j < _board.GetLength(1); j++)
            {
                if (boardStateAfterMove[i, j] == CellState.Free) cellsCosts[i, j] = _minimumPlaceCost;

                if (boardStateAfterMove[i, j] == CellState.Friendly) cellsCosts[i, j] = _ownContentCost;

                if (boardStateAfterMove[i, j] == CellState.FriendlyBase) cellsCosts[i, j] = _ownContentCost;
            }

            return cellsCosts;
        }

        public int[,] GetInverseCellsCostsAfterMove(AbstractMove move)
        {
            var player = move.Player;
            var cellsCosts = GetInverseCellsCosts(player);
            var boardStateAfterMove = _boardCellsStateCalculator.GetBoardStateAfterPlayerMove(move);
            for (var i = 0; i < _board.GetLength(0); i++)
            for (var j = 0; j < _board.GetLength(1); j++)
            {
                if (boardStateAfterMove[i, j] == CellState.Free) cellsCosts[i, j] = _minimumPlaceCost;

                if (boardStateAfterMove[i, j] == CellState.Friendly)
                {
                    var cell = _board[i, j];
                    var cellCost = GetEnemyCellCost(cell);
                    cellsCosts[i, j] = cellCost;
                }

                if (boardStateAfterMove[i, j] == CellState.FriendlyBase) cellsCosts[i, j] = _enemyCapturableContentCost;
            }

            return cellsCosts;
        }

        public int[,] GetCellsCosts(Player player)
        {
            return GetCellsCost(player, GetCellCost);
        }

        public int[,] GetInverseCellsCosts(Player player)
        {
            return GetCellsCost(player, GetInverseCellCost);
        }

        private int[,] GetCellsCost(Player player, Func<Cell, Player, int> getCellCost)
        {
            var costsGraph = new int[_board.GetLength(0), _board.GetLength(1)];
            for (var i = 0; i < _board.GetLength(0); i++)
            for (var j = 0; j < _board.GetLength(1); j++)
            {
                var cell = _board[i, j];
                costsGraph[i, j] = getCellCost(cell, player);
            }

            return costsGraph;
        }

        public int GetInverseCellCost(Cell cell, Player player)
        {
            return GetCellCost(cell, player, GetInversePlayerOwnedContentCost);
        }

        public int GetCellCost(Cell cell, Player player)
        {
            return GetCellCost(cell, player, GetPlayerOwnedContentCost);
        }

        private int GetCellCost(Cell cell, Player player, Func<Cell, Player, int> getPlayerOwnedContentCost)
        {
            if (CellIsEmpty(cell)) return _minimumPlaceCost;
            if (CellHasPlayerOwnedContent(cell)) return getPlayerOwnedContentCost(cell, player);
            if (CellHasRemovableContent(cell))
            {
                var removable = cell.GetContent().Find(c => c is IRemovable) as IRemovable;
                if (removable!.CanBeRemoved()) return _notRemovableObstaclesCost;
                return removable.GetRemoveCost();
            }

            return _undefinedCellCost;
        }

        private int GetPlayerOwnedContentCost(Cell cell, Player player)
        {
            if (CellHasPlayerContent(cell, player)) return _ownContentCost;
            if (CellHasEnemyContent(cell, player)) return GetEnemyCellCost(cell);
            return _undefinedCellCost;
        }

        private int GetInversePlayerOwnedContentCost(Cell cell, Player player)
        {
            if (CellHasPlayerContent(cell, player)) return GetEnemyCellCost(cell);
            if (CellHasEnemyContent(cell, player)) return _ownContentCost;
            return _undefinedCellCost;
        }

        private int GetEnemyCellCost(Cell cell)
        {
            if (CellHasCapturableContent(cell)) return _enemyCapturableContentCost;
            if (CellHasReplaceableContent(cell))
            {
                var replaceable = cell.GetContent().Find(c => c is IReplaceable) as IReplaceable;
                return replaceable!.GetReplaceCost() + _minimumPlaceCost;
            }

            return _undefinedCellCost;
        }

        #region Boolean functions for cells content

        private static bool CellIsEmpty(Cell cell)
        {
            return !cell.HasContent();
        }

        private static bool CellHasPlayerOwnedContent(Cell cell)
        {
            return cell.HasContent(c => c is IPlayerOwned);
        }

        private static bool CellHasPlayerContent(Cell cell, Player player)
        {
            return cell.HasContent(c => c is IPlayerOwned playerOwned && playerOwned.GetOwner() == player);
        }

        private static bool CellHasEnemyContent(Cell cell, Player player)
        {
            return cell.HasContent(c => c is IPlayerOwned playerOwned && playerOwned.GetOwner() != player);
        }

        private static bool CellHasReplaceableContent(Cell cell)
        {
            return cell.HasContent(c => c is IReplaceable);
        }

        private static bool CellHasCapturableContent(Cell cell)
        {
            return cell.HasContent(c => c is ICapturable);
        }

        private static bool CellHasRemovableContent(Cell cell)
        {
            return cell.HasContent(c => c is IRemovable);
        }

        #endregion
    }
}