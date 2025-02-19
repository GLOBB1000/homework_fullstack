using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.SaveSystem.Core;
using Modules.Entities;
using SampleGame.Gameplay;
using UnityEngine;
using Zenject;

namespace Game.Scripts.SaveSystem.Presenters
{
    public class SaveSystemMediator : MonoBehaviour
    {
        private Dictionary<Entity, List<ISerializedComponent>> _savedComponents = new Dictionary<Entity, List<ISerializedComponent>>();
        
        private EntityWorld _entityWorld;
        private ISaveSystem _saveSystem;

        [Inject]
        public void Construct(EntityWorld entityWorld, ISaveSystem saveSystem)
        {
            _entityWorld = entityWorld;
            _saveSystem = saveSystem;
        }

        private void Start()
        {
            _saveSystem.SetSaveData(GetSavedComponents());
        }

        private Dictionary<Entity, List<ISerializedComponent>> GetSavedComponents()
        {
            var allEntities = _entityWorld.GetAll();

            foreach (var entity in allEntities)
            {
                _savedComponents.Add(entity, entity.GetComponents<ISerializedComponent>().ToList());
            }

            return _savedComponents;
        }

        public bool Save()
        {
            return _saveSystem.Save();
        }
        
        public bool Load()
        {
            return _saveSystem.Load();
        }
        
    }
}