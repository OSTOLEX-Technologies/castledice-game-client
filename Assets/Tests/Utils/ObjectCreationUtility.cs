﻿using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.Content.Generated;
using castledice_game_data_logic.Content.Placeable;
using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using Src.GameplayPresenter;
using Src.GameplayPresenter.ClientMoves;
using Tests.Mocks;
using CastleGO = castledice_game_logic.GameObjects.Castle;


namespace Tests
{
    public static class ObjectCreationUtility
    {
        public static GameStartData GetGameStartData(CellType cellType = CellType.Square)
        {
            var cellsPresence = GetValuesMatrix(10, 10, true);
            return GetGameStartData(cellsPresence, cellType);
        }
        
        public static GameStartData GetGameStartData(bool[,] cellsPresence, CellType cellType = CellType.Square)
        {
            var boardLength = 10;
            var boardWidth = 10;
            var playerIds = new List<int>() { 1, 2 };
            var firstCastle = new CastleData((0, 0), 1, 1, 3, 3, playerIds[0]);
            var secondCastle = new CastleData((9, 9), 1, 1, 3, 3, playerIds[1]);
            var generatedContent = new List<GeneratedContentData>
            {
                firstCastle, 
                secondCastle
            };
            var placeablesConfigs = new List<PlaceableContentData>
            {
                new KnightData(1, 2)
            };
            var playerDecks = new List<PlayerDeckData>()
            {
                new(playerIds[0], new List<PlacementType> { PlacementType.Knight }),
                new (playerIds[1], new List<PlacementType> { PlacementType.Knight })
            };
            var data = new GameStartData(boardLength, boardWidth, cellType, cellsPresence, generatedContent, placeablesConfigs, playerIds, playerDecks);
            return data;
        }
        
        public static T[,] GetValuesMatrix<T>(int length, int width, T value)
        {
            var matrix = new T[length, width];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = value;
                }
            }

            return matrix;
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
        
        public static Board GetFullNByNBoard(int size)
        {
            var board = new Board(CellType.Square);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board.AddCell(i, j);
                }
            }
            return board;
        }
        
        public static Content GetCellContent()
        {
            return new ObstacleMock();
        }
        
        public static CastleGO GetCastle()
        {
            return new CastleGO(new Player(new PlayerActionPoints(), 0), 1, 2, 3);
        }
        
        public static Knight GetKnight()
        {
            return new Knight(new Player(new PlayerActionPoints(), 0), 1, 2);
        }
        
        public static Tree GetTree()
        {
            return new Tree(1, false);
        }

        public static Player GetPlayer()
        {
            return new Player(new PlayerActionPoints(), 0);
        }

        public static IPlayerDataProvider GetPlayerDataProvider(bool authorized = true, string token = "token", int id = 1)
        {
            var mock = new Mock<IPlayerDataProvider>();
            mock.Setup(x => x.IsAuthorized()).Returns(authorized);
            mock.Setup(x => x.GetAccessToken()).Returns(token);
            mock.Setup(x => x.GetId()).Returns(id);
            return mock.Object;
        }
        
        public static IPossibleMovesListProvider GetPossibleMovesListProvider()
        {
            var mock = new Mock<IPossibleMovesListProvider>();
            mock.Setup(x => x.GetPossibleMoves(It.IsAny<Vector2Int>(), It.IsAny<int>())).Returns(new List<AbstractMove>());
            return mock.Object;
        }

        public static AbstractMove GetMove()
        {
            var mock = new Mock<AbstractMove>(GetPlayer(), new Vector2Int(0, 0));
            return mock.Object;
        }
    }
}