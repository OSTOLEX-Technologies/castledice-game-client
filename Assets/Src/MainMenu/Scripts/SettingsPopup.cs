using System;
using Src.MainMenu.Controllers;
using Src.MainMenu.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

namespace Src.MainMenu.Scripts
{
    public class SettingsPopup: MonoBehaviour
    {
        private Button _topBarAccountButton;
        private Button _topBarSupportButton;
        private Button _topBarSubmitABugButton;
        private Button _topBarAboutUsButton;
        private Button _topBarCreditsButton;
        
        private Button _avatarButton;
        private TMP_InputField _nameInputField;
        private Slider _soundSlider;
        private Slider _musicSlider;
        private Slider _voiceSlider;
        
        private Button _cancelButton;
        
        public SettingsPopupView SettingsPopupView;
        public SettingsPopupController SettingsPopupController;

        public void Init()
        {
            // TODO: get data from server and IStringSaver
            _nameInputField.text = SettingsPopupView.GetName();
            _soundSlider.value = SettingsPopupView.GetSound();
            _musicSlider.value = SettingsPopupView.GetMusic();
            _voiceSlider.value = SettingsPopupView.GetVoice();
        }
        
        private void Awake()
        {
            // TODO: review with guys and decide if we need to use Find or SerializeField
            // IStringSaver, 
            _topBarAccountButton = transform.Find("TopBar/Account").GetComponent<Button>();
            _topBarSupportButton = transform.Find("TopBar/Support").GetComponent<Button>();
            _topBarSubmitABugButton = transform.Find("TopBar/Submit a bug").GetComponent<Button>();
            _topBarAboutUsButton = transform.Find("TopBar/About us").GetComponent<Button>();
            _topBarCreditsButton = transform.Find("TopBar/Credits").GetComponent<Button>();
            _nameInputField = transform.Find("NameInputField").GetComponent<TMP_InputField>();
            _soundSlider = transform.Find("SlidersSettings/Sliders/SoundSlider").GetComponent<Slider>();
            _musicSlider = transform.Find("SlidersSettings/Sliders/MusicSlider").GetComponent<Slider>();
            _voiceSlider = transform.Find("SlidersSettings/Sliders/VoiceSlider").GetComponent<Slider>();
            _cancelButton = transform.Find("CancelButton").GetComponent<Button>();
            
            _soundSlider.onValueChanged.AddListener(delegate { SettingsPopupController.UpdateSound(Convert.ToInt32(_soundSlider.value)); });
            _musicSlider.onValueChanged.AddListener(delegate { SettingsPopupController.UpdateMusic(Convert.ToInt32(_musicSlider.value)); });
            _voiceSlider.onValueChanged.AddListener(delegate { SettingsPopupController.UpdateVoice(Convert.ToInt32(_voiceSlider.value)); });
            _nameInputField.onValueChanged.AddListener(delegate { SettingsPopupController.UpdateName(_nameInputField.text); });
        }
    }
}