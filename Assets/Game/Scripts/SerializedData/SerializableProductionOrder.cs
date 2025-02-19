using System.Collections.Generic;
using Modules.Entities;

namespace SampleGame.SerializedData
{
    public class SerializableProductionOrder : SerializedComponentData
    {
        public List<EntityConfig> Queue;
    }
}