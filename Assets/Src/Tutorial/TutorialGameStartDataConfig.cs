using System;
using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.ConfigsData;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.Tutorial
{
    public class TutorialGameStartDataConfig : ScriptableObject, ITutorialGameStartDataProvider
    {
        
        
        [SerializeField] private int boardWidth = 10;
        [SerializeField] private int boardLength = 10;
        [SerializeField] private CellType cellType = CellType.Square;
        [SerializeField] private Vector2Int enemyBasePosition = (9, 9);
        [SerializeField] private Vector2Int playerBasePosition = (0, 0);
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
            throw new NotImplementedException();
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