using System;
using System.Collections.Generic;
using castledice_game_data_logic.Content;
using castledice_game_data_logic.Content.Generated;
using castledice_game_logic;
using castledice_game_logic.BoardGeneration.ContentGeneration;

namespace Src.GameplayPresenter.GameCreation.GameCreationProviders
{
    public class ContentToCoordinateProvider : IGeneratedContentDataVisitor<ContentToCoordinate>
    {
        private readonly List<Player> _players;

        public ContentToCoordinateProvider(List<Player> players)
        {
            _players = players;
        }

        public ContentToCoordinate GetContentToCoordinate(GeneratedContentData data)
        {
            throw new NotImplementedException();
        }
        
        public ContentToCoordinate VisitCastleData(CastleData data)
        {
            throw new System.NotImplementedException();
        }

        public ContentToCoordinate VisitTreeData(TreeData data)
        {
            throw new System.NotImplementedException();
        }
    }
}