using Src.MainMenu.Views;
using UnityEngine;
using UnityEngine.Audio;

namespace Src.MainMenu.Controllers
{
    public class SettingsPopupController
    {
        
        public SettingsPopupController(SettingsPopupView settingsPopupView)
        {
            _settingsPopupView = settingsPopupView;
        }
        
        private SettingsPopupView _settingsPopupView;

        public void InitSound(AudioMixer mixer)
        {
            UpdateSound(PlayerPrefs.GetFloat("soundVolume", 1f), mixer);
            UpdateMusic(PlayerPrefs.GetFloat("musicVolume", 1f), mixer);
            UpdateVoice(PlayerPrefs.GetFloat("voiceVolume", 1f), mixer);
        }

        public void UpdateName(string name)
        {
            _settingsPopupView.ChangeName(name);
        }
        
        public void UpdateSound(float sound, AudioMixer mixer)
        {
            _settingsPopupView.ChangeSound(sound);
            mixer.SetFloat("sound", Mathf.Log10(sound) * 20);
            PlayerPrefs.SetFloat("soundVolume", sound);
        }
        
        public void UpdateMusic(float music, AudioMixer mixer)
        {
            _settingsPopupView.ChangeMusic(music);
            mixer.SetFloat("music", Mathf.Log10(music) * 20);
            PlayerPrefs.SetFloat("musicVolume", music);
        }

        public void UpdateVoice(float voice, AudioMixer mixer)
        {
            _settingsPopupView.ChangeVoice(voice);
            mixer.SetFloat("voice", Mathf.Log10(voice) * 20);
            PlayerPrefs.SetFloat("voiceVolume", voice);
        }
    }
}