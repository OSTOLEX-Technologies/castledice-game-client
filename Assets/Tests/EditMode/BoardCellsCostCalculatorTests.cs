using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.PVE.MoveSearchers.TraitsEvaluators;
using Src.PVE.TraitsEvaluators;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode
{
    public class BoardCellsCostCalculatorTests
    {
        private class CellsCostTestCase
        {
            public Board Board;
            public Player Player;
            public int[,] ExpectedMatrix;
            public int MinimumPlaceCost;
            public int OwnContentCost;
            public int EnemyCapturableContentCost;
            public int NotRemovableObstaclesCost;
            public int UndefinedCellCost;
        }
        
        private class CellsCostAfterMoveTestCase : CellsCostTestCase
        {
            public BoardCellsStateCalculator BoardCellsStateCalculator;
            public AbstractMove Move;
        }
        

        [Test]
        public void GetCellsCosts_ShouldReturnCorrectCellsCostMatrix([Range(1, 3)] int testCaseNumber)
        {
            var testCase = GetCellsCostsTestCase(testCaseNumber);
            var minimumPlaceCost = testCase.MinimumPlaceCost;
            var ownContentCost = testCase.OwnContentCost;
            var enemyCapturableContentCost = testCase.EnemyCapturableContentCost;
            var notRemovableObstaclesCost = testCase.NotRemovableObstaclesCost;
            var undefinedCellCost = testCase.UndefinedCellCost;
            var boardCellsCostCalculator = new BoardCellsCostCalculator(
                testCase.Board, 
                new Mock<IBoardCellsStateCalculator>().Object, 
                minimumPlaceCost, 
                ownContentCost, 
                enemyCapturableContentCost, 
                notRemovableObstaclesCost, 
                undefinedCellCost);
            var expected = testCase.ExpectedMatrix;
            var actual = boardCellsCostCalculator.GetCellsCosts(testCase.Player);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetInverseCellsCosts_ShouldReturnCorrectCellsCostMatrix([Range(1, 3)] int testCaseNumber)
        {
            var testCase = GetInverseCellsCostsTestCase(testCaseNumber);
            var minimumPlaceCost = testCase.MinimumPlaceCost;
            var ownContentCost = testCase.OwnContentCost;
            var enemyCapturableContentCost = testCase.EnemyCapturableContentCost;
            var notRemovableObstaclesCost = testCase.NotRemovableObstaclesCost;
            var undefinedCellCost = testCase.UndefinedCellCost;
            var boardCellsCostCalculator = new BoardCellsCostCalculator(
                testCase.Board, 
                new Mock<IBoardCellsStateCalculator>().Object, 
                minimumPlaceCost, 
                ownContentCost, 
                enemyCapturableContentCost, 
                notRemovableObstaclesCost, 
                undefinedCellCost);
            var expected = testCase.ExpectedMatrix;
            var actual = boardCellsCostCalculator.GetInverseCellsCosts(testCase.Player);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetCellsCostsAfterMove_ShouldReturnCorrectCellsCostMatrix([Range(1, 5)] int testCaseNumber)
        {
            var testCase = GetCellsCostsAfterMoveTestCase(testCaseNumber);
            var move = testCase.Move;
            var boardStateCalculator = testCase.BoardCellsStateCalculator;
            var minimumPlaceCost = testCase.MinimumPlaceCost;
            var ownContentCost = testCase.OwnContentCost;
            var enemyCapturableContentCost = testCase.EnemyCapturableContentCost;
            var notRemovableObstaclesCost = testCase.NotRemovableObstaclesCost;
            var undefinedCellCost = testCase.UndefinedCellCost;
            var boardCellsCostCalculator = new BoardCellsCostCalculator(
                testCase.Board, 
                boardStateCalculator, 
                minimumPlaceCost, 
                ownContentCost, 
                enemyCapturableContentCost, 
                notRemovableObstaclesCost, 
                undefinedCellCost);
            var expected = testCase.ExpectedMatrix;
            var actual = boardCellsCostCalculator.GetCellsCostsAfterMove(move);
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetInverseCellsCostsAfterMove_ShouldReturnCorrectCellsCostMatrix([Range(1, 5)] int testCaseNumber)
        {
            var testCase = GetInverseCellsCostsAfterMoveTestCase(testCaseNumber);
            var move = testCase.Move;
            var boardStateCalculator = testCase.BoardCellsStateCalculator;
            var minimumPlaceCost = testCase.MinimumPlaceCost;
            var ownContentCost = testCase.OwnContentCost;
            var enemyCapturableContentCost = testCase.EnemyCapturableContentCost;
            var notRemovableObstaclesCost = testCase.NotRemovableObstaclesCost;
            var undefinedCellCost = testCase.UndefinedCellCost;
            var boardCellsCostCalculator = new BoardCellsCostCalculator(
                testCase.Board, 
                boardStateCalculator, 
                minimumPlaceCost, 
                ownContentCost, 
                enemyCapturableContentCost, 
                notRemovableObstaclesCost, 
                undefinedCellCost);
            var expected = testCase.ExpectedMatrix;
            var actual = boardCellsCostCalculator.GetInverseCellsCostsAfterMove(move);
            
            Assert.AreEqual(expected, actual);
        }

        private CellsCostAfterMoveTestCase GetInverseCellsCostsAfterMoveTestCase(int testCaseNumber)
        {
            int freeCellCost = 1;
            int ownContentCost = 1002;
            int enemyCapturableContentCost = 1003;
            int notRemovableObstaclesCost = 1004;
            int undefinedCellCost = 1005;
            //First case with 2 by 2 field and 1 player castle on it.
            //The move places a friendly knight on the board.]
            if (testCaseNumber == 1)
            {
                var board = GetFullNByNBoard(2);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3 ,1, 1);
                var playerKnight = GetKnight(player, 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[1, 1].AddContent(playerKnight);
                return new CellsCostAfterMoveTestCase
                {
                    Board = board,
                    Player = player,
                    Move = new PlaceMove(player, (1, 1), playerKnight),
                    ExpectedMatrix = new [,] 
                    {
                        {enemyCapturableContentCost, freeCellCost},
                        {freeCellCost, 3}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost,
                    BoardCellsStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>())
                };
            }
            //Second case with 3 by 3 field, 1 player castle, 1 enemy castle and 1 enemy knight on it.
            //The move replaces enemy knight with player knight
            if (testCaseNumber == 2)
            {
                var board = GetFullNByNBoard(3);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3 ,1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 3, 3, 1, 1);;
                var playerKnight = GetKnight(player, 1, 2);
                var enemyKnight = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[2, 2].AddContent(enemyCastle);
                board[1, 1].AddContent(enemyKnight);
                return new CellsCostAfterMoveTestCase
                {
                    Board = board,
                    Player = player,
                    Move = new ReplaceMove(player, (1, 1), playerKnight),
                    ExpectedMatrix = new [,] 
                    {
                        {enemyCapturableContentCost, freeCellCost, freeCellCost},
                        {freeCellCost, 3, freeCellCost},
                        {freeCellCost, freeCellCost, ownContentCost}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost,
                    BoardCellsStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>())
                };
            }
            //Third case with 4 by 4 field, 1 player castle, 1 enemy castle, 2 enemy knights and 2 player knights on it.
            //The move replaces the root enemy knight and hence cuts other enemy knight.
            if (testCaseNumber == 3)
            {
                var board = GetFullNByNBoard(4);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3 ,1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 3, 3, 1, 1);;
                var playerKnight1 = GetKnight(player, 1, 2);
                var playerKnight2 = GetKnight(player, 1 ,2);
                var enemyKnight1 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight2 = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[3, 3].AddContent(enemyCastle);
                board[1, 0].AddContent(playerKnight1);
                board[2, 1].AddContent(playerKnight2);
                board[1, 1].AddContent(enemyKnight1);
                board[2, 2].AddContent(enemyKnight2);
                return new CellsCostAfterMoveTestCase
                {
                    Board = board,
                    Player = player,
                    Move = new ReplaceMove(player, (2, 2), playerKnight1),
                    ExpectedMatrix = new [,] 
                    {
                        {enemyCapturableContentCost, freeCellCost, freeCellCost, freeCellCost},
                        {3, freeCellCost, freeCellCost, freeCellCost},
                        {freeCellCost, 3, 3, freeCellCost},
                        {freeCellCost, freeCellCost, freeCellCost, ownContentCost}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost,
                    BoardCellsStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>())
                };
            }
            //The fourth case is similar to previous one, except there are 3 player knights and the move tries to capture enemy castle.
            //The capture is not successful and hence no costs should change after the move.
            if(testCaseNumber == 4)
            {
                var board = GetFullNByNBoard(4);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3, 1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 3, 3, 1, 1);
                var playerKnight1 = GetKnight(player, 1, 2);
                var playerKnight2 = GetKnight(player, 1 ,2);
                var playerKnight3 = GetKnight(player, 1 ,2);
                var enemyKnight1 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight2 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight3 = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[3, 3].AddContent(enemyCastle);
                board[1, 0].AddContent(playerKnight1);
                board[2, 1].AddContent(playerKnight2);
                board[3, 2].AddContent(playerKnight3);
                board[1, 1].AddContent(enemyKnight1);
                board[2, 2].AddContent(enemyKnight2);
                board[3, 3].AddContent(enemyKnight3);
                return new CellsCostAfterMoveTestCase
                {
                    Board = board,
                    Player = player,
                    Move = new CaptureMove(player, (3, 3)),
                    ExpectedMatrix = new [,] 
                    {
                        {enemyCapturableContentCost, freeCellCost, freeCellCost, freeCellCost},
                        {3, ownContentCost, freeCellCost, freeCellCost},
                        {freeCellCost, 3, ownContentCost, freeCellCost},
                        {freeCellCost, freeCellCost, 3, ownContentCost}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost,
                    BoardCellsStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>())
                };
            }
            //The last case is the same as fourth, however here player successfully caputres the enemy castle.
            else
            { 
                var board = GetFullNByNBoard(4);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3, 1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 1, 3, 1, 1);
                var playerKnight1 = GetKnight(player, 1, 2);
                var playerKnight2 = GetKnight(player, 1 ,2);
                var playerKnight3 = GetKnight(player, 1 ,2);
                var enemyKnight1 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight2 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight3 = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[3, 3].AddContent(enemyCastle);
                board[1, 0].AddContent(playerKnight1);
                board[2, 1].AddContent(playerKnight2);
                board[3, 2].AddContent(playerKnight3);
                board[1, 1].AddContent(enemyKnight1);
                board[2, 2].AddContent(enemyKnight2);
                board[3, 3].AddContent(enemyKnight3);
                return new CellsCostAfterMoveTestCase
                {
                    Board = board,
                    Player = player,
                    Move = new CaptureMove(player, (3, 3)),
                    ExpectedMatrix = new [,] 
                    {
                        {enemyCapturableContentCost, freeCellCost, freeCellCost, freeCellCost},
                        {3, freeCellCost, freeCellCost, freeCellCost},
                        {freeCellCost, 3, freeCellCost, freeCellCost},
                        {freeCellCost, freeCellCost, 3, enemyCapturableContentCost}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost,
                    BoardCellsStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>())
                };
            }
        }

        
        private CellsCostAfterMoveTestCase GetCellsCostsAfterMoveTestCase(int testCaseNumber)
        {
            int freeCellCost = 1;
            int ownContentCost = 1002;
            int enemyCapturableContentCost = 1003;
            int notRemovableObstaclesCost = 1004;
            int undefinedCellCost = 1005;
            //First case with 2 by 2 field and 1 player castle on it.
            //The move places a friendly knight on the board.]
            if (testCaseNumber == 1)
            {
                var board = GetFullNByNBoard(2);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3 ,1, 1);
                var playerKnight = GetKnight(player, 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[1, 1].AddContent(playerKnight);
                return new CellsCostAfterMoveTestCase
                {
                    Board = board,
                    Player = player,
                    Move = new PlaceMove(player, (1, 1), playerKnight),
                    ExpectedMatrix = new [,] 
                    {
                        {ownContentCost, freeCellCost},
                        {freeCellCost, ownContentCost}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost,
                    BoardCellsStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>())
                };
            }
            //Second case with 3 by 3 field, 1 player castle, 1 enemy castle and 1 enemy knight on it.
            //The move replaces enemy knight with player knight
            if (testCaseNumber == 2)
            {
                var board = GetFullNByNBoard(3);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3 ,1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 3, 3, 1, 1);;
                var playerKnight = GetKnight(player, 1, 2);
                var enemyKnight = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[2, 2].AddContent(enemyCastle);
                board[1, 1].AddContent(enemyKnight);
                return new CellsCostAfterMoveTestCase
                {
                    Board = board,
                    Player = player,
                    Move = new ReplaceMove(player, (1, 1), playerKnight),
                    ExpectedMatrix = new [,] 
                    {
                        {ownContentCost, freeCellCost, freeCellCost},
                        {freeCellCost, ownContentCost, freeCellCost},
                        {freeCellCost, freeCellCost, enemyCapturableContentCost}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost,
                    BoardCellsStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>())
                };
            }
            //Third case with 4 by 4 field, 1 player castle, 1 enemy castle, 2 enemy knights and 2 player knights on it.
            //The move replaces the root enemy knight and hence cuts other enemy knight.
            if (testCaseNumber == 3)
            {
                var board = GetFullNByNBoard(4);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3 ,1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 3, 3, 1, 1);;
                var playerKnight1 = GetKnight(player, 1, 2);
                var playerKnight2 = GetKnight(player, 1 ,2);
                var enemyKnight1 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight2 = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[3, 3].AddContent(enemyCastle);
                board[1, 0].AddContent(playerKnight1);
                board[2, 1].AddContent(playerKnight2);
                board[1, 1].AddContent(enemyKnight1);
                board[2, 2].AddContent(enemyKnight2);
                return new CellsCostAfterMoveTestCase
                {
                    Board = board,
                    Player = player,
                    Move = new ReplaceMove(player, (2, 2), playerKnight1),
                    ExpectedMatrix = new [,] 
                    {
                        {ownContentCost, freeCellCost, freeCellCost, freeCellCost},
                        {ownContentCost, freeCellCost, freeCellCost, freeCellCost},
                        {freeCellCost, ownContentCost, ownContentCost, freeCellCost},
                        {freeCellCost, freeCellCost, freeCellCost, enemyCapturableContentCost}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost,
                    BoardCellsStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>())
                };
            }
            //The fourth case is similar to previous one, except there are 3 player knights and the move tries to capture enemy castle.
            //The capture is not successful and hence no costs should change after the move.
            if(testCaseNumber == 4)
            {
                var board = GetFullNByNBoard(4);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3, 1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 3, 3, 1, 1);
                var playerKnight1 = GetKnight(player, 1, 2);
                var playerKnight2 = GetKnight(player, 1 ,2);
                var playerKnight3 = GetKnight(player, 1 ,2);
                var enemyKnight1 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight2 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight3 = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[3, 3].AddContent(enemyCastle);
                board[1, 0].AddContent(playerKnight1);
                board[2, 1].AddContent(playerKnight2);
                board[3, 2].AddContent(playerKnight3);
                board[1, 1].AddContent(enemyKnight1);
                board[2, 2].AddContent(enemyKnight2);
                board[3, 3].AddContent(enemyKnight3);
                return new CellsCostAfterMoveTestCase
                {
                    Board = board,
                    Player = player,
                    Move = new CaptureMove(player, (3, 3)),
                    ExpectedMatrix = new [,] 
                    {
                        {ownContentCost, freeCellCost, freeCellCost, freeCellCost},
                        {ownContentCost, 3, freeCellCost, freeCellCost},
                        {freeCellCost, ownContentCost, 3, freeCellCost},
                        {freeCellCost, freeCellCost, ownContentCost, enemyCapturableContentCost}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost,
                    BoardCellsStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>())
                };
            }
            //The last case is the same as fourth, however here player successfully caputres the enemy castle.
            else
            {
                                var board = GetFullNByNBoard(4);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3, 1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 1, 3, 1, 1);
                var playerKnight1 = GetKnight(player, 1, 2);
                var playerKnight2 = GetKnight(player, 1 ,2);
                var playerKnight3 = GetKnight(player, 1 ,2);
                var enemyKnight1 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight2 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight3 = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[3, 3].AddContent(enemyCastle);
                board[1, 0].AddContent(playerKnight1);
                board[2, 1].AddContent(playerKnight2);
                board[3, 2].AddContent(playerKnight3);
                board[1, 1].AddContent(enemyKnight1);
                board[2, 2].AddContent(enemyKnight2);
                board[3, 3].AddContent(enemyKnight3);
                return new CellsCostAfterMoveTestCase
                {
                    Board = board,
                    Player = player,
                    Move = new CaptureMove(player, (3, 3)),
                    ExpectedMatrix = new [,] 
                    {
                        {ownContentCost, freeCellCost, freeCellCost, freeCellCost},
                        {ownContentCost, freeCellCost, freeCellCost, freeCellCost},
                        {freeCellCost, ownContentCost, freeCellCost, freeCellCost},
                        {freeCellCost, freeCellCost, ownContentCost, ownContentCost}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost,
                    BoardCellsStateCalculator = new BoardCellsStateCalculator(board, new DfsUnconnectedValuesCutter<CellState>())
                };
            }
        }

        private static CellsCostTestCase GetCellsCostsTestCase(int testCaseNumber)
        {
            int freeCellCost = 1;
            int ownContentCost = 1002;
            int enemyCapturableContentCost = 1003;
            int notRemovableObstaclesCost = 1004;
            int undefinedCellCost = 1005;
            //First case with just one by one board and player castle on it
            if (testCaseNumber == 1)
            {
                var board = GetFullNByNBoard(1);
                var player = GetPlayer();
                var castle = GetCastle(player, 3, 3, 1,1);
                board[0, 0].AddContent(castle);
                return new CellsCostTestCase
                {
                    Board = board,
                    Player = player,
                    ExpectedMatrix = new int[,] {{ownContentCost}},
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost
                };
            }

            if (testCaseNumber == 2)
            {
                var board = GetFullNByNBoard(2);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3 ,1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 3, 3, 1, 1);
                var playerKnight = GetKnight(player, 1, 2);
                var enemyKnight = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[1, 1].AddContent(enemyCastle);
                board[0, 1].AddContent(playerKnight);
                board[1, 0].AddContent(enemyKnight);
                return new CellsCostTestCase
                {
                    Board = board,
                    Player = player,
                    ExpectedMatrix = new int[,] 
                    {
                        {ownContentCost, ownContentCost},
                        {3, enemyCapturableContentCost}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost
                };
            }            
            //Third case with 4 by 4 board, 1 player castle, 1 enemy castle, 2 player knights and 3 enemy knights
            else
            {
                var board = GetFullNByNBoard(4);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3 ,1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 3, 3, 1, 1);;
                var playerKnight1 = GetKnight(player, 1, 2);
                var playerKnight2 = GetKnight(player, 1 ,2);
                var enemyKnight1 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight2 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight3 = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[3, 3].AddContent(enemyCastle);
                board[1, 1].AddContent(playerKnight1);
                board[2, 2].AddContent(playerKnight2);
                board[1, 2].AddContent(enemyKnight1);
                board[2, 1].AddContent(enemyKnight2);
                board[2, 3].AddContent(enemyKnight3);
                return new CellsCostTestCase
                {
                    Board = board,
                    Player = player,
                    ExpectedMatrix = new int[,] 
                    {
                        {ownContentCost, freeCellCost, freeCellCost, freeCellCost},
                        {freeCellCost, ownContentCost, 3, freeCellCost},
                        {freeCellCost, 3, ownContentCost, 3},
                        {freeCellCost, freeCellCost, freeCellCost, enemyCapturableContentCost}
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost
                };
            }
        }

        private static CellsCostTestCase GetInverseCellsCostsTestCase(int testCaseNumber)
        {
            int freeCellCost = 1;
            int ownContentCost = 1002;
            int enemyCapturableContentCost = 1003;
            int notRemovableObstaclesCost = 1004;
            int undefinedCellCost = 1005;
            //First case with just one by one board and player castle on it
            if (testCaseNumber == 1)
            {
                var board = GetFullNByNBoard(1);
                var player = GetPlayer();
                var castle = GetCastle(player, 3, 3 ,1, 1);
                board[0, 0].AddContent(castle);
                return new CellsCostTestCase
                {
                    Board = board,
                    Player = player,
                    ExpectedMatrix = new[,] { { enemyCapturableContentCost } },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost
                };
            }

            if (testCaseNumber == 2)
            {
                var board = GetFullNByNBoard(2);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3 ,1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 3, 3, 1, 1);;
                var playerKnight = GetKnight(player, 1, 2);
                var enemyKnight = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[1, 1].AddContent(enemyCastle);
                board[0, 1].AddContent(playerKnight);
                board[1, 0].AddContent(enemyKnight);
                return new CellsCostTestCase
                {
                    Board = board,
                    Player = player,
                    ExpectedMatrix = new[,]
                    {
                        { enemyCapturableContentCost, 3 },
                        { ownContentCost, ownContentCost }
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost
                };
            }
            //Last case with 4 by 4 board, 1 player castle, 1 enemy castle, 2 player knights and 3 enemy knights
            //Last case with 4 by 4 board, 1 player castle, 1 enemy castle, 2 player knights and 3 enemy knights
            else
            {
                var board = GetFullNByNBoard(4);
                var player = GetPlayer();
                var playerCastle = GetCastle(player, 3, 3 ,1, 1);
                var enemyCastle = GetCastle(GetPlayer(), 3, 3, 1, 1);;
                var playerKnight1 = GetKnight(player, 1, 2);
                var playerKnight2 = GetKnight(player, 1, 2);
                var enemyKnight1 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight2 = GetKnight(GetPlayer(), 1, 2);
                var enemyKnight3 = GetKnight(GetPlayer(), 1, 2);
                board[0, 0].AddContent(playerCastle);
                board[3, 3].AddContent(enemyCastle);
                board[1, 1].AddContent(playerKnight1);
                board[2, 2].AddContent(playerKnight2);
                board[1, 2].AddContent(enemyKnight1);
                board[2, 1].AddContent(enemyKnight2);
                board[2, 3].AddContent(enemyKnight3);
                return new CellsCostTestCase
                {
                    Board = board,
                    Player = player,
                    ExpectedMatrix = new int[,]
                    {
                        { enemyCapturableContentCost, freeCellCost, freeCellCost, freeCellCost },
                        { freeCellCost, 3, ownContentCost, freeCellCost },
                        { freeCellCost, ownContentCost, 3, ownContentCost },
                        { freeCellCost, freeCellCost, freeCellCost, ownContentCost }
                    },
                    MinimumPlaceCost = freeCellCost,
                    OwnContentCost = ownContentCost,
                    EnemyCapturableContentCost = enemyCapturableContentCost,
                    NotRemovableObstaclesCost = notRemovableObstaclesCost,
                    UndefinedCellCost = undefinedCellCost
                };
            }
        }
    }
}