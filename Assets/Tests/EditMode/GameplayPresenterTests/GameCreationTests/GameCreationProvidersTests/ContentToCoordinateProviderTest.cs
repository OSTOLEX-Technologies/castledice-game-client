using System.Collections.Generic;
using castledice_game_data_logic.Content;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;
using static Tests.ObjectCreationUtility;
using Vector2Int = castledice_game_logic.Math.Vector2Int;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.GameCreationProvidersTests
{
    public class ContentToCoordinateProviderTest
    {
        public static Vector2Int[] Positions = { (0, 0), (0, 1), (1, 0), (1, 1) };
        
        [Test]
        public void GetContentToCoordinate_ShouldThrowArgumentException_IfCastleDataWithUnknownIdIsGiven()
        {
            var players = new List<Player> {GetPlayer(1), GetPlayer(2) };
            var castleData = new CastleData((0, 0), 1, 1, 1, 1, 4);
            var contentToCoordinateProvider = new ContentToCoordinateProvider();
            
            Assert.Throws<System.ArgumentException>(() => contentToCoordinateProvider.GetContentToCoordinate(castleData, players));
        }

        [Test]
        public void GetContentToCoordinate_ShouldThrowArgumentException_IfKnightDataWithUnknownIdIsGiven()
        {
            var players = new List<Player> {GetPlayer(1), GetPlayer(2) };
            var knightData = new KnightData((0, 0), 1, 1, 3);
            var contentToCoordinateProvider = new ContentToCoordinateProvider();
            
            Assert.Throws<System.ArgumentException>(() => contentToCoordinateProvider.GetContentToCoordinate(knightData, players));
        }

        [Test]
        public void GetContentToCoordinate_ShouldReturnContentToCoordinateWithAppropriatePosition([ValueSource(nameof(Positions))]Vector2Int position)
        {
            var players = new List<Player> {GetPlayer(1), GetPlayer(2) };
            var castleData = new CastleData(position, 1, 1, 1, 1, 1);
            var contentToCoordinateProvider = new ContentToCoordinateProvider();
            
            var contentToCoordinate = contentToCoordinateProvider.GetContentToCoordinate(castleData, players);
            
            Assert.That(contentToCoordinate.Coordinate, Is.EqualTo(position));
        }

        [Test]
        [TestCase(1, 2, 3, 3, 1)]
        [TestCase(1, 1, 5, 4, 13)]
        [TestCase(2, 3, 5, 2, 4)]
        public void GetContentToCoordinate_ShouldReturnContentToCoordinateWithAppropriateCastle_IfCastleDataGiven(int captureHitCost, int maxFreeDurability, int maxDurability, int durability, int ownerId)
        {
            var owner = GetPlayer(ownerId);
            var players = new List<Player> {owner, GetPlayer(2) };
            var castleData = new CastleData((0, 0), captureHitCost, maxFreeDurability, maxDurability, durability, ownerId);
            var contentToCoordinateProvider = new ContentToCoordinateProvider();
            
            var contentToCoordinate = contentToCoordinateProvider.GetContentToCoordinate(castleData, players);
            var content = contentToCoordinate.Content;
            var castle = content as CastleGO;
            
            Assert.AreEqual(castleData.Durability, castle.GetDurability());
            Assert.AreEqual(castleData.CaptureHitCost, castle.GetCaptureHitCost(GetPlayer()));
            Assert.AreSame(owner, castle.GetOwner());
            Assert.AreEqual(castleData.MaxDurability, castle.GetMaxDurability());
            castle.Free();
            Assert.AreEqual(castleData.MaxFreeDurability, castle.GetDurability());
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(2, false)]
        public void GetContentToCoordinate_ShouldReturnContentToCoordinateWithAppropriateTree_IfTreeDataGiven(int removeCost, bool canBeRemoved)
        {
            var treeData = new TreeData((0, 0), removeCost, canBeRemoved);
            var contentToCoordinateProvider = new ContentToCoordinateProvider();
            
            var contentToCoordinate = contentToCoordinateProvider.GetContentToCoordinate(treeData, new List<Player>());
            var content = contentToCoordinate.Content;
            var tree = content as Tree;

            Assert.AreEqual(treeData.CanBeRemoved, tree.CanBeRemoved());
            Assert.AreEqual(treeData.RemoveCost, tree.GetRemoveCost());
        }

        [Test]
        [TestCase(1, 2, 3)]
        [TestCase(2, 3, 4)]
        [TestCase(3, 4, 5)]
        public void GetContentToCoordinate_ShouldReturnContentToCoordinateWithAppropriateKnight_IfKnightDataGiven(int health, int placeCost, int ownerId)
        {
            var owner = GetPlayer(ownerId);
            var players = new List<Player> {owner, GetPlayer(2) };
            var knightData = new KnightData((0, 0), health, placeCost, ownerId);
            var contentToCoordinateProvider = new ContentToCoordinateProvider();
            
            var contentToCoordinate = contentToCoordinateProvider.GetContentToCoordinate(knightData, players);
            var content = contentToCoordinate.Content;
            var knight = content as Knight;
            
            Assert.AreEqual(knightData.Health, knight.GetReplaceCost());
            Assert.AreEqual(knightData.PlaceCost, knight.GetPlacementCost());
            Assert.AreSame(owner, knight.GetOwner());
        }
    }
}