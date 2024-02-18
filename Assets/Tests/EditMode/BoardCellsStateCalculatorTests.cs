using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic;
using NUnit.Framework;
using Src.PVE.MoveSearchers.TraitsEvaluators;
using Src.PVE.TraitsEvaluators;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode
{
    public class BoardCellsStateCalculatorTests
    {
        [Test]
        public void GetCurrentStateTest()
        {
            var board = GetFullNByNBoard(4);
            var playerCastle = GetCastle();
            var enemyCastle = GetCastle();
            board[0, 0].AddContent(playerCastle);
            board[3, 3].AddContent(enemyCastle);
            board[2, 2].AddContent(GetKnight());
            board[1, 1].AddContent(GetKnight());
            var boardStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>());
            var expected = new CellState[,]
            {
                {CellState.FriendlyBase, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Enemy, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Enemy, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.EnemyBase}
            };
            
            var actual = boardStateCalculator.GetCurrentBoardState(playerCastle.GetOwner());
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBoardStateAfterPlayerMoveTest()
        {
            var board = GetFullNByNBoard(6);
            var player = GetPlayer();
            var enemy = GetPlayer();
            var playerCastle = GetCastle(player);
            var enemyCastle = GetCastle(enemy);
            board[0, 0].AddContent(playerCastle);
            board[5, 5].AddContent(enemyCastle);
            board[4, 4].AddContent(GetKnight(enemy));
            board[3, 3].AddContent(GetKnight(enemy));
            board[2, 2].AddContent(GetKnight(enemy));
            board[1, 1].AddContent(GetKnight(enemy));
            var boardStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>());
            var expected = new CellState[,] 
            {
                {CellState.FriendlyBase, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Friendly, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.EnemyBase}
            };
            
            var actual = boardStateCalculator.GetBoardStateAfterPlayerMove(new ReplaceMove(player, (4, 4), GetKnight(player)));
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBoardStateAfterCaptureMoveTest()
        {
            var board = GetFullNByNBoard(6);
            var player = GetPlayer();
            var enemy = GetPlayer();
            var playerCastle = GetCastle(player, 3, 3, 1, 1);
            var enemyCastle = GetCastle(enemy, 1, 3, 1, 1);
            board[0, 0].AddContent(playerCastle);
            board[5, 5].AddContent(enemyCastle);
            board[4, 4].AddContent(GetKnight(player));
            board[3, 3].AddContent(GetKnight(player));
            board[2, 2].AddContent(GetKnight(player));
            board[1, 1].AddContent(GetKnight(player));
            board[1, 2].AddContent(GetKnight(enemy));
            board[2, 1].AddContent(GetKnight(enemy));
            var boardStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>());
            var expected = new CellState[,] 
            {
                {CellState.FriendlyBase, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Friendly, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Friendly, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Friendly, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Friendly, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.FriendlyBase}
            };
            var move = new CaptureMove(player, (5, 5));
            
            var actual = boardStateCalculator.GetBoardStateAfterPlayerMove(move);
            
            Assert.AreEqual(expected, actual);
        }
    }
}