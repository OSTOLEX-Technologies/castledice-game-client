using System;
using UnityEngine;
using UnityEngine.UI;

namespace Src.GameplayPresenter.EnemyDisconnect
{
    public class EnemyDisconnectView : IEnemyDisconnectView
    {
        private readonly GameObject _popup;
        private readonly Button _okButton;
        
        public event Action OkPressed;
        
        public EnemyDisconnectView(GameObject popup, Button okButton)
        {
            _popup = popup;
            _okButton = okButton;
            _okButton.onClick.AddListener(() => OkPressed?.Invoke());
        }

        public void ShowPopup()
        {
            _popup.SetActive(true);
        }
    }
}