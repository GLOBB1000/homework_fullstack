using Modules;
using ProgressUI;
using ScoreHandler;
using TMPro;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private TextMeshPro progressText;

        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;
        
        [Inject] private IScore score;
        [Inject] private IDifficulty difficulty;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScoreObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScorePresenter>().FromNew().AsSingle().WithArguments(scoreText, score).NonLazy();
            
            Container.BindInterfacesAndSelfTo<ProgressPresenter>().FromNew().AsSingle().WithArguments(progressText, difficulty).NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameStateUI.GameStateUI>().FromNew().AsSingle().WithArguments(loseScreen, winScreen).NonLazy();
        }
    }
}