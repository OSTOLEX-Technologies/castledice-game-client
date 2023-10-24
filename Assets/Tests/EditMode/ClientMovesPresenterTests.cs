using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter;
using Src.GameplayPresenter.ClientMoves;
using Src.GameplayView.ClientMoves;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode
{
    public class ClientMovesPresenterTests
    {
        public static int[] Ids = { 1, 2, 3, 4, 5, 6 };
        public static Vector2Int[] Positions = { (1, 1), (2, 2), (3, 3) };
        
        [Test]
        public void ShowMovesForPosition_ShouldPassGivenPosition_ToGivenPossibleMovesListProvider([ValueSource(nameof(Positions))]Vector2Int position)
        {
            var possibleMovesListProviderMock = new Mock<IPossibleMovesListProvider>();
            possibleMovesListProviderMock.Setup(p => p.GetPossibleMoves(It.IsAny<Vector2Int>(), It.IsAny<int>())).Returns(new List<AbstractMove>());
            var presenter = new ClientMovesPresenterBuilder
            {
                PossibleMovesListProvider = possibleMovesListProviderMock.Object
            }.Build();
            
            presenter.ShowMovesForPosition(position);
            
            possibleMovesListProviderMock.Verify(p => p.GetPossibleMoves(position, It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ShowMovesForPosition_ShouldPassIdFromGivenPlayerDataProvider_ToGivenPossibleMovesListProvider([ValueSource(nameof(Ids))]int id)
        {
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            playerDataProviderMock.Setup(p => p.GetId()).Returns(id);
            var possibleMovesListProviderMock = new Mock<IPossibleMovesListProvider>();
            possibleMovesListProviderMock.Setup(p => p.GetPossibleMoves(It.IsAny<Vector2Int>(), It.IsAny<int>())).Returns(new List<AbstractMove>());
            var presenter = new ClientMovesPresenterBuilder
            {
                PlayerDataProvider = playerDataProviderMock.Object,
                PossibleMovesListProvider = possibleMovesListProviderMock.Object
            }.Build();
            
            presenter.ShowMovesForPosition((1, 1));
            
            possibleMovesListProviderMock.Verify(p => p.GetPossibleMoves(It.IsAny<Vector2Int>(), id), Times.Once);
        }

        [Test]
        public void ShowMovesForPosition_ShouldPassMovesListFromGivenPossibleMovesListProvider_ToGivenClientMovesView()
        {
            var movesList = new List<AbstractMove>();
            var possibleMovesListProviderMock = new Mock<IPossibleMovesListProvider>();
            possibleMovesListProviderMock.Setup(p => p.GetPossibleMoves(It.IsAny<Vector2Int>(), It.IsAny<int>())).Returns(movesList);
            var viewMock = new Mock<IClientMovesView>();
            var presenter = new ClientMovesPresenterBuilder
            {
                PossibleMovesListProvider = possibleMovesListProviderMock.Object,
                View = viewMock.Object
            }.Build();
            
            presenter.ShowMovesForPosition((1, 1));
            
            viewMock.Verify(v => v.ShowMovesList(movesList), Times.Once);
        }
        
        [Test]
        public async Task MakeMove_ShouldPassGivenMove_ToGivenServerMoveApplier()
        {
            var move = GetMove();
            var serverMoveApplierMock = new Mock<IServerMoveApplier>();
            var presenter = new ClientMovesPresenterBuilder
            {
                ServerMoveApplier = serverMoveApplierMock.Object
            }.Build();
            
            await presenter.MakeMove(move);
            
            serverMoveApplierMock.Verify(s => s.ApplyMoveAsync(move), Times.Once);
        }

        [Test]
        public async Task MakeMove_ShouldPassGivenMoveToGivenLocalMoveApplier_IfMoveApplicationResultIsApplied()
        {
            var move = GetMove();
            var serverMoveApplierMock = new Mock<IServerMoveApplier>();
            serverMoveApplierMock.Setup(s => s.ApplyMoveAsync(It.IsAny<AbstractMove>())).ReturnsAsync(MoveApplicationResult.Applied);
            var localMoveApplierMock = new Mock<ILocalMoveApplier>();
            var presenter = new ClientMovesPresenterBuilder
            {
                ServerMoveApplier = serverMoveApplierMock.Object,
                LocalMoveApplier = localMoveApplierMock.Object
            }.Build();
            
            await presenter.MakeMove(move);
            
            localMoveApplierMock.Verify(l => l.ApplyMove(move), Times.Once);
        }
        
        [Test]
        public async Task MakeMove_ShouldNotPassGivenMoveToGivenLocalMoveApplier_IfMoveApplicationResultIsRejected()
        {
            var move = GetMove();
            var serverMoveApplierMock = new Mock<IServerMoveApplier>();
            serverMoveApplierMock.Setup(s => s.ApplyMoveAsync(It.IsAny<AbstractMove>())).ReturnsAsync(MoveApplicationResult.Rejected);
            var localMoveApplierMock = new Mock<ILocalMoveApplier>();
            var presenter = new ClientMovesPresenterBuilder
            {
                ServerMoveApplier = serverMoveApplierMock.Object,
                LocalMoveApplier = localMoveApplierMock.Object
            }.Build();
            
            await presenter.MakeMove(move);
            
            localMoveApplierMock.Verify(l => l.ApplyMove(move), Times.Never);
        }
        
        [Test]
        public void ShowMovesForPosition_ShouldBeCalled_IfPositionClickedEventIsInvoked()
        {
            var viewStub = new ClientMovesViewStub();
            var position = new Vector2Int(0, 0);
            var presenterMock = new Mock<ClientMovesPresenter>(GetPlayerDataProvider(), new Mock<IServerMoveApplier>().Object, GetPossibleMovesListProvider(), new Mock<ILocalMoveApplier>().Object, viewStub);
            var presenter = presenterMock.Object;
            
            viewStub.ClickOnPosition(position);
            
            presenterMock.Verify(p => p.ShowMovesForPosition(position), Times.Once);
        }
        
        [Test]
        public void MakeMove_ShouldBeCalled_IfMovePickedEventIsInvoked()
        {
            var viewStub = new ClientMovesViewStub();
            var move = GetMove();
            var presenterMock = new Mock<ClientMovesPresenter>(GetPlayerDataProvider(), new Mock<IServerMoveApplier>().Object, GetPossibleMovesListProvider(), new Mock<ILocalMoveApplier>().Object, viewStub);
            var presenter = presenterMock.Object;
            
            viewStub.PickMove(move);
            
            presenterMock.Verify(p => p.MakeMove(move), Times.Once);
        }

        private class ClientMovesPresenterBuilder
        {
            public IPlayerDataProvider PlayerDataProvider { get; set; } = GetPlayerDataProvider();
            public IServerMoveApplier ServerMoveApplier { get; set; } = new Mock<IServerMoveApplier>().Object;
            public IPossibleMovesListProvider PossibleMovesListProvider { get; set; } = GetPossibleMovesListProvider();
            public ILocalMoveApplier LocalMoveApplier { get; set; } = new Mock<ILocalMoveApplier>().Object;
            public IClientMovesView View { get; set; } = new Mock<IClientMovesView>().Object;
            
            public ClientMovesPresenter Build()
            {
                return new ClientMovesPresenter(PlayerDataProvider, ServerMoveApplier, PossibleMovesListProvider, LocalMoveApplier, View);
            }
        }

        public class ClientMovesViewStub : IClientMovesView
        {
            public void ShowMovesList(List<AbstractMove> moves)
            {
                
            }

            public void ClickOnPosition(Vector2Int position)
            {
                PositionClicked?.Invoke(this, position);
            }

            public void PickMove(AbstractMove move)
            {
                MovePicked?.Invoke(this, move);
            }

            public event EventHandler<Vector2Int> PositionClicked;
            public event EventHandler<AbstractMove> MovePicked;
        }
    }
}