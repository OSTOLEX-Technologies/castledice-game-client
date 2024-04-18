using UnityEngine;

namespace Src.MainMenu.Views
{
    public class SettingsPopupView
    {
        private string _name;
        private int _sound;
        private int _music;
        private int _voice;
        private Sprite _avatar;
        
        public void ChangeName(string name)
        {
            _name = name;
        }
        
        public void ChangeSound(int sound)
        {
            _sound = sound;
        }
        
        public void ChangeMusic(int music)
        {
            _music = music;
        }
        
        public void ChangeVoice(int voice)
        {
            _voice = voice;
        }
        
        public int GetSound()
        {
            return _sound;
        }

        public int GetMusic()
        {
            return _music;
        }

        public int GetVoice()
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