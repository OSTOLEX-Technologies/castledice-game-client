using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using castledice_game_data_logic;
using castledice_game_data_logic.ConfigsData;
using castledice_game_data_logic.Content;
using castledice_game_data_logic.TurnSwitchConditions;
using castledice_game_logic;
using castledice_game_logic.ActionPointsLogic;
using castledice_game_logic.BoardGeneration.CellsGeneration;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using static Tests.EditMode.GameplayViewTests.ContentVisualsTests.PlayerColoredContentVisualFieldNames;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.GameObjects.Configs;
using castledice_game_logic.GameObjects.Factories.Castles;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.Time;
using castledice_game_logic.TurnsLogic.TurnSwitchConditions;
using Moq;
using Src.GameplayPresenter;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayView.Audio;
using Src.GameplayView.ContentVisuals;
using Src.GameplayView.Grid;
using static Tests.EditMode.GameplayViewTests.ContentVisualsTests.ContentVisualsFieldNames;
using Tests.Utils.Mocks;
using UnityEngine;
using CastleGO = castledice_game_logic.GameObjects.Castle;
using Random = System.Random;
using Tree = castledice_game_logic.GameObjects.Tree;
using Vector2Int = castledice_game_logic.Math.Vector2Int;


namespace Tests.Utils
{
    public static class ObjectCreationUtility
    {
        private static Random _random = new Random();
        
        public static TimeSpan GetRandomTimeSpan(int min = 1, int max = 1000)
        {
            var ticks = _random.Next(min, max);
            return new TimeSpan(ticks);
        }
        
        public static SoundPlayer GetSoundPlayer()
        {
            return new GameObject().AddComponent<AudioSourceSoundPlayer>();
        }
        
        public static Sound GetSound()
        {
            return new Sound(GetAudioClip(), 1);
        }
        
        public static AudioClip GetAudioClip()
        {
            return AudioClip.Create("someClip", 1, 1, 1001, false);
        }
        
        public static void SetPrivateFieldValue<T>(T value, object obj, string propertyName)
        {
            FieldInfo field = obj.GetType().GetField(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(obj, value);
            }
        }
        
        public static Mock<TestGame> GetTestGameMock()
        {
            var player = GetPlayer(1);
            var secondPlayer = GetPlayer(2);
            var playersList = new List<Player> { player, secondPlayer };
            var gameMock = new Mock<TestGame>(playersList, GetBoardConfig(new Dictionary<Player, Vector2Int>
            {
                {player, (0, 0)},
                {secondPlayer, (9, 9)}
            }), GetPlaceablesConfig(), GetTurnSwitchConditionsConfig());
            gameMock.Setup(x => x.GetPlayer(It.IsAny<int>())).Returns(player);
            gameMock.Setup(x => x.GetAllPlayers()).Returns(playersList);
            gameMock.Setup(x => x.GetAllPlayersIds()).Returns(new List<int> { 1, 2 });
            gameMock.Setup(g => g.GetCurrentPlayer()).Returns(GetPlayer(1));
            return gameMock;
        }
        
        public static Mock<Game> GetGameMock()
        {
            var player = GetPlayer(1);
            var secondPlayer = GetPlayer(2);
            var playersList = new List<Player> { player, secondPlayer };
            var gameMock = new Mock<Game>(playersList, GetBoardConfig(new Dictionary<Player, Vector2Int>
            {
                {player, (0, 0)},
                {secondPlayer, (9, 9)}
            }), GetPlaceablesConfig(), GetTurnSwitchConditionsConfig());
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
            var boardData = GetBoardData();
            var placeablesConfigs = new PlaceablesConfigData(new KnightConfigData(1, 2));
            var playersData = new List<PlayerData>
            {
                GetPlayerData(id: 1),
                GetPlayerData(id: 2)
            };
            var tscConfigData = GetTscConfigData();
            var data = new GameStartData(version, boardData, placeablesConfigs, tscConfigData, playersData);
            return data;
        }
        

        public static TurnSwitchConditionsConfig GetTurnSwitchConditionsConfig()
        {
            return new TurnSwitchConditionsConfig(new List<TscType> { TscType.SwitchByActionPoints });
        }
        
