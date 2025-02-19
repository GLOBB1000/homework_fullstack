using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.SerializedData;
using SampleGame.Gameplay;
using UnityEngine;
using Zenject;
using File = System.IO.File;

namespace Game.Scripts.SaveSystem.Core
{
    public class SaveSystem : ISaveSystem
    {
        private string _saveDirectory;
        
        private EntityWorld _entityWorld;

        private Dictionary<Entity, List<ISerializedComponent>> _saveData;
        
        private Dictionary<int, List<SerializedComponentData>> _serializedComponents = new();
        
        public SaveSystem(string saveDirectory, EntityWorld entityWorld)
        {
            _saveDirectory = saveDirectory;
            _entityWorld = entityWorld;
        }
        
        public void SetSaveData(Dictionary<Entity, List<ISerializedComponent>> saveData)
        {
            _saveData = saveData;
        }

        public bool Save()
        {
            foreach (var saves in _saveData)
            {
                _serializedComponents.Add(saves.Key.Id, new List<SerializedComponentData>());
                
                foreach (var serializedComponent in saves.Value)
                    _serializedComponents[saves.Key.Id].Add(serializedComponent.Serialize());
            }
            
            try
            {
                var json = JsonConvert.SerializeObject(_serializedComponents, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        TypeNameHandling = TypeNameHandling.All
                    });

                File.WriteAllText(_saveDirectory, json);

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        public bool Load()
        {
            var json = File.ReadAllText(_saveDirectory);
            
            Dictionary<int, List<SerializedComponentData>> loaded = JsonConvert.DeserializeObject<Dictionary<int, List<SerializedComponentData>>>(json, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
            
            foreach (var load in loaded)
            {
                var entity = _saveData.Keys.First(x => x.Id == load.Key);

                for (int i = 0; i < _saveData[entity].Count; i++)
                {
                    if (load.Value[i] is SerializedTargetObject serializedTargetObject)
                    {
                        serializedTargetObject.SetEntity(serializedTargetObject.EntityID == -1 ? null : _entityWorld.Get(serializedTargetObject.EntityID));
                    }
                    
                    _saveData[entity][i].Deserialize(load.Value[i]);
                }
            }
            
            return true;
        }
    }
}