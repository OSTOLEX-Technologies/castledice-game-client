using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;

namespace Tests
{
    public static class ObjectCreationUtility
    {
        public static GameStartData GetGameStartData()
        {
            var data = new GameStartData()
            {
                CellType = CellType.Square,
                CellsPresence = new[,]
                {
                    {true, true, true},
                    {true, true, true},
                    {true, true, true}
                },
                KnightHealth = 2,
                KnightPlaceCost = 1,
                PlayersIds = new List<int>(){1, 2}
            };
            data.Decks.Add(new PlayerDeckData(){PlayerId = 1, AvailablePlacements = { PlacementType.Knight }});
            data.Decks.Add(new PlayerDeckData(){PlayerId = 2, AvailablePlacements = { PlacementType.Knight }});
            return data;
        }

        public static Game GetGame()
        {
            var firstPlayer = new Player(new PlayerActionPoints(), 1);
            var secondPlayer = new Player(new PlayerActionPoints(), 2);
            var players = new List<Player>()
            {
                firstPlayer,
                secondPlayer
            };

            var playersToCastlesPositions = new Dictionary<Player, Vector2Int>()
            {
                { firstPlayer, (0, 0) },
                { secondPlayer, (9, 9) }
            };
            var castleConfig = new CastleConfig(3, 1, 1);
            var castlesFactory = new CastlesFactory(castleConfig);
            var casltesSpawner = new CastlesSpawner(playersToCastlesPositions, castlesFactory);

            var contentSpanwers = new List<IContentSpawner>()
            {
                casltesSpawner
            };

            var cellsGenerator = new RectCellsGenerator(10, 10);
            
            var boardConfig = new BoardConfig(contentSpanwers, cellsGenerator, CellType.Square);

            var unitsConfig = new UnitsConfig(new KnightConfig(1, 2));

            var placementListProvider = new CommonPlacementListProvider(new List<PlacementType>()
            {
                PlacementType.Knight
            });
            
            var game = new Game(players, boardConfig, unitsConfig, placementListProvider);

            return game;
        }
    }
}