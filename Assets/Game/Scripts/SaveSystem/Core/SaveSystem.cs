using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Game.Scripts.Rest;
using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.SerializedData;
using SampleGame.Gameplay;
using UnityEngine;
using File = System.IO.File;

namespace Game.Scripts.SaveSystem.Core
{
    public class SaveSystem : ISaveSystem
    {
        public event Action OnDataNeeded;
        
        private string _saveDirectory;
        
        private IHttpRestHandler _httpRestHandler;
        
        private Dictionary<Entity, List<ISerializedComponent>> _saveData;
        
        private Dictionary<EntitySerializedData, List<SerializedComponentData>> _serializedComponents = new();

        private int _saveVersion;

        public SaveSystem()
        {
            _saveVersion = PlayerPrefs.GetInt("SaveVersion");
            _saveDirectory = Path.Combine(Application.streamingAssetsPath, $"data_{_saveVersion}.json");
            
            _httpRestHandler = new HttpRestHandler();
        }
        
        public void SetSaveData(Dictionary<Entity, List<ISerializedComponent>> saveData)
        {
            _saveData = saveData;
        }

        public async Task<bool> Save()
        {
            _serializedComponents.Clear();
            
            foreach (var saves in _saveData)
            {
                var s_entity = new EntitySerializedData() { EntityId = saves.Key.Id, EntityName = saves.Key.Name };
                _serializedComponents.Add(s_entity, new List<SerializedComponentData>());
                
                foreach (var serializedComponent in saves.Value)
                    _serializedComponents[s_entity].Add(serializedComponent.Serialize());
            }
            
            try
            {
                var dList = _serializedComponents.ToList();
                
                var json = JsonConvert.SerializeObject(dList, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        TypeNameHandling = TypeNameHandling.All
                    });

                if (!File.Exists(_saveDirectory))
                {
                    File.Create(_saveDirectory).Close();
                }
                
                await File.WriteAllTextAsync(_saveDirectory, json);
                await _httpRestHandler.Save(_saveVersion.ToString(), json);
                
                UpdateVersion();
                
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        private void UpdateVersion()
        {
            PlayerPrefs.SetInt("SaveVersion", _saveVersion);
            _saveVersion++;
            _saveDirectory = Path.Combine(Application.streamingAssetsPath, $"data_{_saveVersion}.json");
        }

        public async Task<bool> Load(string version, EntityWorld entityWorld)
        {
            var response = await _httpRestHandler.Load(version);

            var json = response;
            
           var loaded = 
                JsonConvert.DeserializeObject<List<KeyValuePair<EntitySerializedData, List<SerializedComponentData>>>>(json,new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                }).ToDictionary(kv => kv.Key, kv => kv.Value);

           if (loaded.Keys.Count < _saveData.Count)
           {
               var loadedEntities = loaded.Keys.ToList();
               
               foreach (var entity in _saveData)
               {
                   if(!loadedEntities.Exists(x => x.EntityId == entity.Key.Id))
                       entityWorld.Destroy(entity.Key);
               }
           }
            
            foreach (var load in loaded)
            {
                var entities = _saveData.Keys.ToList();
                
                
                var entity = entities.Exists(x => x.Id == load.Key.EntityId) 
                    ? _saveData.Keys.First(x => x.Id == load.Key.EntityId) 
                    : entityWorld.Spawn(load.Key.EntityName ,Vector3.zero, Quaternion.identity);
                
                if(!entities.Exists(x => entity == x))
                {
                    OnDataNeeded?.Invoke();
                    Debug.Log("Entity not exists");
                }

                for (int i = 0; i < _saveData[entity].Count; i++)
                {
                    _saveData[entity][i].Deserialize(load.Value[i], entityWorld);
                }
            }
            
            return true;
        }
    }
}