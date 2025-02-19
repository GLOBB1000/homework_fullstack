using System.Collections.Generic;
using Modules.Entities;
using SampleGame.Gameplay;

namespace Game.Scripts.SaveSystem.Core
{
    public interface ISaveSystem
    {
        void SetSaveData(Dictionary<Entity, List<ISerializedComponent>> saveData);
        
        bool Save();
        
        bool Load();
    }
}