using Newtonsoft.Json;
using SampleGame.Common;
using SampleGame.SerializedData;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Team : MonoBehaviour, ISerializedComponent
    {
        ///Variable
        [field: SerializeField]
        public TeamType Type { get; set; }

        public SerializedComponentData Serialize() => new SerializedTeam { ClassName = "SerializedTeam", Type = Type };
        public void Deserialize(SerializedComponentData serializedComponentData)
        {
            if (serializedComponentData is SerializedTeam serData)
                Type = serData.Type;
        }
    }
}