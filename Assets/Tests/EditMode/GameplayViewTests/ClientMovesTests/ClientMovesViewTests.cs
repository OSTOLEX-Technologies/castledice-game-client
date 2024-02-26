using System;
using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using NUnit.Framework;
using Src.GameplayView.ClickDetection;
using Src.GameplayView.ClientMoves;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.ClientMovesTests
{
    public class ClientMovesViewTests
    {
        private class DetectorForTests : ICellClickDetector
        {
            public Vector2Int Position { get; set; }
            public event EventHandler<Vector2Int> Clicked;

            public void Click()
            {
                Clicked?.Invoke(this, Position);
            }
        }
        
        public static Vector2Int[] Positions = { (0, 0), (1, 0), (2, 0), (0, 1), (1, 1) };
        
        [Test]
        public void PositionClickedEvent_ShouldBeCalled_IfClickedEventOnDetectorInvoked()
        {
            var detector = new DetectorForTests();
            var view = new ClientMovesView(new List<ICellClickDetector> {detector});
            var eventInvoked = false;
            view.PositionClicked += (sender, position) => eventInvoked = true;
            
            detector.Click();
            
            Assert.True(eventInvoked);
        }

        [Test]
        public void PositionClickedEvent_ShouldBeInvokedWithPosition_FromClickedEventOnDetector([ValueSource(nameof(Positions))]Vector2Int position)
        {
            var detector = new DetectorForTests();
            detector.Position = position;
            var view = new ClientMovesView(new List<ICellClickDetector> {detector});
            Vector2Int actualPosition = default;
            
            view.PositionClicked += (sender, pos) => actualPosition = pos;
            detector.Click();
            
            Assert.AreEqual(position, actualPosition);
        }

        [Test]
        public void ShowMovesList_ShouldInvokeMovePickedEvent_WithFirstMoveFromGivenList()
        {
            var view = new ClientMovesView(new List<ICellClickDetector>());
            var move = GetMove();
            var moves = new List<AbstractMove> {move};
            AbstractMove actualMove = null;
            
            view.MovePicked += (sender, m) => actualMove = m;
            view.ShowMovesList(moves);
            
            Assert.AreSame(move, actualMove);
        }
        
        [Test]
        public void ShowMovesList_ShouldNotInvokeMovePickedEvent_IfGivenEmptyList()
        {
            var view = new ClientMovesView(new List<ICellClickDetector>());
            var moves = new List<AbstractMove>();
            AbstractMove actualMove = null;
            
            view.MovePicked += (sender, m) => actualMove = m;
            view.ShowMovesList(moves);
            
            Assert.IsNull(actualMove);
        }
    }
}