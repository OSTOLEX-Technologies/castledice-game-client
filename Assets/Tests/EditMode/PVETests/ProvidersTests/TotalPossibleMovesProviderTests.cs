using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using NUnit.Framework;
using Src.PVE.Providers;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.ProvidersTests
{
    public class TotalPossibleMovesProviderTests
    {
        [Test]
        public void GetTotalPossibleMoves_ShouldReturnAllPossibleMoves_ForPlayerWithGivenId()
        {
            var playerId = new Random().Next();
            var movesCount = new Random().Next(1, 10);
            var gameMock = GetGameMock();
            var cellMoves = new List<CellMove>();
            for (var i = 0; i < movesCount; i++)
            {
                cellMoves.Add(new CellMove(new Cell(i, i), MoveType.Place));
            } 
            var expectedMoves = new List<AbstractMove>();
            for (var i = 0; i < movesCount; i++)
            {
                expectedMoves.Add(GetMove());
            }
            gameMock.Setup(g => g.GetCellMoves(playerId)).Returns(cellMoves);
            for (int i = 0; i < movesCount; i++)
            {
                gameMock.Setup(g => g.GetPossibleMoves(playerId, cellMoves[i].Cell.Position)).Returns(new List<AbstractMove>{expectedMoves[i]});
            }
            var provider = new TotalPossibleMovesProvider(gameMock.Object);
            
            var actualMoves = provider.GetTotalPossibleMoves(playerId);

            Assert.AreEqual(expectedMoves, actualMoves);
        }
    }
}