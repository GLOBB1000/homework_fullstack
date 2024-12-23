using GameCycleInstances;
using Zenject;

namespace GameCycleInstances
{
    public class GameCycleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameCycle>().FromNew().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameCycleController>().FromNew().AsSingle().NonLazy();
        }
    }
}