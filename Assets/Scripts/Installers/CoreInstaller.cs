using CoinsHandlers;
using InputHandlers;
using Modules;
using ScoreHandler;
using SnakeBehaviour;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private Snake snakeInstance;
        [SerializeField] private WorldBounds worldBoundsInstance;
        
        [SerializeField] private Coin coinPrefab;

        [SerializeField] private int maxDiff;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<KeyBoardInput>().FromNew().AsSingle();
            
            Container.BindInterfacesAndSelfTo<Snake>().FromInstance(snakeInstance).AsSingle();
            Container.BindInterfacesAndSelfTo<WorldBounds>().FromInstance(worldBoundsInstance).AsSingle();
            
            Container.BindInterfacesAndSelfTo<Score>().FromNew().AsSingle();

            Container.BindInterfacesAndSelfTo<Difficulty>().FromNew().AsSingle().WithArguments(maxDiff);
            
            Container.BindMemoryPool<Coin, CoinsPool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(coinPrefab)
                .UnderTransform(worldBoundsInstance.transform)
                .AsSingle();

            Container.Bind<ICoinsSpawner>().To<CoinsPool>().FromResolve();
        }
    }
}