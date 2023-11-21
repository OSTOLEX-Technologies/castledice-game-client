using System;
using castledice_game_logic.Math;
using JetBrains.Annotations;

namespace Src.GameplayView.ClickDetection
{
    public interface ICellClickDetector
    {
        event EventHandler<Vector2Int> Clicked; 
    }
}