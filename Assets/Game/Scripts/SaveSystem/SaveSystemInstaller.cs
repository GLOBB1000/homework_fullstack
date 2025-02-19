using Game.Scripts.SaveSystem.Presenters;
using SampleGame.Gameplay;
using SampleGame.SerializedData;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Scripts.SaveSystem
{
    [CreateAssetMenu(
        fileName = "SaveSystemInstaller",
        menuName = "Zenject/New Save System Installer"
    )]
    public class SaveSystemInstaller : ScriptableObjectInstaller
    {
        [SerializeField, FilePath] private string _saveSystemDirectory;
        public override void InstallBindings()
        {
            //Container.Bind<ISerializedComponent>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<Core.SaveSystem>().AsSingle().WithArguments(_saveSystemDirectory);
            Container.Bind<SaveSystemMediator>().FromComponentInHierarchy().AsSingle();
        }
    }
}