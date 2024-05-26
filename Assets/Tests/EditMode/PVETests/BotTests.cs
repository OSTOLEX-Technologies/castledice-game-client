using System;
using System.Threading.Tasks;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Src.GameplayPresenter.GameWrappers;
using Src.PVE;
using Src.PVE.BotTriggers;
using Src.PVE.MoveSearchers;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests
{
    public class BotTests
    {
        private readonly Random _random = new();
        
        [Test]
        public void Bot_ShouldGetBestMove_FromSearcher_WithBotPlayerId_Once_IfBotShouldMove()
        {
            var bestMoveSearcherMock = new Mock<IBestMoveSearcher>();
            var botMoveTriggerMock = new Mock<IBotMoveTrigger>();
            var botPlayer = GetPlayer(id: _random.Next());
            var bot = new BotBuilder
            {
                BotPlayer = botPlayer, 
                BestMoveSearcher = bestMoveSearcherMock.Object, 
                MoveTrigger = botMoveTriggerMock.Object
            }.Build();
            
            botMoveTriggerMock.Raise(m => m.ShouldMakeMove += null);
            
            bestMoveSearcherMock.Verify(m => m.GetBestMove(botPlayer.Id), Times.Once);
        }

        [Test]
        public void Bot_ShouldApplyMove_FromSearcher()
        {
            var bestMoveSearcherMock = new Mock<IBestMoveSearcher>();
            var move = GetMove();
            bestMoveSearcherMock.Setup(m => m.GetBestMove(It.IsAny<int>())).Returns(move);
            var localMoveApplierMock = new Mock<ILocalMoveApplier>();
            var botMoveTriggerMock = new Mock<IBotMoveTrigger>();
            var bot = new BotBuilder
            {
                BestMoveSearcher = bestMoveSearcherMock.Object, 
                LocalMoveApplier = localMoveApplierMock.Object, 
                MoveTrigger = botMoveTriggerMock.Object
            }.Build();
            
            botMoveTriggerMock.Raise(m => m.ShouldMakeMove += null);
            
            localMoveApplierMock.Verify(m => m.ApplyMove(move), Times.Once);
        }
    }
    
    public class BotBuilder
    {
        public ILocalMoveApplier LocalMoveApplier = new Mock<ILocalMoveApplier>().Object;
        public IBestMoveSearcher BestMoveSearcher = new Mock<IBestMoveSearcher>().Object;
        public IBotMoveTrigger MoveTrigger = new Mock<IBotMoveTrigger>().Object;
        public Player BotPlayer = GetPlayer();
            
        public Bot Build()
        {
            return new Bot(LocalMoveApplier, BestMoveSearcher, BotPlayer, MoveTrigger);
        }
    }
}