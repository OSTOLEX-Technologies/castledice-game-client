﻿using System;
using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.ConfigsData;
using castledice_game_data_logic.Content;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions;
using NUnit.Framework;
using Src.Tutorial;
using UnityEngine;
using Random = System.Random;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.TutorialTests
{
    public class TutorialGameStartDataConfigTests
    {
        private const string VersionFieldName = "defaultVersion";
        private const string BoardWidthFieldName = "boardWidth";
        private const string BoardLengthFieldName = "boardLength";
        private const string CellTypeFieldName = "cellType";
        private const string EnemyBasePositionFieldName = "enemyBasePosition";
        private const string PlayerBasePositionFieldName = "playerBasePosition";
        private const string PlayerCastleConfigFieldName = "playerCastleConfig";
        private const string EnemyCastleConfigFieldName = "enemyCastleConfig";
        private const string DefaultTscTypesFieldName = "defaultTscTypes";
        private const string DefaultAvailablePlacementTypesFieldName = "defaultPlacementTypes";
        
        private const string KnightPlacementCostFieldName = "knightPlacementCost";
        private const string KnightHealthFieldName = "knightHealth";
        
        private const string MaxFreeDurabilityFieldName = "MaxFreeDurability";
        private const string MaxDurabilityFieldName = "MaxDurability";
        private const string DurabilityFieldName = "Durability";
        private const string CaptureHitCostFieldName = "CaptureHitCost";

        //Test for version
        [Test]
        public void GetGameStartData_ShouldReturnGameStartData_WithProperlySetVersion()
        {
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            var expectedVersion = config.GetPrivateConst<string>(VersionFieldName);
            
            var gameStartData = config.GetGameStartData(0, 1);
            
            Assert.AreEqual(expectedVersion, gameStartData.Version);
        }
        
        [Test]
        public void GetGameStartData_ShouldReturnGameStartData_WhereBoardDataHasGivenBoardWidthAndLength()
        {
            var rnd = new Random();
            var boardWidth = rnd.Next(1, 100);
            var boardLength = rnd.Next(1, 100);
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            config.SetPrivateField(BoardWidthFieldName, boardWidth);
            config.SetPrivateField(BoardLengthFieldName, boardLength);
            
            var gameStartData = config.GetGameStartData(0, 1);
            
            Assert.AreEqual(boardWidth, gameStartData.BoardData.BoardWidth);
            Assert.AreEqual(boardLength, gameStartData.BoardData.BoardLength);
        }
        
        [Test]
        [TestCaseSource(nameof(CellTypes))]
        public void GetGameStartData_ShouldReturnGameStartData_WhereBoardDataHasGivenCellType(CellType expectedCellType)
        {
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            config.SetPrivateField(CellTypeFieldName, expectedCellType);
            
            var gameStartData = config.GetGameStartData(0, 1);
            
            Assert.AreEqual(expectedCellType, gameStartData.BoardData.CellType);
        }
        
        private static IEnumerable<CellType> CellTypes()
        {
            yield return CellType.Square;
            yield return CellType.Triangle;
        }

        [Test]
        public void GetGameStartData_ShouldReturnGameStartData_WithTwoGeneratedContents()
        {
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            
            var gameStartData = config.GetGameStartData(0, 1);
            var contentList = gameStartData.BoardData.GeneratedContent;
            
            Assert.AreEqual(2, contentList.Count);
        }
        
        [Test]
        public void GetGameStartData_ShouldReturnGameStartData_WithProperlyConfiguredPlayerCastle()
        {
            var rnd = new Random();
            var expectedPosition = new Vector2Int(rnd.Next(), rnd.Next());
            var expectedMaxFreeDurability = rnd.Next();
            var expectedMaxDurability = rnd.Next();
            var expectedDurability = rnd.Next();
            var expectedCaptureHitCost = rnd.Next();
            var expectedOwnerId = rnd.Next();
            var expectedCastleData = new CastleData(expectedPosition, 
                expectedCaptureHitCost, 
                expectedMaxFreeDurability,
                expectedMaxDurability, 
                expectedDurability, 
                expectedOwnerId);
            
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            var playerCastleConfig = config.GetPrivateField<object>(PlayerCastleConfigFieldName);
            playerCastleConfig.SetPublicField(MaxFreeDurabilityFieldName, expectedMaxFreeDurability);
            playerCastleConfig.SetPublicField(MaxDurabilityFieldName, expectedMaxDurability);
            playerCastleConfig.SetPublicField(DurabilityFieldName, expectedDurability);
            playerCastleConfig.SetPublicField(CaptureHitCostFieldName, expectedCaptureHitCost);
            config.SetPrivateField(PlayerBasePositionFieldName, expectedPosition);
            
            var gameStartData = config.GetGameStartData(expectedOwnerId, 1);
            var contentList = gameStartData.BoardData.GeneratedContent;
            
            Assert.Contains(expectedCastleData, contentList);
        }
        
        [Test]
        public void GetGameStartData_ShouldReturnGameStartData_WithProperlyConfiguredEnemyCastle()
        {
            var rnd = new Random();
            var expectedPosition = new Vector2Int(rnd.Next(), rnd.Next());
            var expectedMaxFreeDurability = rnd.Next();
            var expectedMaxDurability = rnd.Next();
            var expectedDurability = rnd.Next();
            var expectedCaptureHitCost = rnd.Next();
            var expectedOwnerId = rnd.Next();
            var expectedCastleData = new CastleData(expectedPosition, 
                expectedCaptureHitCost, 
                expectedMaxFreeDurability,
                expectedMaxDurability, 
                expectedDurability, 
                expectedOwnerId);
            
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            var enemyCastleConfig = config.GetPrivateField<object>(EnemyCastleConfigFieldName);
            enemyCastleConfig.SetPublicField(MaxFreeDurabilityFieldName, expectedMaxFreeDurability);
            enemyCastleConfig.SetPublicField(MaxDurabilityFieldName, expectedMaxDurability);
            enemyCastleConfig.SetPublicField(DurabilityFieldName, expectedDurability);
            enemyCastleConfig.SetPublicField(CaptureHitCostFieldName, expectedCaptureHitCost);
            config.SetPrivateField(EnemyBasePositionFieldName, expectedPosition);
            
            var gameStartData = config.GetGameStartData(0, expectedOwnerId);
            var contentList = gameStartData.BoardData.GeneratedContent;
            
            Assert.Contains(expectedCastleData, contentList);
        }
        
        [Test]
        public void GetGameStartData_ShouldReturnGameStartData_WhereCellsPresenceHasCorrectSize()
        {
            var rnd = new Random();
            var boardLength = rnd.Next(1, 100);
            var boardWidth = rnd.Next(1, 100);
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            config.SetPrivateField(BoardWidthFieldName, boardWidth);
            config.SetPrivateField(BoardLengthFieldName, boardLength);
            
            var gameStartData = config.GetGameStartData(0, 1);
            var cellPresence = gameStartData.BoardData.CellsPresence;
            
            Assert.AreEqual(boardLength, cellPresence.GetLength(0));
            Assert.AreEqual(boardWidth, cellPresence.GetLength(1));
        }

        [Test]
        public void GetGameStartData_ShouldReturnGameStartData_WhereCellsPresenceIsFullTrue()
        {
            var boardLength = 10;
            var boardWidth = 10;
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            config.SetPrivateField(BoardWidthFieldName, boardWidth);
            config.SetPrivateField(BoardLengthFieldName, boardLength);
            
            var gameStartData = config.GetGameStartData(0, 1);
            
            for (var i = 0; i < boardLength; i++)
            {
                for (var j = 0; j < boardWidth; j++)
                {
                    Assert.True(gameStartData.BoardData.CellsPresence[i, j]);
                }
            }
        }

        [Test]
        public void GetGameStartData_ShouldReturnGameStartData_WithCorrectPlaceablesConfigData()
        {
            var rnd = new Random();
            var expectedKnightPlacementCost = rnd.Next();
            var expectedKnightHealth = rnd.Next();
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            config.SetPrivateField(KnightPlacementCostFieldName, expectedKnightPlacementCost);
            config.SetPrivateField(KnightHealthFieldName, expectedKnightHealth);
            var expectedKnightConfig = new KnightConfigData(expectedKnightPlacementCost, expectedKnightHealth);
            
            var gameStartData = config.GetGameStartData(0, 1);
            var placeablesConfigData = gameStartData.PlaceablesConfigData;
            var knightConfig = placeablesConfigData.KnightConfig;
            
            Assert.AreEqual(expectedKnightConfig, knightConfig);
        }
        
        [Test]
        public void GetGameStartData_ShouldReturnGameStartData_WithCorrectTscConfigData()
        {
            var expectedDefaultTscTypes = new List<TscType> {TscType.SwitchByActionPoints};
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            config.SetPrivateField(DefaultTscTypesFieldName, expectedDefaultTscTypes);
            var gameStartData = config.GetGameStartData(0, 1);
            var tscConfigData = gameStartData.TscConfigData;
            
            Assert.AreEqual(expectedDefaultTscTypes, tscConfigData.TscTypes);
        }

        [Test]
        public void GetGameStartData_ShouldReturnGameStartData_WithTwoPlayerDataInstances()
        {
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            
            var gameStartData = config.GetGameStartData(0, 1);
            
            Assert.AreEqual(2, gameStartData.PlayersData.Count);
        }
        
        [Test]
        //Player data in this test means the PlayerData related to the player, that is not to the enemy.
        public void GetGameStartData_ShouldReturnGameStartData_WithProperlySetPlayerData()
        {
            var rnd = new Random();
            var expectedTimeSpan = new TimeSpan(rnd.Next(), rnd.Next(), rnd.Next(), rnd.Next(), rnd.Next());
            var expectedPlayerId = rnd.Next();
            var expectedAvailablePlacementTypes = new List<PlacementType> {PlacementType.Knight};
            var expectedPlayerData = new PlayerData(expectedPlayerId, expectedAvailablePlacementTypes, expectedTimeSpan);
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            config.SetPrivateField(DefaultTscTypesFieldName, expectedTimeSpan);
            config.SetPrivateField(DefaultAvailablePlacementTypesFieldName, expectedAvailablePlacementTypes);
            
            var gameStartData = config.GetGameStartData(expectedPlayerId, 1);
            var playersData = gameStartData.PlayersData;
         
            Assert.Contains(expectedPlayerData, playersData);
        }
        
        [Test]
        public void GetGameStartData_ShouldReturnGameStartData_WithProperlySetEnemyData()
        {
            var rnd = new Random();
            var expectedTimeSpan = new TimeSpan(rnd.Next(), rnd.Next(), rnd.Next(), rnd.Next(), rnd.Next());
            var expectedEnemyId = rnd.Next();
            var expectedAvailablePlacementTypes = new List<PlacementType> {PlacementType.Knight};
            var expectedEnemyData = new PlayerData(expectedEnemyId, expectedAvailablePlacementTypes, expectedTimeSpan);
            var config = ScriptableObject.CreateInstance<TutorialGameStartDataConfig>();
            config.SetPrivateField(DefaultTscTypesFieldName, expectedTimeSpan);
            config.SetPrivateField(DefaultAvailablePlacementTypesFieldName, expectedAvailablePlacementTypes);
            
            var gameStartData = config.GetGameStartData(0, expectedEnemyId);
            var playersData = gameStartData.PlayersData;
         
            Assert.Contains(expectedEnemyData, playersData);
        }
    }
}