using System;
using Game.View.Popup;
using Game.Views;
using Modules.Planets;
using UnityEngine;

namespace Game.View
{
    public class PlanetPresenter : IPlanetPresenter, IDisposable
    {
        public event Action<Transform> OnMoneyGathered;

        private PlanetView _planetView;
        
        private IPlanet _currentPlanet;
        
        private PlanetPopupShower _planetPopupShower;

        public PlanetPresenter(IPlanet planet, PlanetView view, PlanetPopupShower popupShower)
        {
            _currentPlanet = planet;
            _planetView = view;
            _planetPopupShower = popupShower;
            
            SetUpView();
            SetUpPlanet();
        }

        private void SetUpView()
        {
            _planetView.OnPlanetSelected += ViewOnClick;
            _planetView.OnPopupOpened += OnPopupOpened;
            _planetView.SetPrice(_currentPlanet.Price);
            _planetView.SetIcon(_currentPlanet.GetIcon(_currentPlanet.IsUnlocked));
        }

        private void OnPopupOpened()
        {
            if(!_currentPlanet.IsUnlocked)
                return;
            
            _planetPopupShower.Show(_currentPlanet);
        }

        private void SetUpPlanet()
        {
            _currentPlanet.OnUnlocked += OnPlanetUnlocked;
            _currentPlanet.OnIncomeTimeChanged += OnIncomeTimeChanged;
            _currentPlanet.OnIncomeReady += OnIncomeReady;
        }

        private void OnIncomeReady(bool obj)
        {
            _planetView.SetIncomeReady(obj);
        }

        private void OnIncomeTimeChanged(float time)
        {
            _planetView.ChangeTime(time, _currentPlanet.IncomeProgress);
        }

        private void ViewOnClick()
        {
            Debug.Log("Click on planet");

            if (_currentPlanet.GatherIncome())
            {
                OnMoneyGathered?.Invoke(_planetView.transform);
            }
            
            if(_currentPlanet.IsUnlocked)
                return;
            
            if (!_currentPlanet.Unlock())
                return;
        }

        private void OnPlanetUnlocked()
        {
            _planetView.Unlock();
            _planetView.SetIcon(_currentPlanet.GetIcon(_currentPlanet.IsUnlocked));
        }

        public void Dispose()
        {
            _currentPlanet.OnUnlocked -= OnPlanetUnlocked;
            _currentPlanet.OnIncomeTimeChanged -= OnIncomeTimeChanged;
            _currentPlanet.OnIncomeReady -= OnIncomeReady;
            
            _planetView.OnPlanetSelected -= ViewOnClick;
            _planetView.OnPopupOpened -= OnPopupOpened;
        }
    }
}