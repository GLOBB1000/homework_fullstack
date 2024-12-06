using GameCycle;
using GamePlay;
using ScoreHandler;
using SnakeBehaviour;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GamePlayInstaller : MonoInstaller
    {
        [SerializeField]
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SnakeMovementController>().FromNew().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameCycleController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GamePlayController>().FromNew().AsSingle().NonLazy();
            Container.Bind<SnakeCollisionBehaviour>().FromNew().AsSingle().NonLazy();
        }
    }
}