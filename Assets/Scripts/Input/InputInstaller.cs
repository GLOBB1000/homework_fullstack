using Zenject;

namespace Input
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<KeyBoardInput>().FromNew().AsSingle();
        }
    }
}