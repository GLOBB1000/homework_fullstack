using System;
using System.Collections.Generic;
using Game.View;
using Modules.Planets;
using UnityEngine;

namespace Game.View.Popup
{
    public class PlanetPopupShower
    {
        private IPlanetPopupPresenter _planetPopupPresenter;
        
        public PlanetPopupShower(IPlanetPopupPresenter planetPopupPresenter)
        {
            _planetPopupPresenter = planetPopupPresenter;
        }

        public void Show(IPlanet planet)
        {
            _planetPopupPresenter.ShowPopup(planet);
        }
    }
}