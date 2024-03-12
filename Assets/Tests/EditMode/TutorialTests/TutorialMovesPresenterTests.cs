using System;
using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayView.ClientMoves;
using Src.PVE.MoveConditions;
using Src.Tutorial;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.TutorialTests
{
    public class TutorialMovesPresenterTests
    {
        [Test]
        public void Presenter_ShouldShowPossibleMovesListOnView_IfPositionClickedOnView()
        {
            var rnd = new Random();
            var playerId = rnd.Next();
            var position = new Vector2Int(rnd.Next(), rnd.Next());
            var expectedList = new List<AbstractMove>();
            var possibleMovesListProviderMock = new Mock<IPossibleMovesListProvider>();
            possibleMovesListProviderMock.Setup(provider => provider.GetPossibleMoves(position, playerId))
                .Returns(expectedList);
            var viewMock = new Mock<IMovesView>();
            var presenter = new TutorialMovesPresenterBuilder
            {
                PlayerId = playerId,
                View = viewMock.Object,
                PossibleMovesListProvider = possibleMovesListProviderMock.Object
            }.Build();
            
            viewMock.Raise(view => view.PositionClicked += null, viewMock.Object, position);
            
            viewMock.Verify(view => view.ShowMovesList(expectedList), Times.Once);
        }

        [Test]
        public void Presenter_ShouldApplyMovePickedOnView_IfMoveSatisfiesCurrentCondition()
        {
            var move = GetMove();
            var moveApplierMock = new Mock<ILocalMoveApplier>();
            var moveConditionMock = new Mock<IMoveCondition>();
            moveConditionMock.Setup(condition => condition.IsSatisfiedBy(move)).Returns(true);
            var moveConditionsSequenceMock = new Mock<IMoveConditionsSequence>();
            moveConditionsSequenceMock.Setup(sequence => sequence.GetCurrentCondition()).Returns(moveConditionMock.Object);
            var viewMock = new Mock<IMovesView>();
            var presenter = new TutorialMovesPresenterBuilder
            {
                MoveConditionsSequence = moveConditionsSequenceMock.Object,
                View = viewMock.Object,
                LocalMoveApplier = moveApplierMock.Object
            }.Build();
            
            viewMock.Raise(view => view.MovePicked += null, viewMock.Object, move);
            
            moveApplierMock.Verify(applier => applier.ApplyMove(move), Times.Once);
        }
        
        [Test]
        public void Presenter_ShouldNotApplyMovePickedOnView_IfMoveDoesNotSatisfyCurrentCondition()
        {
            var move = GetMove();
            var moveApplierMock = new Mock<ILocalMoveApplier>();
            var moveConditionMock = new Mock<IMoveCondition>();
            moveConditionMock.Setup(condition => condition.IsSatisfiedBy(move)).Returns(false);
            var moveConditionsSequenceMock = new Mock<IMoveConditionsSequence>();
            moveConditionsSequenceMock.Setup(sequence => sequence.GetCurrentCondition()).Returns(moveConditionMock.Object);
            var viewMock = new Mock<IMovesView>();
            var presenter = new TutorialMovesPresenterBuilder
            {
                MoveConditionsSequence = moveConditionsSequenceMock.Object,
                View = viewMock.Object,
                LocalMoveApplier = moveApplierMock.Object
            }.Build();
            
            viewMock.Raise(view => view.MovePicked += null, viewMock.Object, move);
            
            moveApplierMock.Verify(applier => applier.ApplyMove(move), Times.Never);
        }
        
        [Test]
        public void Presenter_ShouldRaiseRightMovePickedEvent_IfMoveSatisfiesCurrentCondition()
        {
            var expectedMove = GetMove();
            var moveConditionMock = new Mock<IMoveCondition>();
            moveConditionMock.Setup(condition => condition.IsSatisfiedBy(expectedMove)).Returns(true);
            var moveConditionsSequenceMock = new Mock<IMoveConditionsSequence>();
            moveConditionsSequenceMock.Setup(sequence => sequence.GetCurrentCondition()).Returns(moveConditionMock.Object);
            var viewMock = new Mock<IMovesView>();
            var presenter = new TutorialMovesPresenterBuilder
            {
                MoveConditionsSequence = moveConditionsSequenceMock.Object,
                View = viewMock.Object
            }.Build();
            AbstractMove actualMove = null;
            presenter.RightMovePicked += (sender, pickedMove) => actualMove = pickedMove;
            
            viewMock.Raise(view => view.MovePicked += null, viewMock.Object, expectedMove);
            
            Assert.AreSame(expectedMove, actualMove);
        }

        [Test]
        public void Presenter_ShouldNotRaiseRightMovePickedEvent_IfMoveDoesNotSatisfyCurrentCondition()
        {
            var move = GetMove();
            var moveConditionMock = new Mock<IMoveCondition>();
            moveConditionMock.Setup(condition => condition.IsSatisfiedBy(move)).Returns(false);
            var moveConditionsSequenceMock = new Mock<IMoveConditionsSequence>();
            moveConditionsSequenceMock.Setup(sequence => sequence.GetCurrentCondition()).Returns(moveConditionMock.Object);
            var viewMock = new Mock<IMovesView>();
            var presenter = new TutorialMovesPresenterBuilder
            {
                MoveConditionsSequence = moveConditionsSequenceMock.Object,
                View = viewMock.Object
            }.Build();
            AbstractMove actualMove = null;
            presenter.RightMovePicked += (sender, pickedMove) => actualMove = pickedMove;
            
            viewMock.Raise(view => view.MovePicked += null, viewMock.Object, move);
            
            Assert.IsNull(actualMove);
        }
        
        [Test]
        public void Presenter_ShouldRaiseWrongMovePickedEvent_IfMoveDoesNotSatisfyCurrentCondition()
        {
            var move = GetMove();
            var moveConditionMock = new Mock<IMoveCondition>();
            moveConditionMock.Setup(condition => condition.IsSatisfiedBy(move)).Returns(false);
            var moveConditionsSequenceMock = new Mock<IMoveConditionsSequence>();
            moveConditionsSequenceMock.Setup(sequence => sequence.GetCurrentCondition()).Returns(moveConditionMock.Object);
            var viewMock = new Mock<IMovesView>();
            var presenter = new TutorialMovesPresenterBuilder
            {
                MoveConditionsSequence = moveConditionsSequenceMock.Object,
                View = viewMock.Object
            }.Build();
            AbstractMove actualMove = null;
            presenter.WrongMovePicked += (sender, pickedMove) => actualMove = pickedMove;
            
            viewMock.Raise(view => view.MovePicked += null, viewMock.Object, move);
            
            Assert.AreSame(move, actualMove);
        }
        
        [Test]
        public void Presenter_ShouldNotRaiseWrongMovePickedEvent_IfMoveSatisfiesCurrentCondition()
        {
            var move = GetMove();
            var moveConditionMock = new Mock<IMoveCondition>();
            moveConditionMock.Setup(condition => condition.IsSatisfiedBy(move)).Returns(true);
            var moveConditionsSequenceMock = new Mock<IMoveConditionsSequence>();
            moveConditionsSequenceMock.Setup(sequence => sequence.GetCurrentCondition()).Returns(moveConditionMock.Object);
            var viewMock = new Mock<IMovesView>();
            var presenter = new TutorialMovesPresenterBuilder
            {
                MoveConditionsSequence = moveConditionsSequenceMock.Object,
                View = viewMock.Object
            }.Build();
            AbstractMove actualMove = null;
            presenter.WrongMovePicked += (sender, pickedMove) => actualMove = pickedMove;
            
            viewMock.Raise(view => view.MovePicked += null, viewMock.Object, move);
            
            Assert.IsNull(actualMove);
        }
        
        [Test]
        public void Presenter_ShouldMoveToNextCondition_IfMoveSatisfiesCurrentCondition()
        {
            var move = GetMove();
            var moveConditionMock = new Mock<IMoveCondition>();
            moveConditionMock.Setup(condition => condition.IsSatisfiedBy(move)).Returns(true);
            var moveConditionsSequenceMock = new Mock<IMoveConditionsSequence>();
            moveConditionsSequenceMock.Setup(sequence => sequence.GetCurrentCondition()).Returns(moveConditionMock.Object);
            var viewMock = new Mock<IMovesView>();
            var presenter = new TutorialMovesPresenterBuilder
            {
                MoveConditionsSequence = moveConditionsSequenceMock.Object,
                View = viewMock.Object
            }.Build();
            
            viewMock.Raise(view => view.MovePicked += null, viewMock.Object, move);
            
            moveConditionsSequenceMock.Verify(sequence => sequence.MoveToNextCondition(), Times.Once);
        }
        
        [Test]
        public void Presenter_ShouldNotMoveToNextCondition_IfMoveDoesNotSatisfyCurrentCondition()
        {
            var move = GetMove();
            var moveConditionMock = new Mock<IMoveCondition>();
            moveConditionMock.Setup(condition => condition.IsSatisfiedBy(move)).Returns(false);
            var moveConditionsSequenceMock = new Mock<IMoveConditionsSequence>();
            moveConditionsSequenceMock.Setup(sequence => sequence.GetCurrentCondition()).Returns(moveConditionMock.Object);
            var viewMock = new Mock<IMovesView>();
            var presenter = new TutorialMovesPresenterBuilder
            {
                MoveConditionsSequence = moveConditionsSequenceMock.Object,
                View = viewMock.Object
            }.Build();
            
            viewMock.Raise(view => view.MovePicked += null, viewMock.Object, move);
            
            moveConditionsSequenceMock.Verify(sequence => sequence.MoveToNextCondition(), Times.Never);
        }
        
        private class TutorialMovesPresenterBuilder
        {
            public IMovesView View { get; set; }
            public IPossibleMovesListProvider PossibleMovesListProvider { get; set; }
            public ILocalMoveApplier LocalMoveApplier { get; set; }
            public IMoveConditionsSequence MoveConditionsSequence { get; set; }
            public int PlayerId { get; set; }

            public TutorialMovesPresenterBuilder()
            {
                var viewMock = new Mock<IMovesView>();
                View = viewMock.Object;
                var possibleMovesListProviderMock = new Mock<IPossibleMovesListProvider>();
                possibleMovesListProviderMock.Setup(provider => provider.GetPossibleMoves(It.IsAny<Vector2Int>(), It.IsAny<int>())).Returns(new List<AbstractMove>());
                PossibleMovesListProvider = possibleMovesListProviderMock.Object;
                var localMoveApplierMock = new Mock<ILocalMoveApplier>();
                LocalMoveApplier = localMoveApplierMock.Object;
                var moveConditionsSequenceMock = new Mock<IMoveConditionsSequence>();
                var condition = new Mock<IMoveCondition>().Object;
                moveConditionsSequenceMock.Setup(sequence => sequence.GetCurrentCondition()).Returns(condition);
                MoveConditionsSequence = moveConditionsSequenceMock.Object;
            }
            
            public TutorialMovesPresenter Build()
            {
                return new TutorialMovesPresenter(View, PossibleMovesListProvider, LocalMoveApplier, MoveConditionsSequence, PlayerId);
            }
        }
    }
}