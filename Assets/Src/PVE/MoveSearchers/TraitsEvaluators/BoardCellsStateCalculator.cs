using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.PVE.TraitsEvaluators;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public class BoardCellsStateCalculator : IBoardCellsStateCalculator
    {
        private readonly Board _board;
        private readonly DfsUnconnectedValuesCutter _unconnectedValuesCutter;

        public BoardCellsStateCalculator(Board board, DfsUnconnectedValuesCutter unconnectedValuesCutter)
        {
            _board = board;
            _unconnectedValuesCutter = unconnectedValuesCutter;
        }

        public CellState[,] GetCurrentBoardState(Player player)
        {
            var boardState = new CellState[_board.GetLength(0), _board.GetLength(1)];
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    var cell = _board[i, j];
                    if (!cell.HasContent(c => c is IPlayerOwned))
                    {
                        boardState[i, j] = CellState.Free;
                    }
                    else if (cell.HasContent(c => c is IPlayerOwned playerOwned && playerOwned.GetOwner() == player))
                    {
                        boardState[i, j] = CellState.Friendly;
                        if (cell.HasContent(c => c is CastleGO))
                        {
                            boardState[i, j] = CellState.FriendlyBase;
                        }
                    }
                    else
                    {
                        boardState[i, j] = CellState.Enemy;
                        if (cell.HasContent(c => c is CastleGO))
                        {
                            boardState[i, j] = CellState.EnemyBase;
                        }
                    }
                }
            }

            return boardState;
        }

        public CellState[,] GetBoardStateAfterPlayerMove(AbstractMove move)
        {
            var player = move.Player;
            var boardState = GetCurrentBoardState(player);
            var movePosition = move.Position;
            if (move is PlaceMove or ReplaceMove)
            {
                boardState[movePosition.X, movePosition.Y] = CellState.Friendly;
            }
            else if (move is CaptureMove)
            {
                //Checking if this capture move will capture enemy castle
                var cell = _board[movePosition.X, movePosition.Y];
                var castle = (CastleGO) cell.GetContent().Find(c => c is CastleGO);
                if (castle.GetDurability() <= castle.GetCaptureHitCost())
                {
                    boardState[movePosition.X, movePosition.Y] = CellState.FriendlyBase;
                }
            }
            var boardStateWithCutUnconnected = _unconnectedValuesCutter.CutUnconnectedUnits(boardState, CellState.Enemy, CellState.EnemyBase, CellState.Free);
            return boardStateWithCutUnconnected;
        }
        
    }
}