using Game.Scripts.SaveSystem.Presenters;
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
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Core.SaveSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveSystemMediator>().AsSingle().NonLazy();
        }

        [Button]
        private void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}