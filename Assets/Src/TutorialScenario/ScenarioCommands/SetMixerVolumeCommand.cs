using UnityEngine;
using UnityEngine.Audio;

namespace Src.TutorialScenario.ScenarioCommands
{
    public class SetMixerVolumeCommand : TutorialScenarioCommand
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private string parameterName;
        [SerializeField] private float targetVolume;

        private float _cachedVolume;
        
        public override void Do()
        {
            mixer.GetFloat(parameterName, out _cachedVolume);
            mixer.SetFloat(parameterName, targetVolume);
        }

        public override void Undo()
        {
            mixer.SetFloat(parameterName, _cachedVolume);
        }
    }
}