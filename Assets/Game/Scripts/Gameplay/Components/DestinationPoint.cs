using SampleGame.Common;
using SampleGame.SerializedData;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class DestinationPoint : MonoBehaviour, ISerializedComponent
    {
        ///Variable
        [field: SerializeField]
        public Vector3 Value { get; set; }

        public SerializedComponentData Serialize() => new SerializedDestinationPoint() { ClassName = "SerializedDestinationPoint", Value = new SerializedVector3(Value)};
        
        public void Deserialize(SerializedComponentData serializedComponentData)
        {
            if (serializedComponentData is SerializedDestinationPoint serData) 
                Value = serData.Value;
        }
    }
}