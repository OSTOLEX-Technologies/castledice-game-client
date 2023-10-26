using System.Collections.Generic;
using castledice_game_data_logic.Content.Generated;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.GameCreationProviders;
using static Tests.ObjectCreationUtility;
using Vector2Int = castledice_game_logic.Math.Vector2Int;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Tests.EditMode
{
    public class ContentToCoordinateProviderTest
    {
        public static Vector2Int[] Positions = { (0, 0), (0, 1), (1, 0), (1, 1) };
        
        [Test]
        public void GetContentToCoordinate_ShouldThrowArgumentException_IfCastleDataWithUnknownIdIsGiven()
        {
            var players = new List<Player> {GetPlayer(1), GetPlayer(2) };
            var castleData = new CastleData((0, 0), 1, 1, 1, 1, 4);
            var contentToCoordinateProvider = new ContentToCoordinateProvider(players);
            
            Assert.Throws<System.ArgumentException>(() => contentToCoordinateProvider.GetContentToCoordinate(castleData));
        }

        [Test]
        public void GetContentToCoordinate_ShouldReturnContentToCoordinateWithAppropriatePosition([ValueSource(nameof(Positions))]Vector2Int position)
        {
            var players = new List<Player> {GetPlayer(1), GetPlayer(2) };
            var castleData = new CastleData(position, 1, 1, 1, 1, 1);
            var contentToCoordinateProvider = new ContentToCoordinateProvider(players);
            
            var contentToCoordinate = contentToCoordinateProvider.GetContentToCoordinate(castleData);
            
            Assert.That(contentToCoordinate.Coordinate, Is.EqualTo(position));
        }

        [Test]
        public void GetContentToCoordinate_ShouldReturnContentToCoordinateWithAppropriateCastle_IfCastleDataGiven()
        {
            var players = new List<Player> {GetPlayer(1), GetPlayer(2) };
            var castleData = new CastleData((0, 0), 1, 2, 3, 4, 1);
            var contentToCoordinateProvider = new ContentToCoordinateProvider(players);
            
            var contentToCoordinate = contentToCoordinateProvider.GetContentToCoordinate(castleData);
            var content = contentToCoordinate.Content;
            var castle = content as CastleGO;
            
            //TODO: Make it possible to create castle with given durability.
            Assert.AreEqual(castleData.CastleCaptureHitCost, castle.GetCaptureHitCost(GetPlayer()));
            Assert.AreEqual(castleData.OwnerId, castle.GetOwner().Id);
            Assert.AreEqual(castleData.DefaultDurability, castle.GetMaxDurability());
            castle.Free();
            Assert.AreEqual(castleData.FreeDurability, castle.GetDurability());
        }

        [Test]
        public void GetContentToCoordinate_ShouldReturnContentToCoordinateWithAppropriateTree_IfTreeDataGiven()
        {
            var treeData = new TreeData((0, 0), 3, true);
            var contentToCoordinateProvider = new ContentToCoordinateProvider(new List<Player>());
            
            var contentToCoordinate = contentToCoordinateProvider.GetContentToCoordinate(treeData);
            var content = contentToCoordinate.Content;
            var tree = content as Tree;

            Assert.AreEqual(treeData.CanBeRemoved, tree.CanBeRemoved());
            Assert.AreEqual(treeData.RemoveCost, tree.GetRemoveCost());
        }
    }
}