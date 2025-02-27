using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.Entities;
using SampleGame.Gameplay;

namespace Game.Scripts.SaveSystem.Core
{
    public interface ISaveSystem
    {
        public event Action OnDataNeeded;
        
        void SetSaveData(Dictionary<Entity, List<ISerializedComponent>> saveData);
        
        Task<bool> Save();
        
        Task<bool> Load(string version, EntityWorld entityWorld);
    }
}