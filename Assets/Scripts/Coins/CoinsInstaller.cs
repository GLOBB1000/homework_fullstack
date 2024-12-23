using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Coins
{
    public class CoinsInstaller : MonoInstaller
    {
        [Inject] private WorldBounds worldBoundsInstance;
        
        [SerializeField] private Coin coinPrefab;
        
        public override void InstallBindings()
        {
            Container.BindMemoryPool<Coin, CoinsPool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(coinPrefab)
                .UnderTransform(worldBoundsInstance.transform)
                .AsSingle();

            Container.Bind<ICoinsSpawner>().To<CoinsPool>().FromResolve();
            Container.BindInterfacesAndSelfTo<CoinsSpawnerController>().FromNew().AsSingle();
        }
    }
}