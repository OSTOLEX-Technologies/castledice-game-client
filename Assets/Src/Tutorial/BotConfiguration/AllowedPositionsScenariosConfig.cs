using System.Collections.Generic;
using UnityEngine;

namespace Src.Tutorial.BotConfiguration
{
    [CreateAssetMenu(fileName = "AllowedPositionsScenariosConfig", menuName = "Configs/Tutorial/AllowedPositionsScenariosConfig")]
    public class AllowedPositionsScenariosConfig : ScriptableObject
    {
        [SerializeField] private List<AllowedPositionsScenario> scenarios;
        
        public List<AllowedPositionsScenario> Scenarios => scenarios;
    }
}