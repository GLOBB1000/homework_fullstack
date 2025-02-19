using System;
using Game.Scripts.SaveSystem.Core;
using Game.Scripts.SaveSystem.Presenters;

namespace Game.Gameplay
{
    public sealed class ControlsPresenter : IControlsPresenter
    {
        private SaveSystemMediator _saveSystemMediator;
        
        public ControlsPresenter(SaveSystemMediator saveSystemMediator)
        {
            _saveSystemMediator = saveSystemMediator;
        }
        public async void Save(Action<bool, int> callback)
        {
            //TODO:
            callback.Invoke(_saveSystemMediator.Save(), -1);
        }

        public async void Load(string versionText, Action<bool, int> callback)
        {
            //TODO:
            callback.Invoke(_saveSystemMediator.Load(), -1);
        }
    }
}