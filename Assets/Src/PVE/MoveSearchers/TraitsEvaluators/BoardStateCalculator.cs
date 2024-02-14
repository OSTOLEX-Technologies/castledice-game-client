using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.PVE.TraitsEvaluators;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public class BoardStateCalculator : IBoardStateCalculator
    {
        private readonly Board _board;
        private readonly bool[,] _connectedCells;

        public BoardStateCalculator(Board board)
        {
            _board = board;
            _connectedCells = new bool[board.GetLength(0), board.GetLength(1)];
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
                    }
                    else
                    {
                        boardState[i, j] = CellState.Enemy;
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
                    boardState[movePosition.X, movePosition.Y] = CellState.Friendly;
                }
            }
            CutUnconnectedEnemyBranches(boardState, player);
            return boardState;
        }
        
        private void CutUnconnectedEnemyBranches(CellState[,] boardState, Player player)
        {
            ResetConnectedCells();
            var currentEnemyBasePositions = GetEnemyBasePositions(player);
            var futureEnemyBasePositions = new List<Vector2Int>();
            foreach (var basePosition in currentEnemyBasePositions)
            {
                if (boardState[basePosition.X, basePosition.Y] == CellState.Enemy)
                {
                    futureEnemyBasePositions.Add(basePosition);
                }
            }
            if (futureEnemyBasePositions.Count == 0)
            {
                SetAllEnemyCellsFree(boardState);
                return;
            }
            foreach (var enemyBasePosition in futureEnemyBasePositions)
            {
                MarkConnectedCells(enemyBasePosition.X, enemyBasePosition.Y, boardState);
            }
            SetNotConnectedEnemyCellsFree(boardState);
        }
        
        private void SetNotConnectedEnemyCellsFree(CellState[,] boardState)
        {
            for (int i = 0; i < _connectedCells.GetLength(0); i++)
            {
                for (int j = 0; j < _connectedCells.GetLength(1); j++)
                {
                    if (boardState[i, j] == CellState.Enemy && !_connectedCells[i, j])
                    {
                        boardState[i, j] = CellState.Free;
                    }
                }
            }
        }

        private void MarkConnectedCells(int x, int y, CellState[,] boardState)
        {
            _connectedCells[x, y] = true;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int nextX = x + i;
                    int nextY = y + j;
                    if (!_board.HasCell(nextX, nextY) || _connectedCells[nextX, nextY]) continue;
                    if (boardState[x, y] == CellState.Enemy && 
                        boardState[nextX, nextY] == CellState.Enemy)
                    {
                        MarkConnectedCells(nextX, nextY, boardState);
                    }
                }
            }
        }

        private void SetAllEnemyCellsFree(CellState[,] boardState)
        {
            for (int i = 0; i < boardState.GetLength(0); i++)
            {
                for (int j = 0; j < boardState.GetLength(1); j++)
                {
                    if (boardState[i, j] == CellState.Enemy)
                    {
                        boardState[i, j] = CellState.Free;
                    }
                }
            }
        }

        private List<Vector2Int> GetEnemyBasePositions(Player player)
        {
            var enemyBasePositions = new List<Vector2Int>();
            foreach (var cell in _board)
            {
                if (cell.HasContent(c => c is CastleGO castle && castle.GetOwner() != player))
                {
                    enemyBasePositions.Add(cell.Position);
                }
            }
            
            return enemyBasePositions;
        }

        private void ResetConnectedCells()
        {
            for (int i = 0; i < _connectedCells.GetLength(0); i++)
            {
                for (int j = 0; j < _connectedCells.GetLength(1); j++)
                {
                    _connectedCells[i, j] = false;
                }
            }
        }
    }
}