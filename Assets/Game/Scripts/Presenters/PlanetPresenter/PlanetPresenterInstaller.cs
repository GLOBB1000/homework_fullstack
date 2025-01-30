using System.Collections.Generic;
using Game.Scripts.Views.PlanetView;
using Game.Views;
using Modules.Planets;
using Zenject;

namespace Game.View
{
    public class PlanetPresenterInstaller : Installer<PlanetPresenterInstaller>
    {
        [Inject] private List<IPlanet> planets;
        [Inject] private List<PlanetView> planetViews;
        
        public override void InstallBindings()
        {
            PlanetViewInstaller.Install(Container);
            
            for (int i = 0; i < planetViews.Count; i++)
            {
                Container.BindInterfacesAndSelfTo<PlanetPresenter>().FromNew().AsCached();
            }
        }
    }
}