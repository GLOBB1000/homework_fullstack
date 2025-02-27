using System;
using Game.Scripts.SaveSystem.Core;
using Game.Scripts.SaveSystem.Presenters;
using UnityEngine;

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
            callback.Invoke(await _saveSystemMediator.Save(), PlayerPrefs.GetInt("SaveVersion"));
        }

        public async void Load(string versionText, Action<bool, int> callback)
        {
            //TODO:
            callback.Invoke(await _saveSystemMediator.Load(versionText), Convert.ToInt32(versionText));
        }
    }
}