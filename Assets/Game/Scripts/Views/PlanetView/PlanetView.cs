using System;
using Modules.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Game.Views
{
    public class PlanetView : MonoBehaviour
    {
        public event Action OnPlanetSelected;
        public event Action OnPopupOpened;
        
        [SerializeField]
        private SmartButton button;

        [SerializeField] private GameObject lockIcon;
        
        [SerializeField] private Image planetIcon;
        
        [SerializeField] private TimerBar timerBar;
        
        [SerializeField] private TextMeshProUGUI planetPriceText;
        
        [SerializeField] private GameObject incomeIcon;

        [Inject]
        public void Construct()
        {
            button.OnClick += OnClick;
            button.OnHold += OnLongClick;
        }

        private void OnLongClick()
        {
            OnPopupOpened?.Invoke();
        }

        public void SetIcon(Sprite icon)
        {
            planetIcon.sprite = icon;
        }

        private void OnClick()
        {
            Debug.Log("Clicked");
            OnPlanetSelected?.Invoke();
        }

        public void Unlock()
        {
            lockIcon.SetActive(false);
            planetPriceText.transform.parent.gameObject.SetActive(false);
            
            planetIcon.color = Color.green;
            timerBar.gameObject.SetActive(true);
        }
        
        public void SetPrice(int price)
        {
            planetPriceText.text = price.ToString();
        }

        public void ChangeTime(float time, float income)
        {
            timerBar.ChangeTimerBar(time, income);
        }

        public void SetIncomeReady(bool ready)
        {
            incomeIcon.SetActive(ready);
            timerBar.gameObject.SetActive(!ready);
        }
    }
}