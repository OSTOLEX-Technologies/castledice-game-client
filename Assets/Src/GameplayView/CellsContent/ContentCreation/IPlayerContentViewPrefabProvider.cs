﻿using castledice_game_logic;
using Src.GameplayView.CellsContent.ContentViews;

namespace Src.GameplayView.CellsContent.ContentCreation
{
    public interface IPlayerContentViewPrefabProvider
    {
        KnightView GetKnightPrefab(Player player);
        CastleView GetCastlePrefab(Player player);
    }
}