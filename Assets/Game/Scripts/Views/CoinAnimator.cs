using System.Collections.Generic;
using Game.View;
using Modules.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public class CoinAnimator : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private ParticleAnimator particleAnimator;

        private List<IPlanetPresenter> _planetPresenters;

        [Inject]
        public void Construct(List<IPlanetPresenter> planetPresenters)
        {
            _planetPresenters = planetPresenters;
            
            foreach (var view in _planetPresenters)
                view.OnMoneyGathered += ViewOnOnMoneyGathered;
        }

        private void OnDestroy()
        {
            foreach (var view in _planetPresenters)
                view.OnMoneyGathered -= ViewOnOnMoneyGathered;
        }

        private void ViewOnOnMoneyGathered(Transform viewTransform)
        {
            particleAnimator.Emit(viewTransform.position, target.position);
        }
    }
}