using System;
using Src.MainMenu.Controllers;
using Src.MainMenu.Views;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

namespace Src.MainMenu.Scripts
{
    public class SettingsPopup: MonoBehaviour
    {
        [SerializeField] private Button _topBarAccountButton;
        [SerializeField] private Button _topBarSupportButton;
        [SerializeField] private Button _topBarSubmitABugButton;
        [SerializeField] private Button _topBarAboutUsButton;
        [SerializeField] private Button _topBarCreditsButton;
        
        [SerializeField] private Button _avatarButton;
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private Slider _soundSlider; 
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _voiceSlider;
        
        [SerializeField] AudioMixer audioMixer;
        
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _settingsButton;
        
        public SettingsPopupView SettingsPopupView;
        public SettingsPopupController SettingsPopupController;

        public void Init()
        {
            SettingsPopupController.InitSound(audioMixer);
            _nameInputField.text = SettingsPopupView.GetName();
            _soundSlider.value = SettingsPopupView.GetSound();
            _musicSlider.value = SettingsPopupView.GetMusic();
            _voiceSlider.value = SettingsPopupView.GetVoice();
            
            _soundSlider.onValueChanged.AddListener(delegate { SettingsPopupController.UpdateSound(_soundSlider.value, audioMixer); });
            _musicSlider.onValueChanged.AddListener(delegate { SettingsPopupController.UpdateMusic(_musicSlider.value, audioMixer); });
            _voiceSlider.onValueChanged.AddListener(delegate { SettingsPopupController.UpdateVoice(_voiceSlider.value, audioMixer); });
            _nameInputField.onValueChanged.AddListener(delegate { SettingsPopupController.UpdateName(_nameInputField.text); });
            
            _closeButton.onClick.AddListener(Close);
            _settingsButton.onClick.AddListener(Open);
        }
        
        private void Close()
        {
            gameObject.SetActive(false);
        }

        private void Open()
        {
            gameObject.SetActive(true);
        }
    }
}