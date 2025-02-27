using Modules.Entities;
using SampleGame.SerializedData;
using UnityEngine;

namespace SampleGame.Gameplay
{
    public class OverridedTransform : MonoBehaviour, ISerializedComponent
    {
        public SerializedComponentData Serialize() => new SerializableTransform()
        {
            Position = transform.position,
            Rotation = transform.rotation
        };
        
        public void Deserialize(SerializedComponentData serializedComponentData, EntityWorld entityWorld)
        {
            if (serializedComponentData is SerializableTransform serializableTransform)
            {
                transform.position = serializableTransform.Position;
                transform.rotation = serializableTransform.Rotation;
            }
        }
    }
}