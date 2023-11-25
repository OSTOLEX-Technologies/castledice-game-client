using System.Collections.Generic;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.PlayersRotations
{
    public interface IPlayerRotationConfig
    {
        /// <summary>
        /// Each number corresponds to player`s turn order. If player moves first, he will have rotation with key 1.
        /// </summary>
        /// <returns></returns>
        Dictionary<PlayerColor, Vector3> GetRotations();
    }
}