using Modules;
using Zenject;

namespace ScoreHandler
{
    public class ScoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Score>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreObserver>().AsSingle().NonLazy();
        }
    }
}