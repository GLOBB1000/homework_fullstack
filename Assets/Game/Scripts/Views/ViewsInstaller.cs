using Game.Scripts.Views.PlanetView;
using Game.Views.MoneyView;
using Game.Views.Popup;
using Modules.UI;
using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            PlanetViewInstaller.Install(Container);
            MoneyViewInstaller.Install(Container);
            
            Container.Bind<PlanetPopup>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<ParticleAnimator>().FromComponentInHierarchy().AsSingle();
        }
    }
}