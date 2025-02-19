using UnityEngine;
using Newtonsoft.Json;
using SampleGame.Common;
using SampleGame.SerializedData;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Health : MonoBehaviour, ISerializedComponent
    {
        ///Variable
        [field: SerializeField]
        public int Current { get; set; } = 50;

        ///Const
        [field: SerializeField, JsonIgnore]
        public int Max { get; private set; } = 100;

        public SerializedComponentData Serialize() => new SerializedHealth { ClassName = "SerializedHealth", CurrentHealth = Current };

        public void Deserialize(SerializedComponentData serializedComponentData)
        {
            if (serializedComponentData is SerializedHealth serData) 
                Current = serData.CurrentHealth;
        }
    }
}