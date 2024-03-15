using System;
using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.ConfigsData;
using castledice_game_data_logic.Content;
using castledice_game_data_logic.TurnSwitchConditions;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;
using Vector2IntUnity = UnityEngine.Vector2Int;

namespace Src.Tutorial
{
    [CreateAssetMenu(fileName = "TutorialGameStartDataConfig", menuName = "Configs/Tutorial/TutorialGameStartDataConfig")]
    public class TutorialGameStartDataConfig : ScriptableObject, ITutorialGameStartDataProvider
    {
        
        
        [SerializeField] private int boardWidth = 10;
        [SerializeField] private int boardLength = 10;
        [SerializeField] private CellType cellType = CellType.Square;
        [SerializeField] private Vector2IntUnity enemyBasePosition = new Vector2IntUnity(9, 9);
        [SerializeField] private Vector2IntUnity playerBasePosition = new Vector2IntUnity(0, 0);
        [SerializeField] private TutorialCastleConfig playerCastleConfig = new TutorialCastleConfig
        {
            MaxDurability = 3,
            MaxFreeDurability = 3,
            Durability = 3,
            CaptureHitCost = 1
        };
        [SerializeField] private TutorialCastleConfig enemyCastleConfig = new TutorialCastleConfig
        {
            MaxDurability = 3,
            MaxFreeDurability = 3,
            Durability = 3,
            CaptureHitCost = 1
        };
        [SerializeField] private int knightPlacementCost = 1;
        [SerializeField] private int knightHealth = 2;
        
        private readonly TimeSpan defaultTimeSpan = TimeSpan.MaxValue;
        private const string defaultVersion = "tutorial";
        private readonly List<TscType> defaultTscTypes = new List<TscType> {TscType.SwitchByActionPoints};
        private readonly List<PlacementType> defaultPlacementTypes = new List<PlacementType> {PlacementType.Knight};
        
        public GameStartData GetGameStartData(int playerId, int enemyId)
        {
            var playerBasePositionGl = new Vector2Int(playerBasePosition.x, playerBasePosition.y);
            var enemyBasePositionGl = new Vector2Int(enemyBasePosition.x, enemyBasePosition.y);
            var cellsPresence = new bool[boardLength,boardWidth];
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    cellsPresence[i, j] = true;
                }
            }
            var playerCastleData = new CastleData(
                playerBasePositionGl, 
                playerCastleConfig.CaptureHitCost, 
                playerCastleConfig.MaxFreeDurability, 
                playerCastleConfig.MaxDurability, 
                playerCastleConfig.Durability, 
                playerId);
            var enemyCastleData = new CastleData(
                enemyBasePositionGl, 
                enemyCastleConfig.CaptureHitCost, 
                enemyCastleConfig.MaxFreeDurability, 
                enemyCastleConfig.MaxDurability, 
                enemyCastleConfig.Durability, 
                enemyId);
            var contentData = new List<ContentData>
            {
                playerCastleData,
                enemyCastleData
            };
            var boardData = new BoardData(boardLength, boardWidth, cellType, cellsPresence, contentData);
            var placeablesConfigData = new PlaceablesConfigData(
                new KnightConfigData(knightPlacementCost, knightHealth));
            var tscConfigData = new TscConfigData(defaultTscTypes);
            var playerData = new PlayerData(playerId, defaultPlacementTypes, defaultTimeSpan);
            var enemyData = new PlayerData(enemyId, defaultPlacementTypes, defaultTimeSpan);
            var gameStartData = new GameStartData(
                defaultVersion, 
                boardData, 
                placeablesConfigData, 
                tscConfigData, 
                new List<PlayerData>
                {
                    playerData, enemyData
                });
            return gameStartData;
        }

        [Serializable]
        private class TutorialCastleConfig
        {
            public int MaxFreeDurability;
            public int MaxDurability;
            public int Durability;
            public int CaptureHitCost;
        }
    }
}