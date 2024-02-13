using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.TurnsLogic;
using Moq;
using static Tests.Utils.ObjectCreationUtility;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests
{
    
    public class GameBuilderTests
    {
        private const string PlayersListFieldName = "_playersList";
        private const string BoardConfigFieldName = "_boardConfig";
        private const string PlaceablesConfigFieldName = "_placeablesConfig";
        private const string TurnSwitchConditionsConfigFieldName = "_turnSwitchConditionsConfig";
        
        [Test]
        public void BuildPlayersList_ShouldReturnBuilder()
        {
            var builder = new GameBuilderCreator().Create();
            
            var returnedBuilder = builder.BuildPlayersList(GetPlayersList());
            
            Assert.AreSame(builder, returnedBuilder);
        }
        
        [Test]
        public void BuildPlayersList_ShouldSetPlayersList()
        {
            var builder = new GameBuilderCreator().Create();
            var expectedList = GetPlayersList();
            
            builder.BuildPlayersList(expectedList);
            var actualList = builder.GetPrivateField<List<Player>>(PlayersListFieldName);
            
            Assert.AreSame(expectedList, actualList);
        }
        
        [Test]
        public void BuildBoardConfig_ShouldReturnBuilder()
        {
            var builder = new GameBuilderCreator().Create();
            
            var returnedBuilder = builder.BuildBoardConfig(GetBoardConfig());
            
            Assert.AreSame(builder, returnedBuilder);
        }
        
        [Test]
        public void BuildBoardConfig_ShouldSetBoardConfig()
        {
            var builder = new GameBuilderCreator().Create();
            var expectedConfig = GetBoardConfig();
            
            builder.BuildBoardConfig(expectedConfig);
            var actualConfig = builder.GetPrivateField<BoardConfig>(BoardConfigFieldName);
            
            Assert.AreSame(expectedConfig, actualConfig);
        }
        
        [Test]
        public void BuildPlaceablesConfig_ShouldReturnBuilder()
        {
            var builder = new GameBuilderCreator().Create();
            
            var returnedBuilder = builder.BuildPlaceablesConfig(GetPlaceablesConfig());
            
            Assert.AreSame(builder, returnedBuilder);
        }
        
        [Test]
        public void BuildPlaceablesConfig_ShouldSetPlaceablesConfig()
        {
            var builder = new GameBuilderCreator().Create();
            var expectedConfig = GetPlaceablesConfig();
            
            builder.BuildPlaceablesConfig(expectedConfig);
            var actualConfig = builder.GetPrivateField<PlaceablesConfig>(PlaceablesConfigFieldName);
            
            Assert.AreSame(expectedConfig, actualConfig);
        }
        
        [Test]
        public void BuildTurnSwitchConditionsConfig_ShouldReturnBuilder()
        {
            var builder = new GameBuilderCreator().Create();
            
            var returnedBuilder = builder.BuildTurnSwitchConditionsConfig(GetTurnSwitchConditionsConfig());
            
            Assert.AreSame(builder, returnedBuilder);
        }
        
        [Test]
        public void BuildTurnSwitchConditionsConfig_ShouldSetTurnSwitchConditionsConfig()
        {
            var builder = new GameBuilderCreator().Create();
            var expectedConfig = GetTurnSwitchConditionsConfig();
            
            builder.BuildTurnSwitchConditionsConfig(expectedConfig);
            var actualConfig = builder.GetPrivateField<TurnSwitchConditionsConfig>(TurnSwitchConditionsConfigFieldName);
            
            Assert.AreSame(expectedConfig, actualConfig);
        }
        
        [Test]
        [TestCase(PlayersListFieldName)]
        [TestCase(BoardConfigFieldName)]
        [TestCase(PlaceablesConfigFieldName)]
        [TestCase(TurnSwitchConditionsConfigFieldName)]
        public void Build_ShouldThrowInvalidOperationException_IfOneOfTheGameConstructorParametersIsNull(string nullFiledName)
        {
            var builder = new GameBuilderCreator().Create();
            SetGameConstructorParameters(builder);
            builder.SetPrivateFieldNull(nullFiledName);
            
            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }
        
        [Test]
        public void Build_ShouldPassGameConstructorParameters_ToGameConstructorWrapper()
        {
            var playersList = GetPlayersList();
            var boardConfig = GetBoardConfig();
            var placeablesConfig = GetPlaceablesConfig();
            var turnSwitchConditionsConfig = GetTurnSwitchConditionsConfig();
            var wrapperMock = new Mock<IGameConstructorWrapper>();
            var builder = new GameBuilderCreator{GameConstructorWrapper = wrapperMock.Object}.Create();
            builder.SetPrivateField(PlayersListFieldName, playersList);
            builder.SetPrivateField(BoardConfigFieldName, boardConfig);
            builder.SetPrivateField(PlaceablesConfigFieldName, placeablesConfig);
            builder.SetPrivateField(TurnSwitchConditionsConfigFieldName, turnSwitchConditionsConfig);
            
            builder.Build();
            
            wrapperMock.Verify(wrapper => wrapper.CreateGame(playersList, boardConfig, placeablesConfig, turnSwitchConditionsConfig), Times.Once);
        }
        
        [Test]
        public void Build_ShouldReturnGame_FromWrapper()
        {
            var expectedGame = GetGame();
            var wrapperMock = new Mock<IGameConstructorWrapper>();
            wrapperMock.Setup(wrapper => wrapper.CreateGame(It.IsAny<List<Player>>(), It.IsAny<BoardConfig>(), It.IsAny<PlaceablesConfig>(), It.IsAny<TurnSwitchConditionsConfig>()))
                .Returns(expectedGame);
            var builder = new GameBuilderCreator{GameConstructorWrapper = wrapperMock.Object}.Create();
            SetGameConstructorParameters(builder);
            
            var actualGame = builder.Build();
            
            Assert.AreSame(expectedGame, actualGame);
        }
        
        [Test]
        public void Reset_ShouldReturnBuilder()
        {
            var builder = new GameBuilderCreator().Create();
            
            var returnedBuilder = builder.Reset();
            
            Assert.AreSame(builder, returnedBuilder);
        }
        
        [Test]
        public void Reset_ShouldSetGameConstructorParametersToNull()
        {
            var builder = new GameBuilderCreator().Create();
            SetGameConstructorParameters(builder);
            
            builder.Reset();
            
            Assert.IsNull(builder.GetPrivateField<List<PlayersList>>(PlayersListFieldName));
            Assert.IsNull(builder.GetPrivateField<BoardConfig>(BoardConfigFieldName));
            Assert.IsNull(builder.GetPrivateField<PlaceablesConfig>(PlaceablesConfigFieldName));
            Assert.IsNull(builder.GetPrivateField<TurnSwitchConditionsConfig>(TurnSwitchConditionsConfigFieldName));
        }

        private static void SetGameConstructorParameters(GameBuilder builder)
        {
            builder.SetPrivateField(PlayersListFieldName, GetPlayersList());
            builder.SetPrivateField(BoardConfigFieldName, GetBoardConfig());
            builder.SetPrivateField(PlaceablesConfigFieldName, GetPlaceablesConfig());
            builder.SetPrivateField(TurnSwitchConditionsConfigFieldName, GetTurnSwitchConditionsConfig());
        }

        private class GameBuilderCreator
        {
            public IGameConstructorWrapper GameConstructorWrapper { get; set; } = new Mock<IGameConstructorWrapper>().Object;
            
            public GameBuilder Create()
            {
                return new GameBuilder(GameConstructorWrapper);
            }
        }
    }
}