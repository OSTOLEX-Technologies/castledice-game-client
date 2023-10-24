using System;
using castledice_game_logic;
using Src.GameplayView.CellsContent.ContentViews;

namespace Src.GameplayView.CellsContent.ContentCreation
{
    public class PlayerContentViewPrefabProvider : IPlayerContentViewPrefabProvider
    {
        private readonly IPlayerColorProvider _playerColorProvider;
        private readonly IPlayerContentViewPrefabsConfig _config;

        public PlayerContentViewPrefabProvider(IPlayerColorProvider playerColorProvider, IPlayerContentViewPrefabsConfig config)
        {
            _playerColorProvider = playerColorProvider;
            _config = config;
        }

        public KnightView GetKnightPrefab(Player player)
        {
            var playerColor = _playerColorProvider.GetPlayerColor(player);
            return playerColor switch
            {
                PlayerColor.Blue => _config.BlueKnightPrefab,
                PlayerColor.Red => _config.RedKnightPrefab,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public CastleView GetCastlePrefab(Player player)
        {
            var playerColor = _playerColorProvider.GetPlayerColor(player);
            return playerColor switch
            {
                PlayerColor.Blue => _config.BlueCastlePrefab,
                PlayerColor.Red => _config.RedCastlePrefab,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}