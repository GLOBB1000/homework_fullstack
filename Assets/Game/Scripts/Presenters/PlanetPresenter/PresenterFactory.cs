using Game.Views;
using Modules.Planets;
using Zenject;

namespace Game.View
{
    public class PresenterFactory : PlaceholderFactory<IPlanet, PlanetView, PlanetPresenter>
    {

    }
}