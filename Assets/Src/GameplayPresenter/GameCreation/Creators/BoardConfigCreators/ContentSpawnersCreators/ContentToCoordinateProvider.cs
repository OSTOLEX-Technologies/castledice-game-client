using System;
using System.Collections.Generic;
using castledice_game_data_logic.Content;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;
using castledice_game_logic.GameObjects;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators
{
    public class ContentToCoordinateProvider : IContentDataVisitor<ContentToCoordinate>, IContentToCoordinateProvider
    {
        private List<Player> _players;

        public ContentToCoordinate GetContentToCoordinate(ContentData data, List<Player> players)
        {
            _players = players;
            return data.Accept(this);
        }

        public ContentToCoordinate VisitCastleData(CastleData data)
        {
            var player = _players.Find(p => p.Id == data.OwnerId);
            if (player == null)
            {
                throw new ArgumentException("Castle data with unknown owner id " + data.OwnerId);
            }
            var castle = new CastleGO(player, data.Durability, data.MaxDurability, data.MaxFreeDurability, data.CaptureHitCost);
            return new ContentToCoordinate(data.Position, castle);
        }

        public ContentToCoordinate VisitTreeData(TreeData data)
        {
            var tree = new Tree(data.RemoveCost, data.CanBeRemoved);
            return new ContentToCoordinate(data.Position, tree);
        }

        public ContentToCoordinate VisitKnightData(KnightData data)
        {
            var player = _players.Find(p => p.Id == data.OwnerId);
            if (player == null)
            {
                throw new ArgumentException("Knight data with unknown owner id " + data.OwnerId);
            }
            var knight = new Knight(player, data.PlaceCost, data.Health);
            return new ContentToCoordinate(data.Position, knight);
        }
    }
}