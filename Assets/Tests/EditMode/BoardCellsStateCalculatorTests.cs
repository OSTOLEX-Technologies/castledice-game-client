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
            var boardStateCalculator = new BoardCellsStateCalculator(board);
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
            var playerCastle = GetCastle();
            var enemyCastle = GetCastle();
            board[0, 0].AddContent(playerCastle);
            board[5, 5].AddContent(enemyCastle);
            board[4, 4].AddContent(GetKnight());
            board[3, 3].AddContent(GetKnight());
            board[2, 2].AddContent(GetKnight());
            board[1, 1].AddContent(GetKnight());
            var boardStateCalculator = new BoardCellsStateCalculator(board);
            var expected = new CellState[,] 
            {
                {CellState.FriendlyBase, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Friendly, CellState.Free},
                {CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.Free, CellState.EnemyBase}
            };
            
            var actual = boardStateCalculator.GetBoardStateAfterPlayerMove(new ReplaceMove(playerCastle.GetOwner(), (4, 4), new Knight(playerCastle.GetOwner(), 1, 1)));
            
            Assert.AreEqual(expected, actual);
        }
    }
}