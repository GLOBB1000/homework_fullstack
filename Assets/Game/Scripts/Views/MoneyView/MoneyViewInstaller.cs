using Zenject;

namespace Game.Views.MoneyView
{
    public class MoneyViewInstaller : Installer<MoneyViewInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<MoneyView>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}