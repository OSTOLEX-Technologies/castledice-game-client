using TMPro;
using UnityEngine;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class SetTextCommand : TutorialScenarioCommand
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private string textToSet;
        private string _cachedText;
        
        public override void Do()
        {
            _cachedText = textMesh.text;
            textMesh.text = textToSet;
        }

        public override void Undo()
        {
            textMesh.text = _cachedText;
        }
    }
}