using UnityEngine;
using UnityEngine.Rendering;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class Highlight3DObjectsByUrpLayerOverrideCommand : TutorialScenarioCommand
    {
        [InspectorName("Default Render Pipeline Asset")]
        [SerializeField] private RenderPipelineAsset defaultRPAsset;
        [InspectorName("Highlight Render Pipeline Asset")]
        [SerializeField] private RenderPipelineAsset highlightRPAsset;
        [SerializeField] private bool highlight;
        private RenderPipelineAsset _cachedPipeline;

        public override void Do()
        {
            _cachedPipeline = QualitySettings.renderPipeline;
            QualitySettings.renderPipeline = highlight ? highlightRPAsset : defaultRPAsset;
        }

        public override void Undo()
        {
            QualitySettings.renderPipeline = _cachedPipeline;
        }
    }
}