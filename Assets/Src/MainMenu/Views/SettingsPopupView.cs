using UnityEngine;

namespace Src.MainMenu.Views
{
    public class SettingsPopupView
    {
        private string _name;
        private float _sound;
        private float _music;
        private float _voice;
        private Sprite _avatar;
        
        public void ChangeName(string name)
        {
            _name = name;
        }
        
        public void ChangeSound(float sound)
        {
            _sound = sound;
        }
        
        public void ChangeMusic(float music)
        {
            _music = music;
        }
        
        public void ChangeVoice(float voice)
        {
            _voice = voice;
        }
        
        public float GetSound()
        {
            return _sound;
        }

        public float GetMusic()
        {
            return _music;
        }

        public float GetVoice()
        {
            return _voice;
        }

        public string GetName()
        {
            return _name;
        }

        public Sprite GetAvatar()
        {
            return _avatar;
        }
    }
}