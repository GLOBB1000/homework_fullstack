using System;
using SampleGame.Common;

namespace SampleGame.SerializedData
{
    [Serializable]
    public class SerializedTeam : SerializedComponentData
    {
        public TeamType Type { get; set; }
    }
}