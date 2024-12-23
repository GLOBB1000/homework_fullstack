using Modules;
using UnityEngine;
using Zenject;

namespace SnakeController
{
    public class SnakeInstaller : MonoInstaller
    {
        [SerializeField] private Snake snakeInstance;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Snake>().FromInstance(snakeInstance).AsSingle();
            Container.BindInterfacesAndSelfTo<SnakeMovementController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SnakeCollisionBehaviour>().FromNew().AsSingle().NonLazy();
        }
    }
}