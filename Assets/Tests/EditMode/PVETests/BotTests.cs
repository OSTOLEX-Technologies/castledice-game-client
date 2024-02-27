using System.Threading.Tasks;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Src.GameplayPresenter.GameWrappers;
using Src.PVE;
using Src.PVE.MoveSearchers;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests
{
    public class BotTests
    {
        private const string MakeMoveMethodName = "TryMakeMove";
        private const string DelayMethodName = "Delay";
        
        [Test]
        public void Bot_ShouldTryMakeMove_AfterMoveAppliedInGame()
        {
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.CheckTurns()).Raises(g => g.MoveApplied += null, gameMock.Object, GetMove());
            var botMock = new BotBuilder { Game = gameMock.Object }.BuildMock();
            botMock.Protected().Setup(MakeMoveMethodName).Verifiable();
            var bot = botMock.Object; //Forcing constructor call
            
            gameMock.Object.CheckTurns();
            
            botMock.Protected().Verify(MakeMoveMethodName, Times.Once());
        }
        
        [Test]
        public void Bot_ShouldTryMakeMove_AfterActionPointsIncreasedInBotPlayer()
        {
            var botPlayer = GetPlayer();
            var botMock = new BotBuilder{BotPlayer = botPlayer}.BuildMock();
            botMock.Protected().Setup(MakeMoveMethodName).Verifiable();
            var bot = botMock.Object; //Forcing constructor call
            
            botPlayer.ActionPoints.IncreaseActionPoints(1);
            
            botMock.Protected().Verify(MakeMoveMethodName, Times.Once());
        }
        
        [Test]
        public void TryMakeMove_ShouldApplyBestMove_IfIsBotMove()
        {
            var localMoveApplierMock = new Mock<ILocalMoveApplier>();
            var bestMoveSearcherMock = new Mock<IBestMoveSearcher>();
            var botPlayer = GetPlayer();
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetCurrentPlayer()).Returns(botPlayer);
            var botMock = new BotBuilder
            {
                LocalMoveApplier = localMoveApplierMock.Object,
                BestMoveSearcher = bestMoveSearcherMock.Object,
                BotPlayer = botPlayer,
                Game = gameMock.Object
            }.BuildMock();
            botMock.CallBase = true;
            var bot = botMock.Object; 
            var bestMove = GetMove();
            bestMoveSearcherMock.Setup(b => b.GetBestMove(botPlayer.Id)).Returns(bestMove);
            
            bot.CallProtectedMethod(MakeMoveMethodName);
            
            localMoveApplierMock.Verify(l => l.ApplyMove(bestMove), Times.Once());
        }

        [Test]
        public void TryMakeMove_ShouldDoNothing_IfIsNotBotMove()
        {
            var localMoveApplierMock = new Mock<ILocalMoveApplier>();
            var bestMoveSearcherMock = new Mock<IBestMoveSearcher>();
            var botPlayer = GetPlayer();
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetCurrentPlayer()).Returns(GetPlayer());
            var botMock = new BotBuilder
            {
                LocalMoveApplier = localMoveApplierMock.Object,
                BestMoveSearcher = bestMoveSearcherMock.Object,
                BotPlayer = botPlayer,
                Game = gameMock.Object
            }.BuildMock();
            botMock.CallBase = true;
            var bot = botMock.Object; 
            
            bot.CallProtectedMethod(MakeMoveMethodName);
            
            localMoveApplierMock.Verify(l => l.ApplyMove(It.IsAny<AbstractMove>()), Times.Never());
        }
        
        [Test]
        public async Task TryMakeMove_ShouldCallDelay_BeforeApplyingMove()
        {
            var localMoveApplierMock = new Mock<ILocalMoveApplier>();
            var bestMoveSearcherMock = new Mock<IBestMoveSearcher>();
            var botPlayer = GetPlayer();
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetCurrentPlayer()).Returns(botPlayer);
            var botMock = new BotBuilder
            {
                LocalMoveApplier = localMoveApplierMock.Object,
                BestMoveSearcher = bestMoveSearcherMock.Object,
                BotPlayer = botPlayer,
                Game = gameMock.Object
            }.BuildMock();
            botMock.CallBase = true;
            var bot = botMock.Object; 
            var bestMove = GetMove();
            bestMoveSearcherMock.Setup(b => b.GetBestMove(botPlayer.Id)).Returns(bestMove);
            int callOrder = 0;
            botMock.Protected().Setup<Task>(DelayMethodName).Returns(Task.CompletedTask).Callback(() => Assert.AreEqual(0, callOrder++));
            localMoveApplierMock.Setup(l => l.ApplyMove(bestMove)).Callback(() => Assert.AreEqual(1, callOrder++));
            
           await bot.CallAsyncProtectedMethod(MakeMoveMethodName);
        }
        
        private class BotBuilder
        {
            public ILocalMoveApplier LocalMoveApplier = new Mock<ILocalMoveApplier>().Object;
            public IBestMoveSearcher BestMoveSearcher = new Mock<IBestMoveSearcher>().Object;
            public Game Game = GetGameMock().Object;
            public Player BotPlayer = GetPlayer();
            
            public Bot Build()
            {
                return new Mock<Bot>(LocalMoveApplier, BestMoveSearcher, Game, BotPlayer).Object;
            }
            
            public Mock<Bot> BuildMock()
            {
                return new Mock<Bot>(LocalMoveApplier, BestMoveSearcher, Game, BotPlayer);
            }
        }
    }
}