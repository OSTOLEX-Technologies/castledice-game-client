using System.Collections.Generic;
using castledice_game_logic.Math;
using Vector2IntUnity = UnityEngine.Vector2Int;

namespace Tests.EditMode.GeneralTests
{
    public static class Vector2IntExtensions
    {
        public static Vector2Int ConvertToGameLogicVector2Int(this Vector2IntUnity vector2IntUnity) =>
            new Vector2Int(vector2IntUnity.x, vector2IntUnity.y);
        
        public static List<Vector2Int> ConvertToGameLogicVector2IntList(this List<Vector2IntUnity> vector2IntUnityList) =>
            vector2IntUnityList.ConvertAll(vector2IntUnity => vector2IntUnity.ConvertToGameLogicVector2Int());
    }
}