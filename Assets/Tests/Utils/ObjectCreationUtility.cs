using System.Collections.Generic;
using castledice_game_data_logic;
using castledice_game_data_logic.ConfigsData;
using castledice_game_data_logic.Content;
using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories;
using castledice_game_logic.GameObjects.Factories.Castles;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using Src.GameplayPresenter;
using Src.GameplayPresenter.ClientMoves;
using Src.GameplayPresenter.GameWrappers;
using Tests.Mocks;
using CastleGO = castledice_game_logic.GameObjects.Castle;


namespace Tests
{
    public static class ObjectCreationUtility
    {
        public static Mock<Game> GetGameMock()
        {
            var player = GetPlayer(1);
            var secondPlayer = GetPlayer(2);
            var playersList = new List<Player> { player, secondPlayer };
            var gameMock = new Mock<Game>(playersList, GetBoardConfig(new Dictionary<Player, Vector2Int>
            {
                {player, (0, 0)},
                {secondPlayer, (9, 9)}
            }), GetPlaceablesConfig(), new Mock<IDecksList>().Object);
            gameMock.Setup(x => x.GetPlayer(It.IsAny<int>())).Returns(player);
            gameMock.Setup(x => x.GetAllPlayers()).Returns(playersList);
            gameMock.Setup(x => x.GetAllPlayersIds()).Returns(new List<int> { 1, 2 });
            gameMock.Setup(g => g.GetCurrentPlayer()).Returns(GetPlayer(1));
            return gameMock;
        }
        
        public static PlaceablesConfig GetPlaceablesConfig()
        {
            var unitsConfig = new PlaceablesConfig(new KnightConfig(1, 2));
            return unitsConfig;
        }
        
        public static CastleData GetCastleData()
        {
            return new CastleData((0, 0), 1, 1, 3, 3, 1);
        }

        public static TreeData GetTreeData()
        {
            return new TreeData((0, 0), 3, false);
        }
        
        public static GameStartData GetGameStartData()
        {
            var version = "1.0.0";
            var playerIds = new List<int>() { 1, 2 };
            var boardData = GetBoardData();
            var placeablesConfigs = new PlaceablesConfigData(new KnightConfigData(1, 2));
            var playerDecks = new List<PlayerDeckData>()
            {
                new(playerIds[0], new List<PlacementType> { PlacementType.Knight }),
                new (playerIds[1], new List<PlacementType> { PlacementType.Knight })
            };
            var data = new GameStartData(version, boardData, placeablesConfigs, playerIds, playerDecks);
            return data;
        }

        public static BoardData GetBoardData(CellType cellType = CellType.Square)
        {
            var cellsPresence = GetValuesMatrix(10, 10, true);
            return GetBoardData(cellsPresence, cellType);
        }
        
        public static BoardData GetBoardData(bool[,] cellsPresence, CellType cellType = CellType.Square)
        {
            var boardLength = 10;
            var boardWidth = 10;
            var firstCastle = new CastleData((0, 0), 1, 1, 3, 3, 1);
            var secondCastle = new CastleData((9, 9), 1, 1, 3, 3, 2);
            var generatedContent = new List<ContentData>
            {
                firstCastle, 
                secondCastle
            };
            return new BoardData(boardLength, boardWidth, cellType, cellsPresence, generatedContent);
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
            
            var boardConfig = GetBoardConfig(playersToCastlesPositions);

            var placeablesConfig = new PlaceablesConfig(new KnightConfig(1, 2));

            var placementListProvider = new CommonDecksList(new List<PlacementType>()
            {
                PlacementType.Knight
            });
            
            var game = new Game(players, boardConfig, placeablesConfig, placementListProvider);

            return game;
        }

        public static BoardConfig GetBoardConfig()
        {
            var playersToCastlesPositions = new Dictionary<Player, Vector2Int>()
            {
                { GetPlayer(1), (0, 0) },
                { GetPlayer(2), (9, 9) }
            };
            return GetBoardConfig(playersToCastlesPositions);
        }
        public static BoardConfig GetBoardConfig(Player firstPlayer, Player secondPlayer)
        {
            var playersToCastlesPositions = new Dictionary<Player, Vector2Int>()
            {
                { firstPlayer, (0, 0) },
                { secondPlayer, (9, 9) }
            };
            return GetBoardConfig(playersToCastlesPositions);
        }
        
        
        public static BoardConfig GetBoardConfig(Dictionary<Player, Vector2Int> playersToCastlesPositions)
        {

            var castleConfig = new CastleConfig(3, 1, 1);
            var castlesFactory = new CastlesFactory(castleConfig);
            var castlesSpawner = new CastlesSpawner(playersToCastlesPositions, castlesFactory);

            var contentSpawners = new List<IContentSpawner>()
            {
                castlesSpawner
            };

            var cellsGenerator = new RectCellsGenerator(10, 10);
            
            var boardConfig = new BoardConfig(contentSpawners, cellsGenerator, CellType.Square);
            
            return boardConfig;
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

        public static ContentData GetGeneratedContentData()
        {
            return new Mock<ContentData>().Object;
        }
        
        public static Content GetCellContent()
        {
            return new ObstacleMock();
        }
        
        public static CastleGO GetCastle()
        {
            return new CastleGO(new Player(new PlayerActionPoints(), 0), 3, 3, 1, 1);
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
            return GetPlayer(1);
        }

        public static Player GetPlayer(int id)
        {
            return new Player(new PlayerActionPoints(), id);
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