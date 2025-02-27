using Modules.Entities;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace SampleGame.SerializedData
{
    public class SerializedTargetObject : SerializedComponentData
    {
        public int EntityID { get; set; }
    }
}