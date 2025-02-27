using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Scripts.SaveSystem.Core;
using Modules.Entities;
using SampleGame.Gameplay;

namespace Game.Scripts.SaveSystem.Presenters
{
    public class SaveSystemMediator : IDisposable
    {
        private readonly Dictionary<Entity, List<ISerializedComponent>> _savedComponents = new();
        
        private EntityWorld _entityWorld;
        private ISaveSystem _saveSystem;

        public SaveSystemMediator(EntityWorld entityWorld, ISaveSystem saveSystem)
        {
            _entityWorld = entityWorld;
            _saveSystem = saveSystem;
            
            _saveSystem.OnDataNeeded += OnDataNeeded;
        }

        private void OnDataNeeded()
        {
            _saveSystem.SetSaveData(GetSavedComponents());
        }

        private Dictionary<Entity, List<ISerializedComponent>> GetSavedComponents()
        {
            var allEntities = _entityWorld.GetAll();
            _savedComponents.Clear();

            foreach (var entity in allEntities)
            {
                _savedComponents.Add(entity, entity.GetComponents<ISerializedComponent>().ToList());
            }

            return _savedComponents;
        }

        public async Task<bool> Save()
        {
            _saveSystem.SetSaveData(GetSavedComponents());
            return await _saveSystem.Save();
        }
        
        public async Task<bool> Load(string version)
        {
            _saveSystem.SetSaveData(GetSavedComponents());
            var success = await _saveSystem.Load(version, _entityWorld);
            return success;
        }

        public void Dispose()
        {
            _saveSystem.OnDataNeeded -= OnDataNeeded;
        }
    }
}