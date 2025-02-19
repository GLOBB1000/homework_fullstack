using System.Text;
using Game.Scripts.SaveSystem.Core;
using SampleGame.Common;
using SampleGame.SerializedData;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class ResourceBag : MonoBehaviour, ISerializedComponent
    {
        ///Variable
        [field: SerializeField]
        public ResourceType Type { get; set; }
        
        ///Variable
        [field: SerializeField]
        public int Current { get; set; }
        
        ///Const
        [field: SerializeField]
        public int Capacity { get; set; }

        public SerializedComponentData Serialize() => new SerializedRecourseBag() {ClassName = "SerializedRecourseBag", Type = Type, Current = Current};
        
        public void Deserialize(SerializedComponentData serializedComponentData)
        {
            if (serializedComponentData is not SerializedRecourseBag serData) return;
            
            Type = serData.Type;
            Current = serData.Current;
        }
    }
}