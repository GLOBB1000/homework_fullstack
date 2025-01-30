using System;
using Game.Views.Popup;
using Modules.Planets;
using UnityEngine;

namespace Game.View.Popup
{
    public class PlanetPopupPresenter : IPlanetPopupPresenter, IDisposable
    {
        private PlanetPopup _planetPopup;
        
        private IPlanet _currentPlanet;

        public PlanetPopupPresenter(PlanetPopup planetPopup)
        {
            _planetPopup = planetPopup;

            _planetPopup.OnClosed += ClosePopup;
            _planetPopup.OnUpgraded += OnUpgraded;
        }

        private void OnUpgraded()
        {
            if(_currentPlanet == null)
                return;

            if (_currentPlanet.IsMaxLevel)
            {
                _planetPopup.SetUpgrade(0, true);
                return;
            }
            
            if(!_currentPlanet.Upgrade())
                Debug.Log("Can't upgrade planet");
        }
        
        private void OnUpgraded(int obj)
        {
            UpdatePopup();
        }

        public void ClosePopup()
        {
            Unsubscribe();
        }

        public void ShowPopup(IPlanet planet)
        {
            _currentPlanet = planet;

            UpdatePopup();
            
            _currentPlanet.OnPopulationChanged += OnPopulationChanged;
            _currentPlanet.OnUpgraded += OnUpgraded;
            _planetPopup.Show(planet.GetIcon(true), _currentPlanet.Name);
        }

        private void UpdatePopup()
        {
            _planetPopup.SetLevel(_currentPlanet.Level);
            _planetPopup.SetPopulation(_currentPlanet.Population);
            _planetPopup.SetIncome(_currentPlanet.MinuteIncome);
            _planetPopup.SetUpgrade(_currentPlanet.Price, _currentPlanet.IsMaxLevel);
        }
        
        private void OnPopulationChanged(int obj)
        {
            _planetPopup.SetPopulation(obj);
        }
        
        private void Unsubscribe()
        {
            if(_currentPlanet == null)
                return;
            
            _currentPlanet.OnPopulationChanged -= OnPopulationChanged;
            _currentPlanet.OnUpgraded -= OnUpgraded;
            
            _currentPlanet = null;
        }

        public void Dispose()
        {
            Unsubscribe();
            
            _planetPopup.OnClosed -= ClosePopup;
            _planetPopup.OnUpgraded -= OnUpgraded;
        }
    }
}