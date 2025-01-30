using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Views.Popup
{
    public class PlanetPopup : MonoBehaviour
    {
        public event Action OnClosed;
        public event Action OnUpgraded;
        
        [Space]
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _upgrageButton;
        
        [Space]
        [SerializeField] private Image _planetImage;
        [SerializeField] private TextMeshProUGUI _planetName;
        
        [Space]
        [SerializeField] private TextMeshProUGUI _populatuionText;
        [SerializeField] private TextMeshProUGUI _incomeText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _upgradeText;

        [Inject]
        public void Construct()
        {
            _closeButton.onClick.AddListener(Hide);
            _upgrageButton.onClick.AddListener(Upgrade);
        }

        private void Upgrade()
        {
            OnUpgraded?.Invoke();
        }

        public void Show(Sprite planetImage, string planetName)
        {
            _planetImage.sprite = planetImage;
            _planetName.text = planetName;
            
            gameObject.SetActive(true);
        }

        public void SetPopulation(int population)
        {
            _populatuionText.text = $"Population: {population}";
        }

        public void SetLevel(int level)
        {
            _levelText.text = $"Level: {level}";
        }

        public void SetIncome(int income)
        {
            _incomeText.text = $"Income: {income}$";
        }

        public void SetUpgrade(int upgrade, bool isMaxLevel = false)
        {
            _upgradeText.text = upgrade.ToString();
            
            if (isMaxLevel)
                _upgradeText.text = "Max level";
        }

        private void Hide()
        {
            gameObject.SetActive(false);

            OnClosed?.Invoke();
        }
    }
}