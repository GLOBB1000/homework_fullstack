using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.SerializedData;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class TargetObject : MonoBehaviour, ISerializedComponent
    {
        ///Variable
        [field: SerializeField]
        public Entity Value { get; set; }

        public SerializedComponentData Serialize() => new SerializedTargetObject() {ClassName = "SerializedTargetObject", EntityID = Value == null ? -1 : Value.Id};
        
        public void Deserialize(SerializedComponentData serializedComponentData)
        {
            if (serializedComponentData is SerializedTargetObject serData)
                Value = serData.Entity;
        }
    }
}