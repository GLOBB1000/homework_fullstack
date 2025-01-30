using System;
using Game.Views.MoneyView;
using Modules.Money;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.View.MoneyPresenter
{
    public class MoneyPresenter : IMoneyPresenter, IInitializable 
    {
        public event Action OnMoneyGet;
        
        private IMoneyStorage _moneyStorage;
        private MoneyView _moneyView;
        
        public MoneyPresenter(IMoneyStorage moneyStorage, MoneyView moneyView, PlanetPresentersCollection presentersCollection)
        {
            _moneyStorage = moneyStorage;
            
            _moneyStorage.OnMoneyEarned += OnMoneyEarned;
            
            _moneyStorage.OnMoneyChanged += MoneyChanged;
            _moneyView = moneyView;
            
            presentersCollection.OnPlanetAdded += OnPlanetAdded;
        }

        private void OnPlanetAdded(IPlanetPresenter planetPresenter)
        {
            planetPresenter.OnMoneyGathered += OnMoneyGathered;
        }

        private void OnMoneyGathered(Transform obj)
        {
            _moneyView.PlayAnimation(obj);
        }

        private void MoneyChanged(int newvalue, int prevvalue)
        {
            _moneyView.OnMoneyChanged(newvalue, prevvalue);
        }

        private void OnMoneyEarned(int newvalue, int range)
        {
            OnMoneyGet?.Invoke();
        }

        public void Initialize()
        {
            _moneyView.OnMoneyChanged(_moneyStorage.Money,_moneyStorage.Money);
        }
    }
}