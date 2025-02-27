using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.SerializedData;
using UnityEngine;
using Zenject;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class TargetObject : MonoBehaviour, ISerializedComponent
    {
        ///Variable
        [field: SerializeField]
        public Entity Value { get; set; }

        public SerializedComponentData Serialize() => new SerializedTargetObject() 
            {ClassName = "SerializedTargetObject", EntityID = Value == null ? -1 : Value.Id};

        public void Deserialize(SerializedComponentData serializedComponentData, EntityWorld entityWorld)
        {
            if (serializedComponentData is not SerializedTargetObject serData)
            {
                Debug.LogError($"SerializedComponentData is not a SerializedTargetObject at object {name}");
                return;
            }

            if (!entityWorld.TryGet(serData.EntityID, out var entity))
                Debug.Log($"Entity with id {serData.EntityID} not found");

            Debug.Log($"Entity is {entity}");
            Value = entity;

        }
    }
}