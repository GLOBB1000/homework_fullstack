using Game.View.Popup;
using Game.Views;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.View
{
    [CreateAssetMenu(
        fileName = "PresentersInstallers",
        menuName = "Zenject/New PresentersInstallers"
    )]
    public sealed class PresentersInstallers : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            //TODO:
            
            Container.BindInterfacesAndSelfTo<MoneyPresenter.MoneyPresenter>().AsSingle().NonLazy();
            
            Container.Bind<PlanetPopupShower>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlanetPopupPresenter>().AsSingle().NonLazy();
            
            Container.BindFactory<IPlanet, PlanetView, PlanetPresenter, PresenterFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlanetPresentersCollection>().AsSingle().NonLazy();
        }
    }
}