        public static TscConfigData GetTscConfigData()
        {
            return new TscConfigData(new List<TscType> { TscType.SwitchByActionPoints });
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
            var firstPlayer = GetPlayer(id: 1);
            var secondPlayer = GetPlayer(id: 2);
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

            var turnSwitchConditionsConfig = GetTurnSwitchConditionsConfig();
            
            var game = new Game(players, boardConfig, placeablesConfig, turnSwitchConditionsConfig);

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
        
        public static List<Player> GetPlayersList(int length = 1)
        {
            var players = new List<Player>();
            for (int i = 0; i < length; i++)
            {
                players.Add(GetPlayer(id: i));
            }
            return players;
        }
        
        public static List<PlayerData> GetPlayersDataList(int length = 1)
        {
            var playersData = new List<PlayerData>();
            for (int i = 0; i < length; i++)
            {
                playersData.Add(GetPlayerData(id: i));
            }
            return playersData;
        }
        
        
        public static PlayerData GetPlayerData(int id = 0, TimeSpan timeSpan = new(), List<PlacementType> placementTypes = null)
        {
            return new PlayerData(id, placementTypes ?? new List<PlacementType>(), timeSpan);
        }
        
        public static PlayerData GetPlayerData(int id = 0, TimeSpan timeSpan = new(), params PlacementType[] placementTypes)
        {
            return new PlayerData(id, placementTypes.ToList(), timeSpan);
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

        public static CastleGO GetCastle(Player player, int durability = 3, int maxDurability = 3,
            int maxFreeDurability = 1, int captureHitCost = 1)
        {
            return new CastleGO(player, durability, maxDurability, maxFreeDurability, captureHitCost);
        }
        
        public static CastleGO GetCastle()
        {
            return new CastleGO(GetPlayer(id: 1), 3, 3, 1, 1);
        }

        public static Knight GetKnight(Player player, int placementCost = 1, int health = 2)
        {
            return new Knight(player, placementCost, health);
        }
        
        public static Knight GetKnight()
        {
            return new Knight(GetPlayer(id: 1), 1, 2);
        }
        
        public static Tree GetTree()
        {
            return new Tree(1, false);
        }
        
        public static Player GetPlayer(int id = 1, int actionPointsCount = 6, IPlayerTimer timer = null)
        {
            var actionPoints = new PlayerActionPoints();
            actionPoints.IncreaseActionPoints(actionPointsCount);
            timer ??= new Mock<IPlayerTimer>().Object;
            return new Player(actionPoints, timer, new List<PlacementType> { PlacementType.Knight }, id);
        }

        public static IPossibleMovesListProvider GetPossibleMovesListProvider()
        {
            var mock = new Mock<IPossibleMovesListProvider>();
            mock.Setup(x => x.GetPossibleMoves(It.IsAny<Vector2Int>(), It.IsAny<int>())).Returns(new List<AbstractMove>());
            return mock.Object;
        }

        public static AbstractMove GetMove()
        {
            return GetMove(GetPlayer());
        }

        public static AbstractMove GetMove(Player player)
        {
            return GetMove(player, new Vector2Int(0, 0));
        }
        
        public static AbstractMove GetMove(Player player, Vector2Int position)
        {
            return new PlaceMove(player, position, GetKnight());
        }
        
        public static List<Renderer> GetRenderersListWithMaterial(Material material, int count)
        {
            var renderers = new List<Renderer>();
            for (var i = 0; i < count; i++)
            {
                renderers.Add(GetRendererWithMaterial(material));
            }
            return renderers;
        }
        
        public static Renderer GetRendererWithMaterial(Material material)
        {
            var gameObject = new GameObject();
            var renderer = gameObject.AddComponent<MeshRenderer>();
            renderer.material = material;
            return renderer;
        }
        
        public static Renderer GetRendererWithMultipleMaterials(List<Material> materials)
        {
            var gameObject = new GameObject();
            var renderer = gameObject.AddComponent<MeshRenderer>();
            renderer.materials = materials.ToArray();
            return renderer;
        }
        
        public static List<Material> GetMaterialsList(int count)
        {
            var materials = new List<Material>();
            for (var i = 0; i < count; i++)
            {
                materials.Add(GetMaterialWithColor(UnityEngine.Random.ColorHSV()));
            }
            return materials;
        }
        
        public static Material GetMaterialWithColor(Color color)
        {
            var material = new Material(Shader.Find("Standard"))
            {
                color = color
            };
            return material;
        }
        
        public static KnightVisual GetKnightVisual()
        {
            var visual = new GameObject().AddComponent<KnightVisual>();
            var meshRenderer = visual.gameObject.AddComponent<MeshRenderer>();
            visual.SetPrivateField(ColoringAffectedRenderersFieldName, GetCompoundRenderer(new List<Renderer>
            {
                meshRenderer
            }));
            visual.SetPrivateField(TransparencyAffectedRenderersFieldName, GetCompoundRenderer(new List<Renderer>
            {
                meshRenderer
            }));
            return visual;
        }
        
        public static List<TreeVisual> GetTreeVisualsList(int count)
        {
            var visuals = new List<TreeVisual>();
            for (var i = 0; i < count; i++)
            {
                visuals.Add(GetTreeVisual());
            }
            return visuals;
        }
        
        public static TreeVisual GetTreeVisual()
        {
            var visual = new GameObject().AddComponent<TreeVisual>();
            var renderer = new GameObject().AddComponent<MeshRenderer>();
            visual.SetPrivateField(TransparencyAffectedRenderersFieldName, GetCompoundRenderer(new List<Renderer>
            {
                renderer
            }));
            return visual;
        }

        public static CastleVisual GetCastleVisual()
        {
            var visual = new GameObject().AddComponent<CastleVisual>();
            var renderer = new GameObject().AddComponent<MeshRenderer>();
            visual.SetPrivateField(TransparencyAffectedRenderersFieldName, GetCompoundRenderer(new List<Renderer>
            {
                renderer
            }));
            visual.SetPrivateField(ColoringAffectedRenderersFieldName, GetCompoundRenderer(new List<Renderer>
            {
                renderer
            }));
            return visual;
        }

        public static CompoundRenderer GetCompoundRenderer(List<Renderer> renderers)
        {
            var compoundRenderer = new CompoundRenderer();
            compoundRenderer.SetPrivateField("renderers", renderers);
            return compoundRenderer;
        }
        
        public static List<Renderer> GetRenderersList(int count)
        {
            var renderers = new List<Renderer>();
            for (var i = 0; i < count; i++)
            {
                renderers.Add(new GameObject().AddComponent<MeshRenderer>());
            }
            return renderers;
        }
        
        public static List<Vector2Int> GetRandomVector2IntList(int minCoordinateValue, int maxCoordinateValue, int count)
        {
            var list = new List<Vector2Int>();
            for (var i = 0; i < count; i++)
            {
                list.Add(GetRandomVector2Int(minCoordinateValue, maxCoordinateValue));
            }
            return list;
        }

        public static Vector2Int GetRandomVector2Int(int minCoordinateValue = 0, int maxCoordinateValue = 10)
        {
            var x = _random.Next(minCoordinateValue, maxCoordinateValue);
            var y = _random.Next(minCoordinateValue, maxCoordinateValue);
            return new Vector2Int(x, y);
        }

        public static ContentVisual GetContentVisual()
        {
            return GetTreeVisual();
        }
        
        public static List<Content> GetCellContentList(int count)
        {
            return Enumerable.Repeat(GetCellContent(), count).ToList();
        }
        
        public static List<Mock<IGridCell>> GetGridCellMocksList(int count)
        {
            var list = new List<Mock<IGridCell>>();
            for (var i = 0; i < count; i++)
            {
                list.Add(new Mock<IGridCell>());
            }
            return list;
        }
        
        public static List<int> GetRandomIntList(int count)
        {
            var list = new List<int>();
            for (var i = 0; i < count; i++)
            {
                list.Add(_random.Next());
            }
            return list;
        }
        
        public static List<int> GetRandomIntList(int min, int max, int count)
        {
            var list = new List<int>();
            for (var i = 0; i < count; i++)
            {
                list.Add(_random.Next(min, max));
            }
            return list;
        }
    }
}