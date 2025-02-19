using System.Collections.Generic;
using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.SerializedData;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class ProductionOrder : MonoBehaviour, ISerializedComponent
    {
        ///Variable
        [SerializeField]
        private List<EntityConfig> _queue;
        
        public IReadOnlyList<EntityConfig> Queue
        {
            get { return _queue; }
            set { _queue = new List<EntityConfig>(value); }
        }

        public SerializedComponentData Serialize() => new SerializableProductionOrder() {ClassName = "SerializableProductionOrder", Queue = _queue};
        public void Deserialize(SerializedComponentData serializedComponentData)
        {
            if (serializedComponentData is SerializableProductionOrder serData) 
                _queue = serData.Queue;
        }
    }
}