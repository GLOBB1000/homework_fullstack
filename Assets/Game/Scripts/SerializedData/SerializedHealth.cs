using System;

namespace SampleGame.SerializedData
{
    [Serializable]
    public class SerializedHealth : SerializedComponentData
    {
        public int CurrentHealth { get; set; }
    }
}