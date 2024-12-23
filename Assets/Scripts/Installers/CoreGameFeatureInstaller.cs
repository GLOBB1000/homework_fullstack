using Coins;
using Input;
using Modules;
using ScoreHandler;
using SnakeController;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CoreGameFeatureInstaller : MonoInstaller
    {
        [SerializeField] private WorldBounds worldBoundsInstance;

        [SerializeField] private int maxDiff;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WorldBounds>().FromInstance(worldBoundsInstance).AsSingle();
            Container.BindInterfacesAndSelfTo<Difficulty>().FromNew().AsSingle().WithArguments(maxDiff);
        }
    }
}