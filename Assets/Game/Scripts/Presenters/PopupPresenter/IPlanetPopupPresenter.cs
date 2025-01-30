using System;
using Modules.Planets;

namespace Game.View.Popup
{
    public interface IPlanetPopupPresenter
    {
        void ClosePopup();
        
        void ShowPopup(IPlanet planet);
    }
}