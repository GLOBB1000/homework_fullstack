using Modules.Entities;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace SampleGame.SerializedData
{
    public class SerializedTargetObject : SerializedComponentData
    {
        [Inject] private EntityWorld entityWorld;
        
        public int EntityID { get; set; }
        
        [JsonIgnore]
        public Entity Entity { get; private set; }

        public void SetEntity(Entity entity)
        {
            Entity = entity;
        }
    }
}