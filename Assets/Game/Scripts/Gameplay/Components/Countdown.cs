using Newtonsoft.Json;
using SampleGame.SerializedData;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Countdown : MonoBehaviour, ISerializedComponent
    {
        ///Variable
        [field: SerializeField]
        public float Current { get; set; }

        ///Const
        [field: SerializeField, JsonIgnore]
        public float Duration { get; private set; }

        public SerializedComponentData Serialize() => new SerializedCountDown {ClassName = "SerializedCountDown", Current = Current};
        
        public void Deserialize(SerializedComponentData serializedComponentData)
        {
            if (serializedComponentData is SerializedCountDown serData) 
                Current = serData.Current;
        }
    }
}