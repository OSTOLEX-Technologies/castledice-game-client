using System;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    [Serializable]
    public abstract class TutorialScenarioCommand : MonoBehaviour
    {
        public abstract void Do();
        
        public abstract void Undo();
    }
}