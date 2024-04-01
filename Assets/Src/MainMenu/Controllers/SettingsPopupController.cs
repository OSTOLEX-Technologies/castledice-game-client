using Src.MainMenu.Views;

namespace Src.MainMenu.Controllers
{
    public class SettingsPopupController
    {
        
        public SettingsPopupController(SettingsPopupView settingsPopupView)
        {
            _settingsPopupView = settingsPopupView;
        }
        
        private SettingsPopupView _settingsPopupView;
        
        public void UpdateName(string name)
        {
            _settingsPopupView.ChangeName(name);
        }
        
        public void UpdateSound(int sound)
        {
            _settingsPopupView.ChangeSound(sound);
        }
        
        public void UpdateMusic(int music)
        {
            _settingsPopupView.ChangeMusic(music);
        }
        
        public void UpdateVoice(int voice)
        {
            _settingsPopupView.ChangeVoice(voice);
        }
    }
}