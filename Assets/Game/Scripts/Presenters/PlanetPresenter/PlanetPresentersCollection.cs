using System;
using System.Collections.Generic;
using Game.Views;
using Modules.Planets;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

namespace Game.View
{
    public class PlanetPresentersCollection : IInitializable
    {
        public event Action<IPlanetPresenter> OnPlanetAdded; 
        
        [Inject] private PresenterFactory presenterFactory;
        [Inject] private List<IPlanet> planets;
        [Inject] private List<PlanetView> planetViews;
        
        public void Initialize()
        {
            for (int i = 0; i < planetViews.Count; i++)
                OnPlanetAdded?.Invoke(presenterFactory.Create(planets[i], planetViews[i]));
        }
    }
}