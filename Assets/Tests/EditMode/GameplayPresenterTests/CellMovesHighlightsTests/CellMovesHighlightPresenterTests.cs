﻿using System.Collections.Generic;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter;
using Src.GameplayPresenter.CellMovesHighlights;
using Src.GameplayView.CellMovesHighlights;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.CellMovesHighlightsTests
{
    public class CellMovesHighlightPresenterTests
    {
        [Test]
        public void HighlightCellMoves_ShouldSendCurrentPlayerCellMovesList_ToGivenView([Values(1, 2, 3, 4, 5)]int playerId)
        {
            var viewMock = new Mock<ICellMovesHighlightView>();
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            var cellMovesListProviderMock = new Mock<ICellMovesListProvider>();
            var expectedList = new List<CellMove>();
            playerDataProviderMock.Setup(provider => provider.GetId()).Returns(playerId);
            cellMovesListProviderMock.Setup(provider => provider.GetCellMovesList(playerId)).Returns(expectedList);
            var gameMock = GetGameMock();
            var presenter = new CellMovesHighlightPresenter(playerDataProviderMock.Object, cellMovesListProviderMock.Object, gameMock.Object, viewMock.Object);
            
            presenter.HighlightCellMoves();
            
            viewMock.Verify(view => view.HighlightCellMoves(expectedList), Times.Once);
        }
    }
}