using Zenject;

namespace Game.Scripts.Views.PlanetView
{
    public class PlanetViewInstaller : Installer<PlanetViewInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Game.Views.PlanetView>().FromComponentsInHierarchy().AsCached();
        }
    }
}